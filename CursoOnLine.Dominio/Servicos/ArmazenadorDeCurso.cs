using CursoOnLine.Dominio.Constantes;
using CursoOnLine.Dominio.Dados;
using CursoOnLine.Dominio.Entidades;
using CursoOnLine.Dominio.Enumerados;
using System;

namespace CursoOnLine.Dominio.Servicos
{
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

            if (cursoJaSalvo != null)
            {
                throw new ArgumentException(ConstantesMensagens.ARMAZENADORDECURSO_NOME_CONSTA_BANCO);
            }

            Enum.TryParse(typeof(PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
            {
                throw new ArgumentException(ConstantesMensagens.ARMAZENADORDECURSO_PUBLICO_INVALIDO);
            }

            var curso =
                new Curso(cursoDto.Nome, cursoDto.Descricao,
                    cursoDto.CargaHoraria, (PublicoAlvo)publicoAlvo, cursoDto.Valor);

            _cursoRepositorio.Adicionar(curso);
        }
    }
}
