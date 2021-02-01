using CursoOnLine.Dominio.Entidades;
using CursoOnLine.Dominio.Enumerado;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnLine.DominioTest.Cursos
{
    public class CursoTest
    {
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
    }
}
