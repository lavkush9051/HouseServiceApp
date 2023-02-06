using HouseServiceRepositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Repositories
{
    public interface IServiceRepository
    {
        List<ServicesTable> GetAllService();
        void AddService(ServicesTable service);
        ServicesTable? UpdateService(short id, ServicesTable newService);
        void DeleteService(short id);
    }
}
