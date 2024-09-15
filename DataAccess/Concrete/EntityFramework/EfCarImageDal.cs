using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImageDal:EfEntityRepositoryBase<CarImage,MyDbContext>, ICarImageDal
    {
        public List<CarImage> GetImagesByCarId(int carId)
        {
            using (MyDbContext context = new MyDbContext())
            {
                return context.CarImages.Where(img => img.CarId == carId).ToList();
            }
        }
    }
}
