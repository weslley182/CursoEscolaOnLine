using CursoOnLine.Dominio.Enumerado;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnLine.Dominio.Entidades
{
    public class Curso
    {
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }
        public double Valor { get; set; }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome do Curso não pode ser inválido.");
            }
            if(cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horária do curso não pode ser abaixo de 1 hora.");
            }

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }
}
