using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

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
                             BrandId = c.BrandId,
                             ColorId = c.ColorId,
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             CarName = c.CarName,
                             MinFindeksScore = c.MinFindeksScore, // Minimum Findeks Puanı eklendi
                             Images = context.CarImages
                                         .Where(ci => ci.CarId == c.CarId)
                                         .Select(ci => ci.ImagePath)
                                         .ToList()
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
                             BrandId = c.BrandId, // BrandId eklendi
                             ColorId = c.ColorId, // ColorId eklendi
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             CarName = c.CarName,
                             MinFindeksScore = c.MinFindeksScore,
                             Images = context.CarImages
                                         .Where(ci => ci.CarId == c.CarId)
                                         .Select(ci => ci.ImagePath)
                                         .ToList()
                         };
            return result.ToList();
        }
    }

    // CarId ile filtreleme yapacak metot
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
                             CarId = c.CarId,
                             BrandId = c.BrandId,
                             ColorId = c.ColorId,
                             CarName = c.CarName,
                             BrandName = b.BrandName,
                             DailyPrice = c.DailyPrice,
                             ColorName = co.ColorName,
                             MinFindeksScore = c.MinFindeksScore, // Minimum Findeks Puanı eklendi
                             Images = context.CarImages
                                         .Where(ci => ci.CarId == c.CarId)
                                         .Select(ci => ci.ImagePath)
                                         .ToList()
                         };
            return result.SingleOrDefault();
        }
    }
}
