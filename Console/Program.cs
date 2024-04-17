using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;




static void CarTest()
{
    CarManager carManager = new CarManager(new EfCarDal());
    var result = carManager.GetCarDetails();
    if (result.Success){
        foreach (var car in result.Data)
        {
            Console.WriteLine(car.CarName + " / " + car.BrandName);
        }
    }
    
    else {
        Console.WriteLine(result.Message);
    }
}

CarTest();