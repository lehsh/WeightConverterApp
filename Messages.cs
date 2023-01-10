namespace WeightConverterApp
{
    public class Messages
    {
        public record AboutMessage(string About);
        public record StatusMessage(string Server, int Port);
        public record InfoMessage(string Handler, string Description);
        public record InfoMessages(List<InfoMessage> List);
    }
}
