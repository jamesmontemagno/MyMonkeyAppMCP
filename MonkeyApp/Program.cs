using MonkeyApp;

void ShowMenu()
{
    Console.WriteLine("\nMonkey App Menu:");
    Console.WriteLine("1. List all monkeys");
    Console.WriteLine("2. Get details for a specific monkey");
    Console.WriteLine("3. Get a random monkey");
    Console.WriteLine("4. Exit");
    Console.Write("Choose an option: ");
}

void PrintAsciiMonkey()
{
    Console.WriteLine("  //\\ ");
    Console.WriteLine(" (o o)");
    Console.WriteLine("(  V  )");
    Console.WriteLine("--m-m--");
}

while (true)
{
    ShowMenu();
    var input = Console.ReadLine();
    Console.WriteLine();
    switch (input)
    {
        case "1":
            Console.WriteLine("Monkeys:");
            foreach (var m in MonkeyHelper.GetMonkeys())
                Console.WriteLine("- " + m.Name);
            break;
        case "2":
            Console.Write("Enter monkey name: ");
            var name = Console.ReadLine();
            var monkey = MonkeyHelper.GetMonkeyByName(name ?? "");
            if (monkey != null)
            {
                PrintAsciiMonkey();
                Console.WriteLine($"Name: {monkey.Name}\nLocation: {monkey.Location}\nDetails: {monkey.Details}\nPopulation: {monkey.Population}\nLat/Lon: {monkey.Latitude}, {monkey.Longitude}\nImage: {monkey.Image}");
                Console.WriteLine($"Accessed: {MonkeyHelper.GetMonkeyAccessCount(monkey.Name)} times");
            }
            else
            {
                Console.WriteLine("Monkey not found.");
            }
            break;
        case "3":
            var randomMonkey = MonkeyHelper.GetRandomMonkey();
            if (randomMonkey != null)
            {
                PrintAsciiMonkey();
                Console.WriteLine($"Random Monkey: {randomMonkey.Name}\nLocation: {randomMonkey.Location}\nDetails: {randomMonkey.Details}\nPopulation: {randomMonkey.Population}\nLat/Lon: {randomMonkey.Latitude}, {randomMonkey.Longitude}\nImage: {randomMonkey.Image}");
                Console.WriteLine($"Accessed: {MonkeyHelper.GetMonkeyAccessCount(randomMonkey.Name)} times");
            }
            else
            {
                Console.WriteLine("No monkeys available.");
            }
            break;
        case "4":
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}
