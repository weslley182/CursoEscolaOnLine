using Bogus;
using CursoOnLine.Dominio.Constantes;
using CursoOnLine.Dominio.Entidades;
using CursoOnLine.Dominio.Enumerados;
using CursoOnLine.DominioTest._Builder;
using CursoOnLine.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnLine.DominioTest.Cursos
{
    public class CursoTest: IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly string _descricao;
        private readonly double _cargaHoraria;
        private readonly double _valor;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Criando teste.");

            var faker = new Faker();

            _nome = faker.Random.Word();
            _descricao = faker.Lorem.Paragraph();
            _cargaHoraria = faker.Random.Double(50, 100);
            _valor = faker.Random.Double(200, 1000);
        }

        public void Dispose()
        {
            _output.WriteLine("Fechando teste.");
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                Descricao = _descricao,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = PublicoAlvo.Universitario,
                Valor = _valor
            };

            var curso =
                new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerNomeInvalido(string nomeInvalido)
        {
            Assert
                .Throws<ArgumentException>(
                    () =>
                    CursoBuilder.Novo().ComNome(nomeInvalido).Build()
                ).ComMensagem(ConstantesMensagens.CURSO_NOME_INVALIDO);            
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.30)]
        [InlineData(-1)]
        [InlineData(0.90)]        
        public void CursoNaoDeveTerCargaHorariaMenorQueUm(double cargaHorariaInvalida)
        {
            Assert
                .Throws<ArgumentException>(
                    () =>
                    CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build()
                ).ComMensagem(ConstantesMensagens.CURSO_CARGA_HORARIA_INVALIDA);
        }

        [Theory]
        [InlineData(0)]        
        [InlineData(-1)]
        [InlineData(-100)]        
        public void CursoDeveTerValorMaiorQueZero(double valorInvalido)
        {
            Assert
                .Throws<ArgumentException>(
                    () =>
                    CursoBuilder.Novo().ComValor(valorInvalido).Build()
                ).ComMensagem(ConstantesMensagens.CURSO_VALOR_INVALIDO);
        }        
    }
}
