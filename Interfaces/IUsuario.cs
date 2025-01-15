using CadastroContatos.Models;

namespace CadastroContatos.Interfaces
{
    public interface IUsuario
    {
        int CadastraUsuario(UsuarioModel usuario, ContaModel conta);
        UsuarioModel Login(string email, string senha);
    }
}