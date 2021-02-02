using Bogus;
using CursoOnLine.Dominio.Entidades;
using CursoOnLine.Dominio.Enumerados;

namespace CursoOnLine.DominioTest._Builder
{
    public class CursoBuilder
    {        
        private string _nome;
        private string _descricao;
        private double _cargaHoraria;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Universitario;
        private double _valor;

        public CursoBuilder()
        {
            var faker = new Faker();

            _nome = faker.Company.CompanyName();            
            _descricao = faker.Lorem.Paragraph();
            _cargaHoraria = faker.Random.Double(50, 100);
            _valor = faker.Random.Double(200, 1000);
        }
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
