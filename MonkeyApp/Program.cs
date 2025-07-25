using MonkeyApp;
using Spectre.Console;

// Display welcome banner
AnsiConsole.Write(
    new FigletText("MonkeyApp")
        .LeftJustified()
        .Color(Color.Green));

AnsiConsole.WriteLine();
AnsiConsole.Write(new Markup("[bold yellow]Welcome to the Enhanced Monkey Application![/]"));
AnsiConsole.WriteLine();
AnsiConsole.WriteLine();

void PrintAsciiMonkey()
{
    var monkey = new Markup("[yellow]  //\\[/] \n[yellow] (o o)[/]\n[yellow](  V  )[/]\n[yellow]--m-m--[/]");
    AnsiConsole.Write(monkey);
    AnsiConsole.WriteLine();
}

void ShowMonkeyList()
{
    var monkeys = MonkeyHelper.GetMonkeys();
    
    var table = new Table();
    table.AddColumn("Name");
    table.AddColumn("Location");
    table.AddColumn("Population");
    table.AddColumn("Access Count");
    
    foreach (var monkey in monkeys)
    {
        table.AddRow(
            monkey.Name,
            monkey.Location,
            monkey.Population.ToString("N0"),
            MonkeyHelper.GetMonkeyAccessCount(monkey.Name).ToString());
    }
    
    AnsiConsole.Write(table);
}

void ShowMonkeyDetails(Monkey monkey)
{
    var panel = new Panel(new Markup($"""
        [bold green]Name:[/] {monkey.Name}
        [bold blue]Location:[/] {monkey.Location}
        [bold cyan]Population:[/] {monkey.Population:N0}
        [bold magenta]Coordinates:[/] {monkey.Latitude:F6}, {monkey.Longitude:F6}
        [bold yellow]Access Count:[/] {MonkeyHelper.GetMonkeyAccessCount(monkey.Name)}
        
        [bold white]Details:[/]
        {monkey.Details}
        
        [bold gray]Image URL:[/] [link]{monkey.Image}[/]
        """))
        .Header($"[bold green]{monkey.Name}[/]")
        .Border(BoxBorder.Rounded)
        .BorderColor(Color.Green);
    
    PrintAsciiMonkey();
    AnsiConsole.Write(panel);
}

