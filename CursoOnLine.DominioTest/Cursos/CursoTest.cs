using CursoOnLine.Dominio.Constantes;
using CursoOnLine.Dominio.Entidades;
using CursoOnLine.Dominio.Enumerado;
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

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Criando teste.");
        }

        public void Dispose()
        {
            _output.WriteLine("Fechando teste.");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Curso xUnit",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Universitario,
                Valor = (double)300
            };

            var curso =
                new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = nomeInvalido,
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Universitario,
                Valor = (double)300
            };

            Assert
                .Throws<ArgumentException>(
                    () =>
                    new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)
                ).ComMensagem(ConstantesMensagens.CURSO_NOME_INVALIDO);
            
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.30)]
        [InlineData(-1)]
        [InlineData(0.90)]
        [InlineData(null)]        
        public void CursoNaoDeveTerCargaHorariaMenorQueUm(double cargaHoraria)
        {
            var cursoEsperado = new
            {
                Nome = "Curso de Testes",
                CargaHoraria = cargaHoraria,
                PublicoAlvo = PublicoAlvo.Universitario,
                Valor = (double)300
            };

            Assert
                .Throws<ArgumentException>(
                    () =>
                    new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)
                ).ComMensagem(ConstantesMensagens.CURSO_CARGA_HORARIA_INVALIDA);
        }

        [Theory]
        [InlineData(0)]        
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(null)]
        public void CursoDeveTerValorMaiorQueZero(double valor)
        {
            var cursoEsperado = new
            {
                Nome = "Curso de Testes",
                CargaHoraria = 20,
                PublicoAlvo = PublicoAlvo.Universitario,
                Valor = valor
            };

            Assert
                .Throws<ArgumentException>(
                    () =>
                    new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)                
                ).ComMensagem(ConstantesMensagens.CURSO_VALOR_INVALIDO);
        }        
    }
}
