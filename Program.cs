using HtmlAgilityPack;
using System.Net;
using System.Net.Mail;

var web = new HtmlWeb();
var doc = web.Load("https://www.basketball-reference.com/boxscores/");

var numberOfGames = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[2]/h2");
var gamesDate = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/h1");
var nbaGames = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[3]/div");
int i = 0;

string results = "<html><body>";
results += $"<h1>{gamesDate.First().InnerText}</h1>";
results += $"<h2>{numberOfGames.First().InnerText}</h2>";

// Console.WriteLine(gamesDate.First().InnerText + "\n");
// Console.WriteLine(numberOfGames.First().InnerText + "\n");

foreach (var nbaGame in nbaGames)
{
    var gameTables = nbaGame.SelectNodes(".//table");
    i++;
    results += $"<h3>Game: {i}</h3>";

    // Console.WriteLine($"Game : {i}");
    foreach (var table in gameTables)
    {
        results += "<table style='border: 1px solid black; border-collapse: collapse;'>";
        var rows = table.SelectNodes(".//tbody/tr");

        foreach (var row in rows)
        {
            results += "<tr>";
            var cells = row.SelectNodes(".//td");
            foreach (var cell in cells)
            {
                // Console.Write(cell.InnerText.Replace("&nbsp;", " ").Trim().PadRight(15));
                results += $"<td style='padding: 5px;'>{cell.InnerText.Replace("&nbsp;", " ").Trim()}</td>";
            }
            // Console.WriteLine();
            results += "</tr>";
        }
        // Console.WriteLine();
        results += "</table><br/>";
    }
    // Console.WriteLine("\n");
}
results += "</body></html>";
// Console.WriteLine(results);

string smtpAddress = "smtp.gmail.com";
int portNumber = 587;
bool enableSSL = true;
string emailFromAddress = "myemail@gmail.com"; //Sender Email Address
//Sender Password - its diferent from the main email password - read README.md
//Have to create an specific app password
string password = "appPassword"; 
string emailToAddress = "receiveremail@gmail.com"; //Receiver Email Address  
string subject = "NBA Games";
string body = results;

SendEmail();

void SendEmail()
{
    using (MailMessage mail = new MailMessage())
    {
        mail.From = new MailAddress(emailFromAddress);
        mail.To.Add(emailToAddress);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;
        //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
        {
            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
            smtp.EnableSsl = enableSSL;
            smtp.Send(mail);
        }
    }
}


