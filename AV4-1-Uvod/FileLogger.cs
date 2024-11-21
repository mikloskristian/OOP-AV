internal partial class Program // ALT + ENTER i onda create file for class tako nesto i napravi ovo sam
{
    class FileLogger : ILogger
    {
        private string filename;

        public FileLogger(string filename)
        {
            this.filename = filename;
        }

        public string Name => "FileLogger";

        public void LogError(string message) => LogMessage(message, "ERROR");

        public void LogInfo(string message) => LogMessage(message, "INFO");

        private void LogMessage(string message, string tag)
        {
            using (var writer = new StreamWriter(this.filename, true)) // var je placeholder za neki tip varijable
            {
                writer.WriteLine($"{tag} {DateTime.Now} : {message}");
            }
        }
    }
}