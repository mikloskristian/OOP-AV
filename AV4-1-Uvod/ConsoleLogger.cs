internal partial class Program
{
    class ConsoleLogger : ILogger
    {
        public string Name { get { return "Console Logger"; } } //public string Name => "ConsoleLogger"; **ISTA STVAR** takozvano expression body property

        public void LogError(string message) => LogMessage(message, ConsoleColor.Red);

        public void LogInfo(string message) => LogMessage(message, ConsoleColor.Green);

        private void LogMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{DateTime.Now} : {message}");
            Console.ResetColor();
        }
    }
}