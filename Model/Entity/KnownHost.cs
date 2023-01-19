namespace WeightConverterApp.Model.Entity
{
    public class KnownHost
    {
        public int Id { get; set; }
        public string? Ip { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Ip}";
        }
    }
}
