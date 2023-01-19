namespace WeightConverterApp.Model.Entity
{
    public class Request
    {
        public int Id { get; set; }
        public string? Body { get; set; }
        public int HostId { get; set; }
        public KnownHost? Host { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Body}";
        }
    }
}
