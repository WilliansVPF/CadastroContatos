namespace CadastroContatos.Services
{
    public class HashService
    {
        public static string Salt() => BCrypt.Net.BCrypt.GenerateSalt();
        
        public static string Hash(string senha, string salt) => BCrypt.Net.BCrypt.HashPassword(senha, salt);

    }   
}