using teste.Interface;
string select = string.Empty;

while (select != "[red]Finish[/]")
{
	select = MenuActions.InitialMenuInterface();
    
	switch (select)
	{
		case "Add [aqua]User[/]":
			MenuActions.AddNewUserInterface();
			break;
		case "Update [aqua]User[/]":
			break;
		case "Get [aqua]User[/]":
			break;
		case "Delete [aqua]User[/]":
			break;
		case "[red]Finish[/]":
			break;
        default:
			break;
	}
}

