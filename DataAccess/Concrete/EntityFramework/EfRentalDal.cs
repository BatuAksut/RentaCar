using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,MyDbContext>,IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (MyDbContext context = new MyDbContext())
            {
                var result = from r in context.Rentals
                             join c in context.Customers
                             on r.CustomerId equals c.UserId
                             join ca in context.Cars
                             on r.CarId equals ca.CarId


                             select new RentalDetailDto
                             {
                                 CarName = ca.CarName,
                                 CustomerName=c.CompanyName,
                             };
                return result.ToList();

            }
        }
    }
}
