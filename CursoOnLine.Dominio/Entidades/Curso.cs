using CursoOnLine.Dominio.Enumerados;
using System;
using CursoOnLine.Dominio.Constantes;

namespace CursoOnLine.Dominio.Entidades
{
    public class Curso
    {
        public string Nome { get; init; }
        public string Descricao { get; init; }
        public double CargaHoraria { get; init; }
        public PublicoAlvo PublicoAlvo { get; init; }
        public double Valor { get; init; }
        
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException(ConstantesMensagens.CURSO_NOME_INVALIDO);
            }

            if(cargaHoraria < 1)
            {
                throw new ArgumentException(ConstantesMensagens.CURSO_CARGA_HORARIA_INVALIDA);
            }

            if(!(valor > 0))
            {
                throw new ArgumentException(ConstantesMensagens.CURSO_VALOR_INVALIDO);
            }

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }
}
