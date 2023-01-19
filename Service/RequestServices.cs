using WeightConverterApp.Model.Entity;
using WeightConverterApp.Model;
using Microsoft.Extensions.Hosting;

namespace WeightConverterApp.Service
{
    public class RequestServices
    {
        private WeightConverterDbContext context;

        public RequestServices(WeightConverterDbContext context)
        {
            this.context = context;
        }

        public Request AddRequest(Request request)
        {
            context.Requests.Add(request);
            context.SaveChanges();
            return request;
        }
    }
}
