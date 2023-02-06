using HouseServiceRepositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Repositories
{
    public interface IAdminRepository
    {
        Admin GetAdmin(short id);
        void AddNewService(ServicesTable newService);
    }
}
