/*
  - Sučelja se može naslijediti koliko god hoces, naming convention da je prvo slovo veliko I kao Interface i onda glagol (IComparable, IFlyable...)
  - TODO - Dovrsit komentare, napravit GIT repo za sve ovo
 
 */

internal partial class Program
{
    static void Main(string[] args)
    {
        //ILogger logger = new ILogger();
        ILogger logger = new ConsoleLogger();
        Console.WriteLine(logger.Name);
        logger.LogError("System32 deleted successfully!");
        logger.LogInfo("Hello Everyone");

        logger = new FileLogger("Log.txt");
        Console.WriteLine(logger.Name);
        logger.LogError("System32 deleted successfully!");
        logger.LogInfo("Hello Everyone");

        ILogger[] loggers = new ILogger[] //ovo je legalno jer je ovo polje puno referenci a ne objekt
        {
            new ConsoleLogger(),
            new FileLogger("myLog.txt")
        };

        foreach (var logs in loggers)
        {
            logs.LogError("System32 deleted successfully!");
            logs.LogInfo("Hello Everyone");
        }

    }
}