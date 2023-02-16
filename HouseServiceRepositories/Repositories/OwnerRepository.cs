using HouseServiceRepositories.Data;
using HouseServiceRepositories.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HouseServiceRepositories.Repositories
{
    public class OwnerRepository:IOwnerRepository
    {
        HomeServiceDbContext db;
        public OwnerRepository(HomeServiceDbContext context)
        {
            db = context;
        }
        public OwnerRepository() { }    

        public List<OwnerDTO> Getall()
        {
            List<OwnerDTO> ownerDTOList = new();
            
            
            var list = db.Owners.Select(x => new OwnerDTO
            {
                OwnerName = x.OwnerName,
                YearOfExp= x.YearOfExp,
                CostToService = x.CostToService,
                OwnerId= x.OwnerId,
                Service = x.Service.ServiceName,
            }).ToList();
            
            return list ;
        }
        public List<CustomerRequestDTO> GetAllCutomersRequest(short id)
        {
            //CustomersRequest custReq = new CustomersRequest();

            //var req = db.CustomersRequests.Where(x => x.OwnerId == id).ToList();
            var req = db.CustomersRequests.Where(x => x.OwnerId == id).Select(x => new CustomerRequestDTO
            {
                AppointmentId = x.AppointmentId,
                CustomersName = x.Customer.CustomerName,
                CustomersAddress = x.Customer.CustomerAddress,
                DateOfAppointment = x.DateOfAppointment
            }).ToList();
            //ToList();

            return req;
        }

        public Owner AddNewOwner(Owner owner)
        {
            db.Owners.Add(owner);
            db.SaveChanges();
            return owner;
        }
        public void DeleteOwner(short ownerId) {
            try
            {
                Owner owner = db.Owners.Find(ownerId);
                if(owner != null)
                {
                    db.CustomersRequests.RemoveRange(db.CustomersRequests.Where((x) => x.OwnerId == ownerId));
                    db.ServicesLists.RemoveRange(db.ServicesLists.Where((x) => x.OwnerId == ownerId));
                    db.Owners.Remove(owner);
                    db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
        public void UpdateOwner(short ownerId, Owner newDetails)
        {
            try
            {
                Owner owner = db.Owners.Find(ownerId);
                if(owner != null)
                {
                    owner.OwnerName = newDetails.OwnerName;
                    owner.ServiceId= newDetails.ServiceId;
                    owner.CostToService = newDetails.CostToService;
                    owner.YearOfExp= newDetails.YearOfExp;
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
