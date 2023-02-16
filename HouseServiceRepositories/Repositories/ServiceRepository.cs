using HouseServiceRepositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        HomeServiceDbContext db;
        public ServiceRepository(HomeServiceDbContext context) {
            db = context;
        }
        public ServiceRepository() {
        
        }
        //get all service
        public List<ServicesTable> GetAllService()
        {
            return db.ServicesTables.ToList();
        }

        //add new service
        public void AddService(ServicesTable service)
        {
            db.ServicesTables.Add(service);
            db.SaveChanges();
        }

        //update service
        public ServicesTable? UpdateService(short id, ServicesTable newService)
        {
            var service = db.ServicesTables.Find(id);
            if (service != null)
            {
                service.ServiceName = newService.ServiceName;
                db.SaveChanges();
                return newService;
            }
            else
            {
                return null;
            }
        }

        public void DeleteService(short id)
        {
            try
            {
                var service = db.ServicesTables.Find(id);
                if (service != null)
                {
                    //var owner = db.Owners.Update(db)
                    db.ServicesTables.Remove(service);
                    db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }



    }
}
