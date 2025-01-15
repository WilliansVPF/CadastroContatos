using System;
using CadastroContatos.Interfaces;
using CadastroContatos.Models;
using CadastroContatos.Services;
using MySqlConnector;

namespace CadastroContatos.DataBase
{
    public class UsuarioDb : IUsuario
    {

        public int CadastraUsuario(UsuarioModel usuario, ContaModel conta)
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

        private string? GetSalt(string email)
        {
            try
            {
                using (var connection = Conexao.GetConnection)
                {
                    connection.Open();

                    string sql = "SELECT salt FROM Conta WHERE email = @email;";

                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetString("salt");
                            }
                        }
                    }
                }
                return "";  
            }
            catch (Exception e)
            {
                return null;
            }            
        }

        private int VerificaSenha(string email, string senha, string salt)
        {
            try
            {
                using (var connection = Conexao.GetConnection)
                {
                    connection.Open();

                    string sql = "Select idUsuario from Conta where email = @email AND senha = @senha;";

                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@senha", HashService.Hash(senha, salt));

                        var resultado = command.ExecuteScalar();
                        return Convert.ToInt32(resultado);
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public UsuarioModel? Login(string email, string senha)
        {
            string salt = GetSalt(email);

            if (salt == null)
            {
                return null;
            }

            int idUsuario = VerificaSenha(email, senha, salt);

            if (idUsuario == -1)
            {
                return null;
            }

            UsuarioModel usuario = new UsuarioModel();

            try
            {
                using (var connection = Conexao.GetConnection)
                {
                    connection.Open();

                    string sql = "SELECT idUsuario, nome FROM Usuario WHERE idUsuario = @idUsuario;";

                    using (var command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario.IdUsuario = reader.GetInt32("idUsuario");
                                usuario.Nome = reader.GetString("nome");
                            }
                            return usuario;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}