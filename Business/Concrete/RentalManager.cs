using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            // Araç müsait mi kontrolü
            bool carCheck = IsAvailable(rental.CarId);
            if (carCheck)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.RentalAdded);
            }
            return new ErrorResult(Messages.CarInvalid);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.RentalListed);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        // Belirli bir tarih aralığında araç müsait mi kontrolü
        public IResult CheckCarAvailability(int carId, DateTime startDate, DateTime endDate)
        {
            var rentals = _rentalDal.GetAll(r => r.CarId == carId &&
                                               (r.RentDate < endDate && r.ReturnDate > startDate));
            if (rentals.Any())
            {
                return new ErrorResult("Araç bu tarihlerde kiralanamaz.");
            }
            return new SuccessResult("Araç uygun.");
        }

        // Mevcut: Araç şu anda kirada mı kontrolü
        public bool IsAvailable(int carId)
        {
            var rentals = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate == null);
            return !rentals.Any(); // ReturnDate null olan bir kiralama varsa araç müsait değil
        }
    }
}
