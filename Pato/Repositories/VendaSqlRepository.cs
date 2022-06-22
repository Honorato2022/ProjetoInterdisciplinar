using System.Data.SqlClient;
using Pato.Models;

namespace Pato.Repositories
{
    public class VendaSqlRepository : DBContext, IVendaRepository
    {
        public void Create(Venda venda)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"INSERT INTO Venda 
                    VALUES (@clienteId, @data, @valorLiquido, @valorBruto, @desconto)";

                cmd.Parameters.AddWithValue("@clienteId", venda.IdPessoa);
                cmd.Parameters.AddWithValue("@data", venda.Data);
                cmd.Parameters.AddWithValue("@valorLiquido", venda.valorLiquido);
                cmd.Parameters.AddWithValue("@valorBruto", venda.valorBruto);
                cmd.Parameters.AddWithValue("@desconto", venda.Desconto);

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
                cmd.CommandText = @"DELETE FROM Venda WHERE IdVenda = @id";

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

        public List<Venda> Read()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM Venda";

                SqlDataReader reader = cmd.ExecuteReader();

                List<Venda> lista = new List<Venda>();

                while(reader.Read())
                {
                    lista.Add(
                        new Venda {
                            IdVenda = (int)reader["IdVenda"],
                            Data = (DateTime)reader["Data"],
                            valorLiquido = (decimal)reader["valorLiquido"],
                            valorBruto = (decimal)reader["valorBruto"],
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

        public Venda Read(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT Venda.*, Produto.Nome, Produto.Valor, ProdutoVenda.produtoId FROM Venda JOIN ProdutoVenda ON ProdutoVenda.vendaId = Venda.IdVenda JOIN Produto ON Produto.IdProduto = ProdutoVenda.produtoId WHERE IdVenda = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Venda {
                            IdVenda = (int)reader["IdVenda"],
                            Data = (DateTime)reader["Data"],
                            valorLiquido = (decimal)reader["valorLiquido"],
                            valorBruto = (decimal)reader["valorBruto"],
                            Desconto = (decimal)reader["Desconto"],
                            Produto = (string)reader["Nome"],
                            ValorUnit = (decimal)reader["Valor"]
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

        public void Update(int id, Venda venda)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"UPDATE Venda
                    SET clienteId = @idPessoa, Data = @data, valorLiquido = @valorLiquido, valorBruto = @valorBruto, Desconto = @desconto
                    WHERE IdVenda = @id";

                cmd.Parameters.AddWithValue("@idPessoa", venda.IdPessoa);
                cmd.Parameters.AddWithValue("@data", venda.Data);
                cmd.Parameters.AddWithValue("@valorLiquido", venda.valorLiquido);
                cmd.Parameters.AddWithValue("@valorBruto", venda.valorBruto);
                cmd.Parameters.AddWithValue("@desconto", venda.Desconto);
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
        public List<Venda> ReadByCliente(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT * FROM Venda WHERE clienteId = @id";

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                List<Venda> lista = new List<Venda>();

                while(reader.Read())
                {
                    lista.Add(
                        new Venda {
                            IdVenda = (int)reader["IdVenda"],
                            Data = (DateTime)reader["Data"],
                            valorLiquido = (decimal)reader["valorLiquido"],
                            valorBruto = (decimal)reader["valorBruto"],
                            Desconto = (decimal)reader["Desconto"],
                            Cliente = (string)reader["Cliente"]
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