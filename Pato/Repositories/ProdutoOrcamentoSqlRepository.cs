using System.Data.SqlClient;
using Pato.Models;

namespace Pato.Repositories
{
    public class ProdutoOrcamentoSqlRepository : DBContext, IProdutoOrcamentoRepository
    {
        public void Create(ProdutoOrcamento produtoOrcamento)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"INSERT INTO ProdutoOrcamento 
                    VALUES (@idOrcamento, @idProduto)";

                cmd.Parameters.AddWithValue("@idOrcamento", produtoOrcamento.IdOrcamento);
                cmd.Parameters.AddWithValue("@idProduto", produtoOrcamento.IdProduto);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                // Logar os erros (Sentry, App Insights, etc)...
            }
            finally
            {
                Dispose();
            }
        }
        public void Delete(int id)
        {
            try{
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"DELETE FROM ProdutoOrcamento WHERE IdPo = @id";

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                // Logar os erros (Sentry, App Insights, etc)...
            }
            finally
            {
                Dispose();
            }
        }

        public List<ProdutoOrcamento> Read()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT ProdutoOrcamento.*, Produto.Nome as Produto FROM ProdutoOrcamento JOIN Produto ON ProdutoOrcamento.produtoId = Produto.IdProduto";

                SqlDataReader reader = cmd.ExecuteReader();

                List<ProdutoOrcamento> lista = new List<ProdutoOrcamento>();

                while(reader.Read())
                {
                    lista.Add(
                        new ProdutoOrcamento {
                            IdPo = (int)reader["IdPo"],
                            IdOrcamento = (int)reader["orcamentoId"],
                            IdProduto = (int)reader["produtoId"],
                            qtd = (int)reader["qtd"],
                            Produto = (string)reader["Produto"]
                        }
                    );
                }

                return lista;
            }
            catch(Exception ex) 
            {
                // Logar os erros (Sentry, App Insights, etc)...
                return null;
            }
            finally
            {
                Dispose();
            }
        }

        public ProdutoOrcamento Read(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM ProdutoOrcamento WHERE IdPv = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new ProdutoOrcamento {
                            IdPo = (int)reader["IdPv"],
                            IdOrcamento = (int)reader["orcamentoId"],
                            IdProduto = (int)reader["produtoId"]
                    };
                }

                return null;
            }
            catch(Exception ex) 
            {
                // Logar os erros (Sentry, App Insights, etc)...
                return null;
            }
            finally
            {
                Dispose();
            }            
        }

        public void Update(int id, ProdutoOrcamento produtoOrcamento)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"UPDATE ProdutoOrcamento 
                    SET orcamentoId = @idOrcamento, produtoId = @idProduto
                    WHERE IdPo = @id";

                cmd.Parameters.AddWithValue("@idOrcamento", produtoOrcamento.IdOrcamento);
                cmd.Parameters.AddWithValue("@idProduto", produtoOrcamento.IdProduto);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                // Logar os erros (Sentry, App Insights, etc)...
            }
            finally
            {
                Dispose();
            }
        }
        public List<ProdutoOrcamento> ReadByOrcamento(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT ProdutoOrcamento.*, Produto.Nome as Produto FROM ProdutoOrcamento JOIN Produto ON Produto.IdProduto = ProdutoOrcamento.produtoId WHERE orcamentoId = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                List<ProdutoOrcamento> lista = new List<ProdutoOrcamento>();

                while(reader.Read())
                {
                    lista.Add(
                        new ProdutoOrcamento {
                            IdPo = (int)reader["IdPo"],
                            IdOrcamento = (int)reader["orcamentoId"],
                            qtd = (int)reader["qtd"],
                            IdProduto = (int)reader["produtoId"]
                        }
                    );
                }

                return lista;
            }
            catch(Exception ex) 
            {
                // Logar os erros (Sentry, App Insights, etc)...
                return null;
            }
            finally
            {
                Dispose();
            }
        }
    }
}