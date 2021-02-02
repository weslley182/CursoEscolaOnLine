using Bogus;
using CursoOnLine.Dominio.Entidades;
using CursoOnLine.Dominio.Enumerados;
using Moq;
using System;
using Xunit;

namespace CursoOnLine.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest: IDisposable
    {
        private readonly CursoDto _cursoDto;
        public ArmazenadorDeCursoTest()
        {
            var faker = new Faker();
            
            _cursoDto = new CursoDto
            {
                Nome = faker.Random.Words(),
                Descricao = faker.Lorem.Paragraph(),
                PublicoAlvoId = 1,
                CargaHoraria = faker.Random.Double(100, 500),
                Valor = faker.Random.Double(800, 2000)
            };
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            var repoMock = new Mock<ICursoRepositorio>();

            var armazenadorDeCurso = new ArmazenadorDeCurso(repoMock.Object);

            armazenadorDeCurso.Armazenar(_cursoDto);
            //verifica se passou por adicionar
            repoMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
            //verifica se o nome esta correto, e tbm verifica que tem que ser chamado uma vez
            repoMock.Verify(r => r.Adicionar(It.Is<Curso>(c => c.Nome == _cursoDto.Nome)));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
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
            var curso =
                new Curso(cursoDto.Nome, cursoDto.Descricao, 
                    cursoDto.CargaHoraria, PublicoAlvo.Empreendedor, cursoDto.Valor);
            
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public int PublicoAlvoId { get; set; }
        public double Valor { get; set; }
    }
}
