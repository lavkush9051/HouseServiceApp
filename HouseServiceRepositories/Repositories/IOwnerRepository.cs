using HouseServiceRepositories.Data;
using HouseServiceRepositories.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Repositories
{
    public interface IOwnerRepository
    {
        List<OwnerDTO> Getall();
        List<CustomerRequestDTO> GetAllCutomersRequest(short id);
        Owner AddNewOwner( Owner newOwner);
        void UpdateOwner(short ownerId, Owner newDetails);
        void DeleteOwner(short ownerId);

    }
}
