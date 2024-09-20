using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public List<User> GetAll() // Yeni metod
        {
            return _userDal.GetAll(); // DataAccess katmanından tüm kullanıcıları al
        }

        public void UpdateUser(UserForUpdateDto userForUpdateDto, int userId)
        {
            var existingUser = _userDal.Get(u => u.Id == userId);
            if (existingUser != null)
            {
                existingUser.FirstName = userForUpdateDto.FirstName;
                existingUser.LastName = userForUpdateDto.LastName;
              
                _userDal.Update(existingUser);
            }
            else
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
        }
        public User GetUserById(int userId)
        {
            return _userDal.Get(u => u.Id == userId);
        }




    }
}
