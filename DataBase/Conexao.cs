using MySqlConnector;

namespace CadastroContatos.DataBase;

public class Conexao
{
    private static string connectionString = Environment.GetEnvironmentVariable("");

    public static MySqlConnection Connection => new MySqlConnection(connectionString);
}