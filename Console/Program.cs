using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;


 static void CarTest()
{
    CarManager carManager = new CarManager(new EfCarDal());
    foreach (var car in carManager.GetCarDetails())
    {
        Console.WriteLine(car.CarName + " / " + car.BrandName);
    }
}

CarTest();