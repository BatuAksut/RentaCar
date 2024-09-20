using Core.Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        User GetByMail(string email);
        List<User> GetAll(); // Yeni metod
        User GetUserById(int userId);
        void UpdateUser(UserForUpdateDto userForUpdateDto, int userId);
    }
}
