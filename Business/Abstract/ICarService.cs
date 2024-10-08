﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetById(int id);
        IDataResult<List<Car>> GetCarsByBrandId(int id);

        IDataResult<List<Car>> GetCarsByColorId(int id);
        IDataResult<CarDetailDto> GetCarDetailById(int id);
        CarDetailDto GetCarDetailByIdWithImages(int id);

        public IDataResult<List<CarDetailDto>> GetCarsByBrandName(string brandName);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<List<CarDetailDto>> GetCarDetails();
		public IResult TransactionalOperation(Car car);
	}
}
