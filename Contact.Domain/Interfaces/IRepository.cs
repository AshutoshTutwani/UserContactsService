using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contact.Domain.Models;

namespace Contact.Domain.Interfaces
{
    public interface IRepository
    {
        Task<(int userId, bool result)> Insert(UserDetail userDetail);
        Task<bool> Update(UserDetail userDetail);
        Task<List<UserDetail>> GetAll();
        Task<bool> Delete(int id);
    }
}
