using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Controle_de_Convidados
{
    public class ResultadoPesquisa
    {
        public int CodConvidado { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Email { get; set; }
    }
    public class DatabaseManager
    {
        private MySqlConnection connection;
        private string connectionString;

        public DatabaseManager()
        {
           
            string server = "localhost";
            string database = "dbControledeConvites";
            string username = "gabriel almeida";
            string password = "p1ctasuncservant";

            connectionString = $"Server={server};Database={database};User ID={username};Password={password}";

            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                
                return false;
            }

        }

        public void UpdateIpAddress(string newIpAddress)
        {
            
        }
        public void InsertConvidado(string nome, string cpf, string endereco, string bairro, string cep, string email)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO tbConvidados (nome, cpf, endereco, bairro, cep, email) " +
                               "VALUES (@Nome, @Cpf, @Endereco, @Bairro, @Cep, @Email)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Cpf", cpf);
                cmd.Parameters.AddWithValue("@Endereco", endereco);
                cmd.Parameters.AddWithValue("@Bairro", bairro);
                cmd.Parameters.AddWithValue("@Cep", cep);
                cmd.Parameters.AddWithValue("@Email", email);

                cmd.ExecuteNonQuery();
            } // A conexão é fechada automaticamente quando o bloco "using" é encerrado
        }
        public void UpdateConvidado(int codConvidado, string nome, string cpf, string endereco, string bairro, string cep, string email)
        {
            string query = "UPDATE tbConvidados " +
                           "SET nome = @Nome, cpf = @Cpf, endereco = @Endereco, bairro = @Bairro, Cep = @Cep, E-mail = @Email " +
                           "WHERE codConvidado = @CodConvidado";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nome", nome);
            cmd.Parameters.AddWithValue("@Cpf", cpf);
            cmd.Parameters.AddWithValue("@Endereco", endereco);
            cmd.Parameters.AddWithValue("@Bairro", bairro);
            cmd.Parameters.AddWithValue("@Cep", cep);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@CodConvidado", codConvidado);

            cmd.ExecuteNonQuery();
        }
        public List<ResultadoPesquisa> GetConvidadosPorNome(string nomeParaPesquisar)
        {
            List<ResultadoPesquisa> resultados = new List<ResultadoPesquisa>();

            
            string query = "SELECT * FROM tbConvidados WHERE nome = @Nome";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nome", nomeParaPesquisar);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ResultadoPesquisa resultado = new ResultadoPesquisa
                {
                    CodConvidado = reader.GetInt32("codConvidado"),
                    Nome = reader.GetString("nome"),
                    Cpf = reader.GetString("cpf"),
                    Endereco = reader.GetString("endereco"),
                    Bairro = reader.GetString("bairro"),
                    Cep = reader.GetString("Cep"),
                    Email = reader.GetString("E-mail")
                };
                resultados.Add(resultado);
            }

            reader.Close();
            return resultados;
        }


        public void DeleteConvidado(int codConvidado)
        {
            string query = "DELETE FROM tbConvidados WHERE codConvidado = @CodConvidado";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CodConvidado", codConvidado);

            cmd.ExecuteNonQuery();
        }
    }

}
