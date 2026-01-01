using Client;

// Entry point for the database client
Console.Write("Please Enter db: ");
var database = Console.ReadLine();

if (string.IsNullOrEmpty(database))
{
    Console.WriteLine("Database name is required");
    return;
}

var client = new DbClient();
client.RunRepl(database);
