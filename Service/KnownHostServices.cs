using WeightConverterApp.Model;
using WeightConverterApp.Model.Entity;

namespace WeightConverterApp.Service
{
    public class KnownHostServices
    {
        private WeightConverterDbContext context;

        public KnownHostServices(WeightConverterDbContext context)
        {
            this.context = context;
        }

        public KnownHost? AddHost(KnownHost host)
        {
            KnownHost? knownHost = context.KnownHosts.FirstOrDefault(o => o.Ip == host.Ip);
            if (knownHost == null)
            {
                context.KnownHosts.Add(host);
                context.SaveChanges();
                return context.KnownHosts.FirstOrDefault(o => o.Ip == host.Ip);
            }
            else
            {
                return knownHost;
            }
        }
    }
}
