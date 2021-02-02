using CursoOnLine.Dominio.Enumerado;
using System;
using CursoOnLine.Dominio.Constantes;

namespace CursoOnLine.Dominio.Entidades
{
    public class Curso
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }
        public double Valor { get; set; }
        
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
