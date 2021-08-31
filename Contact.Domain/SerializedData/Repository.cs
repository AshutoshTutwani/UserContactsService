using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.Domain.Helper;
using Contact.Domain.Interfaces;
using Contact.Domain.Models;

namespace Contact.Domain.SerializedData
{
    public class Repository : IRepository
    {
        #region Methods

        public async Task<(int userId, bool result)> Insert(UserDetail userDetail)
        {
            var usersList = DomainHelper.DeserializeData() ?? new List<UserDetail>();
            var counter = 1;

            if (usersList?.LastOrDefault() != null)
            {
                counter = usersList.LastOrDefault().Id;
            }

            var user = new UserDetail()
            {
                Id = Interlocked.Increment(ref counter),
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                DateOfBirth = userDetail.DateOfBirth,
                Emails = userDetail.Emails,
                PhoneNumbers = userDetail.PhoneNumbers
            };

            usersList.Add(user);

            var result = await Task.Run(() => DomainHelper.SerializeData(usersList));

            return (user.Id, result);
        }

        public async Task<bool> Update(UserDetail userDetail)
        {
            var usersList = DomainHelper.DeserializeData() ?? new List<UserDetail>();

            var userToUpdate = usersList.FirstOrDefault(model => model.Id == userDetail.Id);
            if (userToUpdate == null)
            {
                return await Task.FromResult(false);
            }

            usersList.Remove(userToUpdate);
            usersList.Add(userDetail);
            usersList = usersList.OrderBy(model => model.Id).ToList();
            return await Task.Run(() => DomainHelper.SerializeData(usersList));
        }

        public async Task<List<UserDetail>> GetAll()
        {
            return await Task.Run(DomainHelper.DeserializeData);
        }

        public async Task<bool> Delete(int id)
        {
            var usersList = DomainHelper.DeserializeData() ?? new List<UserDetail>();

            var userToDelete = usersList.FirstOrDefault(model => model.Id == id);
            if (userToDelete == null)
            {
                return false;
            }

            usersList.Remove(userToDelete);
            return await Task.Run(() => DomainHelper.SerializeData(usersList));
        }

        #endregion
    }
}