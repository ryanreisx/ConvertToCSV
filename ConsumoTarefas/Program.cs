using Newtonsoft.Json;
using SistemaDeTarefas.Model;
using System.Text.Json.Serialization;
using System;
using System.Data.SqlClient;


namespace ConsumoTarefas
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7038/")};
            var response = await client.GetAsync("api/Tarefa");
            var content = await response.Content.ReadAsStringAsync();

            var tarefas = JsonConvert.DeserializeObject<TarefaModel[]>(content);
            using (var writer = new StreamWriter("C:\\Users\\Ryan\\Desktop\\docs txt\\dadosTarefa.csv"))

            foreach (var tarefa in tarefas)
            {
                writer.WriteLine($"Id:{tarefa.Id}");
                writer.WriteLine($"Nome:{tarefa.Nome}");
                writer.WriteLine($"Descricao:{tarefa.Descricao}");
                writer.WriteLine($"Status:{tarefa.Status}");
                writer.WriteLine($"UsuarioId:{tarefa.UsuarioId}");
                writer.WriteLine();
            }
            

            string connectionString = "Data Source=endor.agentemr.com.br,2432;Initial Catalog=db_ProjetomvcProd;User ID=ryan;Password=*********";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abra a conexão com o banco de dados
                connection.Open();

                // Execute uma consulta SQL para obter os dados desejados
                string query = "SELECT Top 1000 * FROM Contato";
                SqlCommand command = new SqlCommand(query, connection);


                using (var writer = new StreamWriter("C:\\Users\\Ryan\\Desktop\\docs txt\\dadosContato.csv"))
                {
                    // Execute o comando e obtenha um leitor de dados (DataReader)
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verifique se há linhas retornadas
                        if (reader.HasRows)
                        {
                            // Itere sobre as linhas retornadas
                            while (reader.Read())
                            {
                                // Acesse os valores das colunas pelos índices ou nomes das colunas
                                int ContatoId = reader.GetInt32(0);
                                string ContatoCI = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                string Nome = reader.GetString(2);
                                string Sexo = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                                DateTime DataNascimento = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4);
                                string CPF = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                                int EmpresaID = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                                int ClienteId = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                                DateTime DataUltimaVisualizacao = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8);
                                int Escore = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                                bool Descadastrar = reader.IsDBNull(10) ? false : reader.GetBoolean(10);
                                int FilialId = reader.IsDBNull(11) ? 0 : reader.GetInt32(11);
                                int UsuarioId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                                DateTime UltimaAlteracao = reader.IsDBNull(13) ? DateTime.MinValue : reader.GetDateTime(13);
                                int PosicaoRanking = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                                DateTime DataCadastro = reader.IsDBNull(15) ? DateTime.MinValue : reader.GetDateTime(15);
                                string Email = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                                DateTime DataUltimoEnvioEmail = reader.IsDBNull(17) ? DateTime.MinValue : reader.GetDateTime(17);
                                byte StatusEnvioEmailListaId = reader.IsDBNull(18) ? (byte)0 : reader.GetByte(18);
                                byte ClassificaoEmail = reader.IsDBNull(19) ? (byte)0 : reader.GetByte(19);
                                DateTime DataIntegracao = reader.IsDBNull(20) ? DateTime.MinValue : reader.GetDateTime(20);
                                int OptOutWhatsApp = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                                string CodigoMGM = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);


                                // Faça o que desejar com os dados obtidos
                                writer.WriteLine($"ID: {ContatoId},ContatoCl:{ContatoCI}, Nome:{Nome}, Sexo: {Sexo}, DataNascimento:{DataNascimento}, CPF:{CPF}, EmpresaId:{EmpresaID}, ClienteId:{ClienteId}, DataUltimaVisualizacao:{DataUltimaVisualizacao}, Escore:{Escore}, Descadastrar:{Descadastrar}, FilialId:{FilialId}, UsuarioId:{UsuarioId}, UltimaAlteracao:{UltimaAlteracao}, PosicaoRanking:{PosicaoRanking}, DataCadastro:{DataCadastro}, Email:{Email}, DataUltimoEnvioEmail:{DataUltimoEnvioEmail}, StatusEnvioEmailListaId:{StatusEnvioEmailListaId}, ClassificacaoEmail:{ClassificaoEmail}, DataIntegracao:{DataIntegracao},OptOutWhatsApp:{OptOutWhatsApp}, CodigoMGM:{CodigoMGM}");
                                writer.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum dado retornado.");
                        }
                    }

                }

            }
        }
    }
}