void SearchMonkeys()
{
    AnsiConsole.Markup("Enter [green]monkey name[/] to search for: ");
    var searchTerm = Console.ReadLine();
    
    if (string.IsNullOrWhiteSpace(searchTerm))
    {
        AnsiConsole.MarkupLine("[red]Search term cannot be empty![/]");
        return;
    }
    
    var monkey = MonkeyHelper.GetMonkeyByName(searchTerm);
    if (monkey != null)
    {
        AnsiConsole.MarkupLine($"[green]Found monkey:[/] {monkey.Name}");
        AnsiConsole.WriteLine();
        ShowMonkeyDetails(monkey);
    }
    else
    {
        AnsiConsole.MarkupLine($"[red]No monkey found with name '{searchTerm}'[/]");
        
        // Suggest similar names
        var allMonkeys = MonkeyHelper.GetMonkeys();
        var suggestions = allMonkeys
            .Where(m => m.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .Take(3)
            .ToList();
            
        if (suggestions.Any())
        {
            AnsiConsole.MarkupLine("[yellow]Did you mean:[/]");
            foreach (var suggestion in suggestions)
            {
                AnsiConsole.MarkupLine($"  • [cyan]{suggestion.Name}[/]");
            }
        }
    }
}

void GetRandomMonkey()
{
    AnsiConsole.Status()
        .Start("Finding a random monkey...", ctx =>
        {
            ctx.Spinner(Spinner.Known.Star);
            ctx.SpinnerStyle(Style.Parse("green"));
            
            // Simulate some work
            Thread.Sleep(1000);
        });
    
    var randomMonkey = MonkeyHelper.GetRandomMonkey();
    if (randomMonkey != null)
    {
        AnsiConsole.MarkupLine("[green]Here's your random monkey:[/]");
        AnsiConsole.WriteLine();
        ShowMonkeyDetails(randomMonkey);
    }
    else
    {
        AnsiConsole.MarkupLine("[red]No monkeys available![/]");
    }
}

void AddNewMonkey()
{
    AnsiConsole.MarkupLine("[bold yellow]Add New Monkey[/]");
    AnsiConsole.WriteLine();
    
    AnsiConsole.Markup("Enter [green]monkey name[/]: ");
    var name = Console.ReadLine() ?? "";
    
    AnsiConsole.Markup("Enter [green]location[/]: ");
    var location = Console.ReadLine() ?? "";
    
    AnsiConsole.Markup("Enter [green]details[/]: ");
    var details = Console.ReadLine() ?? "";
    
    AnsiConsole.Markup("Enter [green]population[/]: ");
    var populationStr = Console.ReadLine() ?? "0";
    if (!int.TryParse(populationStr, out var population))
        population = 0;
    
    AnsiConsole.Markup("Enter [green]latitude[/]: ");
    var latStr = Console.ReadLine() ?? "0";
    if (!double.TryParse(latStr, out var latitude))
        latitude = 0.0;
    
    AnsiConsole.Markup("Enter [green]longitude[/]: ");
    var lonStr = Console.ReadLine() ?? "0";
    if (!double.TryParse(lonStr, out var longitude))
        longitude = 0.0;
    
    AnsiConsole.Markup("Enter [green]image URL[/]: ");
    var image = Console.ReadLine() ?? "";
    
    var newMonkey = new Monkey
    {
        Name = name,
        Location = location,
        Details = details,
        Population = population,
        Latitude = latitude,
        Longitude = longitude,
        Image = image
    };
    
    // For this demo, we'll just show what would be added
    // In a real application, you'd add this to persistent storage
    AnsiConsole.MarkupLine("[green]✓ Monkey would be added successfully![/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[yellow]Preview of new monkey:[/]");
    ShowMonkeyDetails(newMonkey);
    AnsiConsole.MarkupLine("[gray]Note: This is a demo - the monkey is not actually saved to persistent storage.[/]");
}

void ShowMenu()
{
    AnsiConsole.WriteLine();
    var rule = new Rule("[yellow]Main Menu[/]")
    {
        Style = Style.Parse("yellow")
    };
    AnsiConsole.Write(rule);
    AnsiConsole.WriteLine();
    
    AnsiConsole.MarkupLine("1. [cyan]📋 View all monkeys in table[/]");
    AnsiConsole.MarkupLine("2. [green]🔍 Search for a specific monkey[/]");
    AnsiConsole.MarkupLine("3. [magenta]🎲 Get a random monkey[/]");
    AnsiConsole.MarkupLine("4. [yellow]➕ Add a new monkey[/]");
    AnsiConsole.MarkupLine("5. [red]🚪 Exit[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.Markup("[bold]Choose an option (1-5): [/]");
}

// Main application loop
while (true)
{
    ShowMenu();
    var input = Console.ReadLine();
    AnsiConsole.WriteLine();
    
    switch (input)
    {
        case "1":
            AnsiConsole.MarkupLine("[bold cyan]All Monkeys:[/]");
            AnsiConsole.WriteLine();
            ShowMonkeyList();
            break;
            
        case "2":
            SearchMonkeys();
            break;
            
        case "3":
            GetRandomMonkey();
            break;
            
        case "4":
            AddNewMonkey();
            break;
            
        case "5":
            AnsiConsole.Write(
                new FigletText("Goodbye!")
                    .Centered()
                    .Color(Color.Red));
            return;
            
        default:
            AnsiConsole.MarkupLine("[red]Invalid option. Please choose 1-5.[/]");
            break;
    }
    
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[gray]Press any key to continue...[/]");
    Console.ReadKey(true);
    AnsiConsole.WriteLine();
}
