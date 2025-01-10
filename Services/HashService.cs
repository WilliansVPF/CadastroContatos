namespace CadastroContatos.Services
{
    public class HashService
    {
        // public static string Hash(string senha, string salt)
        // {
        //     return string senha = BCrypt.Net.BCrypt.HashPassword(senha, salt);
        // }

        public static string Hash(string senha, string salt) => BCrypt.Net.BCrypt.HashPassword(senha, salt);

        public static string Salt() => BCrypt.Net.BCrypt.GenerateSalt();
    }   
}