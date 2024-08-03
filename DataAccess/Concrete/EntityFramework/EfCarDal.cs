using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;

public class EfCarDal : EfEntityRepositoryBase<Car, MyDbContext>, ICarDal
{
    // Mevcut GetCarDetails metodu
    public List<CarDetailDto> GetCarDetails()
    {
        using (MyDbContext context = new MyDbContext())
        {
            var result = from c in context.Cars
                         join co in context.Colors on c.ColorId equals co.ColorId
                         join b in context.Brands on c.BrandId equals b.BrandId
                         select new CarDetailDto
                         {
                             CarId = c.CarId,
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             CarName = c.CarName
                         };
            return result.ToList();
        }
    }

    // BrandName ile filtreleme yapacak metot
    public List<CarDetailDto> GetCarDetailsByBrandName(string brandName)
    {
        using (MyDbContext context = new MyDbContext())
        {
            var result = from c in context.Cars
                         join co in context.Colors on c.ColorId equals co.ColorId
                         join b in context.Brands on c.BrandId equals b.BrandId
                         where b.BrandName == brandName
                         select new CarDetailDto
                         {
                             CarId = c.CarId,
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             CarName = c.CarName
                         };
            return result.ToList();
        }
    }
    public CarDetailDto GetCarDetailById(int id)
    {
        using (MyDbContext context = new MyDbContext())
        {
            var result = from c in context.Cars
                         join co in context.Colors on c.ColorId equals co.ColorId
                         join b in context.Brands on c.BrandId equals b.BrandId
                         where c.CarId == id
                         select new CarDetailDto
                         {
                             CarName = c.CarName,
                             BrandName = b.BrandName,
                             DailyPrice = c.DailyPrice,
                             ColorName = co.ColorName,
                             CarId = c.CarId
                         };
            return result.SingleOrDefault();
        }
    }
}
