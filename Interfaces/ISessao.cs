using CadastroContatos.Models;

namespace CadastroContatos.Interfaces
{
    public interface ISessao
    {
        void CriarSessao(UsuarioModel usuario);
        void RemoverSessao();
        UsuarioModel? BuscarSessao();
    }
}