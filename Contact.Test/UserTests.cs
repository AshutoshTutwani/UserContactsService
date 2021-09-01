using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contact.API.Controllers;
using Contact.Domain.Helper;
using Contact.Domain.Models;
using Contact.Domain.SerializedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Contact.Test
{
    public class Tests
    {
        private UserDetail _testUser;

        [SetUp]
        public void Setup()
        {
            DomainHelper.JsonFileName = "user_TestData.json";
            this._testUser = new UserDetail()
            {
                FirstName = "Ashutosh",
                LastName = "Tutwani",
                DateOfBirth = "31/10/1992",
            };

            _testUser.Emails.Add("ashutoshtutwani@gmail.com");
            _testUser.Emails.Add("ashututwani@gmail.com");

            _testUser.PhoneNumbers.Add("8368011119");
        }

        [Test]
        public async Task InsertUser()
        {
            var userController = new UserController(null, new Repository());
            var result = await userController.InsertUser(_testUser);
            Assert.AreNotEqual(0,result.Value);
        }

        [Test]
        public async Task GetAllUsers()
        {
           var userController = new UserController(null,new Repository());
           var result = await userController.GetAllUsers();
           Assert.AreEqual(true, result.Value!=null);
        }

        [Test]
        public async Task UpdateUser()
        {
            var userController = new UserController(null, new Repository());
            this._testUser.FirstName = "Ashu";
            this._testUser.Id = 2;
            var result = await userController.UpdateUser(this._testUser);
            if (result is OkResult)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
            
        }
    }
}