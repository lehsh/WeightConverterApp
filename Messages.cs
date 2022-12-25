namespace WeightConverterApp
{
    public class Messages
    {
        public record StatusMessage(string Server, int Port);
        public record InfoMessage(string Handler, string Discription);
    }
}
