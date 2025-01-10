using MySqlConnector;

namespace CadastroContatos.DataBase;

public class Conexao
{
    private static string connectionString = Environment.GetEnvironmentVariable("CadastroContatoConnectionString");
    public static MySqlConnection GetConnection => new MySqlConnection(connectionString);
}