using HouseServiceRepositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Repositories
{
    public class AdminRepository:IAdminRepository
    {
        HomeServiceDbContext db;
        public AdminRepository(HomeServiceDbContext context)
        {
            db = context;
        }

        public AdminRepository() { }

        public Admin GetAdmin(short id)
        {
            Admin admin = db.Admin.Find(id);
            return admin;
        }

        public void AddNewService(ServicesTable newService)
        {
            db.ServicesTables.Add(newService);
            db.SaveChanges();
            
            
        }
    }
}
