internal partial class Program
{
    interface ILogger
    {
        string Name { get; } //Ovo je po defaultu javno

        void LogInfo(string message);

        void LogError(string message);
    }
}