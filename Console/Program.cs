using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;


RentalManager rentalManager = new RentalManager(new EfRentalDal());
var rentaladd = rentalManager.Add(new Rental { CarId = 5, CustomerId = 4, RentDate = DateTime.Now });
Console.WriteLine(rentaladd.Message);

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

//CarTest();