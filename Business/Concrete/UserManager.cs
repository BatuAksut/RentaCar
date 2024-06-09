using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal userDal;

        public UserManager(IUserDal userDal)
        {
            this.userDal = userDal;
        }

        public IResult Add(User user)
        {
            if (user.FirstName.Length < 2)
            {
                return new ErrorResult(Messages.ColorNameInvalid);
            }
            userDal.Add(user);
            return new SuccessResult(Messages.ColorAdded);
        }

        public IResult Delete(User user)
        {
            userDal.Delete(user);
            return new SuccessResult(Messages.ColorDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(userDal.GetAll(), Messages.ColorsListed);

        }

        public IDataResult<User> GetUserById(int id)
        {
            return new SuccessDataResult<User>(userDal.Get(u => u.Id == id));
        }

        public IResult Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
