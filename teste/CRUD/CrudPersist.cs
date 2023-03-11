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

    public bool InsertToDatabase(Users user)
    {
        using (SqlConnection conn = new(ConnectionString))
        {
            SqlCommand cmd = new("INSERT INTO DADOS_PESSOAIS (NOME, SEBRENOME, IDADE, MAIOR_DE_IDADE) VALUES (@NOME, @SOBRENOME, @IDADE, @MAIOR_DE_IDADE)", conn);
            cmd.Parameters.AddWithValue("@NOME", user.Name);
            cmd.Parameters.AddWithValue("@SOBRENOME", user.LastName);
            cmd.Parameters.AddWithValue("@IDADE", user.YearsOld);
            cmd.Parameters.AddWithValue("@MAIOR_DE_IDADE", user.LegalAge);

            conn.Open();

            int affected = cmd.ExecuteNonQuery();

            if (affected > 0)
                return true;
            
            return false;
        }
    }

    public void UpdateToDatabase(Users data)
    {
        using (SqlConnection conn = new(ConnectionString))
        {
            SqlCommand cmd = new("update DADOS_PESSOAIS SET NOME = @nome, SEBRENOME = @sobre_nome, IDADE = @idade, MAIOR_DE_IDADE = @Maior where NOME = @nome)", conn);
            cmd.Parameters.AddWithValue("@nome", data.Name);
            cmd.Parameters.AddWithValue("@sobre_nome", data.LastName);
            cmd.Parameters.AddWithValue("@idade", data.YearsOld);
            cmd.Parameters.AddWithValue("@Maior", data.LegalAge);

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

            List<Users> pessoas = new();
            while (reader.Read())
            {
                Users pessoa = new()
                {
                    Name = (string)reader["NOME"],
                    LastName = (string)reader["SEBRENOME"],
                    YearsOld = (int)reader["IDADE"],
                    LegalAge = (bool)reader["MAIOR_DE_IDADE"]
                };

                pessoas.Add(pessoa);
            }

            return JsonConvert.SerializeObject(pessoas);
        }
    }
}
