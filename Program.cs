using HtmlAgilityPack;

var web = new HtmlWeb();
var doc = web.Load("https://www.basketball-reference.com/boxscores/?month=12&day=8&year=2024");

var numberOfGames = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/h2");
var gamesDate = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/h1");
var nbaGames = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[3]/div");
int i = 0;

Console.WriteLine(gamesDate.First().InnerText + "\n");
Console.WriteLine(numberOfGames.First().InnerText + "\n");

foreach (var nbaGame in nbaGames)
{
    var game = nbaGame.SelectNodes(".//table");
    
    i++;
    Console.WriteLine($"Game : {i}");

    foreach (var table in game)
    {
        var rows = table.SelectNodes(".//tbody/tr");
        foreach (var row in rows)
        {
            var cells = row.SelectNodes(".//td");
            foreach (var cell in cells)
            {
                Console.Write(cell.InnerText.Replace("&nbsp;", " ").Trim().PadRight(15));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    Console.WriteLine("\n");
}

// var tables = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[3]/div/table");

// foreach (var table in tables)
// {
//     var rows = table.SelectNodes(".//tbody/tr");
//     foreach (var row in rows)
//     {
//         var cells = row.SelectNodes(".//td");
//         foreach (var cell in cells)
//         {
//             Console.Write(cell.InnerText.Replace("&nbsp;", " ").Trim().PadRight(15));
//         }
//         Console.WriteLine();
//     }
//     Console.WriteLine();
// }


