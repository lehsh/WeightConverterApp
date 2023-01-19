using WeightConverterJsonAPI;

namespace WeightConverterApp
{
    public class Messages
    {
        public record AboutMessage(string About);
        public record StatusMessage(string Server, int Port);
        public record InfoMessage(string Handler, string Description);
        public record InfoKnownHosts(string Handler, string Description);
        public record InfoHostRequests(string Handler, string Description);
        public record InfoMessages(List<InfoMessage> List, string ApiFormat);
        public record InfoMeasureOfWeight(string Name, double Value);
        public record BaseHostsMessage(List<string> Ip);
    }
}
