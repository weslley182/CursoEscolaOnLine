using CursoOnLine.Dominio.Entidades;

namespace CursoOnLine.Dominio.Dados
{
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}
