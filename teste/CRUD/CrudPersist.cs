using Newtonsoft.Json;
using System.Data.SqlClient;
using teste.Entity;

namespace teste.CRUD;

public class CrudPersist
{
    private string ConnectionString
    {
        get
        {
            return "Server=ACT-4077\\SQLEXPRESS;Database=USERS;Trusted_Connection=True;";
        }
    }

    public void InsertToDatabase(string nome, string sobreNome, int idade, bool maiorDeIdade = false)
    {
        using (SqlConnection conn = new(ConnectionString))
        {
            SqlCommand cmd = new("INSERT INTO DADOS_PESSOAIS (NOME, SEBRENOME, IDADE, MAIOR_DE_IDADE) VALUES (@NOME, @SOBRENOME, @IDADE, @MAIOR_DE_IDADE)", conn);
            cmd.Parameters.AddWithValue("@NOME", nome);
            cmd.Parameters.AddWithValue("@SOBRENOME", sobreNome);
            cmd.Parameters.AddWithValue("@IDADE", idade);
            cmd.Parameters.AddWithValue("@MAIOR_DE_IDADE", maiorDeIdade);

            conn.Open();

            int affected = cmd.ExecuteNonQuery();

            if (affected > 0)
                Console.WriteLine("Salvo com sucesso.");
        }
    }

    public void UpdateToDatabase(Pessoas data)
    {
        using (SqlConnection conn = new(ConnectionString))
        {
            SqlCommand cmd = new("update DADOS_PESSOAIS SET NOME = @nome, SEBRENOME = @sobre_nome, IDADE = @idade, MAIOR_DE_IDADE = @Maior where NOME = @nome)", conn);
            cmd.Parameters.AddWithValue("@nome", data.Nome);
            cmd.Parameters.AddWithValue("@sobre_nome", data.SobreNome);
            cmd.Parameters.AddWithValue("@idade", data.Idade);
            cmd.Parameters.AddWithValue("@Maior", data.Maior);

            conn.Open();
            int affected = cmd.ExecuteNonQuery();

            if (affected > 0)
                Console.WriteLine("Alterado com sucesso");

        }
    }

    public void DeleteFromDatabase(string nome)
    {
        using (SqlConnection conn = new(ConnectionString))
        {
            SqlCommand cmd = new(@"DELETE FROM DADOS_PESSOAIS WHERE NOME = @NOME", conn);
            cmd.Parameters.AddWithValue("@NOME", nome);

            conn.Open();
            int affected = cmd.ExecuteNonQuery();

            if (affected > 0)
                Console.WriteLine($"Usuário {nome} foi deletado.");
        }
    }

    public string GetAllPeaple()
    {
        using (SqlConnection conn = new(ConnectionString))
        {
            SqlCommand cmd = new("SELECT * FROM DADOS_PESSOAIS", conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Pessoas> pessoas = new();
            while (reader.Read())
            {
                Pessoas pessoa = new()
                {
                    Nome = (string)reader["NOME"],
                    SobreNome = (string)reader["SEBRENOME"],
                    Idade = (int)reader["IDADE"],
                    Maior = (bool)reader["MAIOR_DE_IDADE"]
                };

                pessoas.Add(pessoa);
            }

            return JsonConvert.SerializeObject(pessoas);
        }
    }
}
