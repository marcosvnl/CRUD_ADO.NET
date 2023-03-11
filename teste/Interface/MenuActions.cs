using AutoMapper;
using Spectre.Console;
using teste.CRUD;
using teste.DTO;
using teste.Entity;
using teste.Profiles;

namespace teste.Interface;

internal static class MenuActions
{
    public static string InitialMenuInterface()
    {
        
        var selected = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Selecione a [blue]opção desejada[/].")
            .PageSize(10)
            .MoreChoicesText("Mova o curso para a opção [green]desejada[/].")
            .AddChoices(new[]
            {
                "Update [aqua]User[/]", "Add [aqua]User[/]", "Get [aqua]User[/]", "Delete [aqua]User[/]", "[red]Finish[/]"
            }));

        return selected;
    }

    public static void AddNewUserInterface()
    {
        UsersDTO userDTO = new();
        CrudPersist crud = new();
        MapProfiles profiles = new();
        
        userDTO.Nome = AnsiConsole.Ask<string>("[maroon]1.[/] Your name:");
        userDTO.SobreNome = AnsiConsole.Ask<string>("[maroon]2.[/] Last name:");
        userDTO.Idade = AnsiConsole.Ask<int>("[maroon]3.[/] What're your years old:");
        userDTO.MaiorIdade = userDTO.Idade >= 18;

        var user = profiles.ConfigMapper.Map<UsersDTO, Users>(userDTO);

        Table table = new();
        table.Border(TableBorder.Rounded);
        table.AddColumn("Questions");
        table.AddColumn(new TableColumn("Data").Centered());
        table.AddRow("[olive]Your name[/]", userDTO.Nome);
        table.AddRow("[olive]Last name[/]", userDTO.SobreNome);
        table.AddRow("[olive]Years old[/]", userDTO.Idade.ToString());
        table.AddRow("[olive]Legal Age[/]", userDTO.MaiorIdade ? "YAS" : "NO");

        AnsiConsole.Write(table);

        if (!AnsiConsole.Confirm("Data is correct?"))
        {
            AnsiConsole.MarkupLine("The data not saved, press [blue]<Enter>[/]");
            InitialMenuInterface();
        }

        if (crud.InsertToDatabase(user))
        {
            AnsiConsole.WriteLine("Data saved");
            AnsiConsole.MarkupLine("Press [blue]<Enter>[/]");
            Console.Clear();
            InitialMenuInterface();
        }           
    }
}
