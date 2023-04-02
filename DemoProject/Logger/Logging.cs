namespace DemoProject.Logger
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if(type == "ERROR")
            {
                Console.WriteLine($"Error message {message}");
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
