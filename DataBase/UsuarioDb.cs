using System;
using CadastroContatos.Models;
using MySqlConnector;

namespace CadastroContatos.DataBase
{
    public class UsuarioDb
    {
        
        public static int CadastraUsuario(UsuarioModel usuario, ContaModel conta)
        {
            long i = 0;
            try
            {
                using (var connection = Conexao.GetConnection)
                {
                    connection.Open();

                    using (var transation = connection.BeginTransaction())
                    {
                        try
                        {
                            string sql = "INSERT INTO Usuario VALUES (0, @nome);";

                            using (var command = new MySqlCommand(sql, connection))
                            {
                                command.Transaction = transation;
                                command.Parameters.AddWithValue("@nome", usuario.Nome);
                                command.ExecuteNonQuery();
                                i = command.LastInsertedId;
                            }

                            sql = "INSERT INTO Conta VALUES (0, @email, @senha, @salt , @idUsuario)";

                            using (var command = new MySqlCommand(sql, connection))
                            {
                                command.Transaction = transation;
                                command.Parameters.AddWithValue("@idUsuario", i);
                                command.Parameters.AddWithValue("@email", conta.Email);
                                command.Parameters.AddWithValue("@senha", conta.Senha);
                                command.Parameters.AddWithValue("@salt", conta.Salt);
                                command.ExecuteNonQuery();
                            }

                            transation.Commit();
                            return 0;
                        }
                        catch (Exception e)
                        {
                            transation.Rollback();
                            return -1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}