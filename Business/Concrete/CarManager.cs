using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using System.Transactions;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Microsoft.Extensions.Configuration;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;
        ICarImageDal _carImageDal;
        string _baseUrl;

        public CarManager(ICarDal carDal,IBrandService brandService,ICarImageDal carImageDal, IConfiguration configuration)
        {
            _carDal = carDal;
            _brandService = brandService;
            _carImageDal = carImageDal;
            _baseUrl = configuration["BaseUrl"];
        }
		[CacheAspect]
		public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.CarsListed);
        }
        public IDataResult<CarDetailDto> GetCarDetailById(int id)
        {
            var car = _carDal.GetCarDetailById(id);
            if (car != null)
            {
                return new SuccessDataResult<CarDetailDto>(car);
            }
            return new ErrorDataResult<CarDetailDto>("Car not found");
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrandName(string brandName)
        {
            var carDetails = _carDal.GetCarDetailsByBrandName(brandName);
            return new SuccessDataResult<List<CarDetailDto>>(carDetails);
        }
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {

            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        [SecuredOperation("car.add,admin")]
		[ValidationAspect(typeof(CarValidator))]
		[CacheRemoveAspect("ICarService.Get")]
		public IResult Add(Car car)
        {
			IResult result = BusinessRules.Run(CheckIfCarCountOfBrandCorrect(car.BrandId),
			CheckIfCarNameExists(car.CarName), CheckIfBrandLimitExceeded());
			if (result != null)
			{
				return result;
			}
			_carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);

        }
		[ValidationAspect(typeof(CarValidator))]
		[CacheRemoveAspect("ICarService.Get")]
		public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessDataResult<List<Car>>(Messages.CarUpdated);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessDataResult<List<Car>>(Messages.CarDeleted);
        }

		[CacheAspect]
		[PerformanceAspect(5)]
		public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(b => b.CarId == id));
        }
        //Business
        private IResult CheckIfCarCountOfBrandCorrect(int brandId)
        {
            var result = _carDal.GetAll(p => p.BrandId == brandId).Count;

            if (result >= 10)
            {
                return new ErrorResult(Messages.CarCountOfBrandError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCarNameExists(string carName)
        {
            var result = _carDal.GetAll(p => p.CarName == carName).Any();

            if (result)
            {
                return new ErrorResult(Messages.CarNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfBrandLimitExceeded()
        {
            var result = _brandService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.BrandLimitExceeded);
            }
            return new SuccessResult();
        }

		[TransactionScopeAspect]
		public IResult TransactionalOperation(Car car)
		{
			_carDal.Update(car);
			_carDal.Add(car);
			return new SuccessResult(Messages.CarUpdated);
		}

        public CarDetailDto GetCarDetailByIdWithImages(int id)
        {
            var car = _carDal.GetCarDetailById(id);
            if (car == null)
                return null;

            var carImages = _carImageDal.GetImagesByCarId(id);

            var imageUrls = carImages.Select(img => $"{_baseUrl}uploads/{Path.GetFileName(img.ImagePath)}").ToList();

            return new CarDetailDto
            {
                CarId = car.CarId,
                CarName = car.CarName,
                BrandName = car.BrandName,
                DailyPrice = car.DailyPrice,
                ColorName = car.ColorName,
                Images = imageUrls
            };
        }

    }
}
