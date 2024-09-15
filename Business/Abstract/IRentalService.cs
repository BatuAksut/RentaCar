using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int id);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IResult Add(Rental rental);
        IResult Delete(Rental rental);
        IResult Update(Rental rental);

        // Yeni metot: Belirli bir tarih aralığında aracın müsait olup olmadığını kontrol eder.
        IResult CheckCarAvailability(int carId, DateTime startDate, DateTime endDate);

        // Eski metot: Genel araç müsaitlik kontrolü (belirli bir zamandaki kiralama durumu).
        bool IsAvailable(int carId);
    }
}
