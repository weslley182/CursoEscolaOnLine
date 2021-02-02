﻿using Bogus;
using CursoOnLine.Dominio.Constantes;
using CursoOnLine.Dominio.Entidades;
using CursoOnLine.Dominio.Enumerados;
using CursoOnLine.DominioTest._Builder;
using CursoOnLine.DominioTest._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnLine.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest: IDisposable
    {
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _repoMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        public ArmazenadorDeCursoTest()
        {
            var faker = new Faker();
            
            _cursoDto = new CursoDto
            {
                Nome = faker.Random.Words(),
                Descricao = faker.Lorem.Paragraph(),
                PublicoAlvo = "Estudante",
                CargaHoraria = faker.Random.Double(100, 500),
                Valor = faker.Random.Double(800, 2000)
            };

            _repoMock = new Mock<ICursoRepositorio>();

            _armazenadorDeCurso = new ArmazenadorDeCurso(_repoMock.Object);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);
            //verifica se passou por adicionar
            _repoMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
            //verifica se o nome esta correto, e tbm verifica que tem que ser chamado uma vez
            _repoMock.Verify(r => r.Adicionar(It.Is<Curso>(c => c.Nome == _cursoDto.Nome)));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            const string PUBLICOALVO_INVALIDO = "Médico";
            _cursoDto.PublicoAlvo = PUBLICOALVO_INVALIDO;            

            Assert.Throws<ArgumentException>(
                () =>
                _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem(ConstantesMensagens.ARMAZENADORDECURSO_PUBLICO_INVALIDO);
        }        
        
        [Fact]
        public void NaoDeveSalvarCursoComMesmoNomeDeOurtoJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _repoMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(
                () =>
                _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem(ConstantesMensagens.ARMAZENADORDECURSO_NOME_CONSTA_BANCO);
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        Curso ObterPeloNome(string nome);
    }

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);
            
            if(cursoJaSalvo != null)
            {
                throw new ArgumentException(ConstantesMensagens.ARMAZENADORDECURSO_NOME_CONSTA_BANCO);
            }

            Enum.TryParse(typeof(PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);
            
            if(publicoAlvo == null)
            {
                throw new ArgumentException(ConstantesMensagens.ARMAZENADORDECURSO_PUBLICO_INVALIDO);
            }

            var curso =
                new Curso(cursoDto.Nome, cursoDto.Descricao, 
                    cursoDto.CargaHoraria, (PublicoAlvo)publicoAlvo, cursoDto.Valor);
            
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public int PublicoAlvoId { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}