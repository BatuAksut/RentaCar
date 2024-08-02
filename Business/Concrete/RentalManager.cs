using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rental)
        {
            _rentalDal = rental;
        }

        public IResult Add(Rental rental)
        {
            bool carCheck = IsAvailable(rental.CarId);
            if(carCheck)
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
            throw new NotImplementedException();
        }

        public bool IsAvailable(int carId)
        {
            var rentals = _rentalDal.GetAll();
            bool isRented = rentals.Any(Car => Car.Id == carId);
            if (isRented)
            {
                bool isReturnEmpty = rentals.Any(Car => Car.Id == carId && Car.ReturnDate == null);
                if (isReturnEmpty)
                {
                    return false;
                }
            }
            return true;

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
    }
}
