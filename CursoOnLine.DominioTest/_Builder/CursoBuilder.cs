using CursoOnLine.Dominio.Entidades;
using CursoOnLine.Dominio.Enumerado;

namespace CursoOnLine.DominioTest._Builder
{
    public class CursoBuilder
    {
        private string _nome = "Curso de Xunit";
        private string _descricao = "Curso para verificaçãõ de Xunit e Moq";
        private double _cargaHoraria = 50;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Universitario;
        private double _valor = 890;
        
        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public Curso Build()
        {
            return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double carga)
        {
            _cargaHoraria = carga;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publico)
        {
            _publicoAlvo = publico;
            return this;
        }

    }
}
