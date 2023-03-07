//CRUD NA TABELA USERS

using teste.CRUD;

string? nome = string.Empty;
string? sobreNome = string.Empty;
int idade = 0;
bool maior = false;

Console.WriteLine("Digite o seu nome:");
nome = Console.ReadLine();

if (string.IsNullOrEmpty(nome))
{
    Console.WriteLine("Você precisa digitar o seu nome:");
    nome = Console.ReadLine();
}

Console.WriteLine("Digite o seu sobrenome");
sobreNome = Console.ReadLine();

Console.WriteLine("Digite sua idade");
idade = Convert.ToInt32(Console.ReadLine());

//fazer a logica para saber se é maior de idade
CrudPersist crud = new();
crud.InsertToDatabase(nome!, sobreNome!, idade);
Console.WriteLine(crud.GetAllPeaple());
