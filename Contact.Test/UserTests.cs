using System;
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
        #region Fields

        private UserDetail _testUser;

        #endregion

        #region Setup

        [SetUp]
        public void Setup()
        {
            DomainHelper.JsonFileName = "user_TestData.json";
            this._testUser = new UserDetail()
            {
                FirstName = "Ashutosh",
                LastName = "Tutwani",
                DateOfBirth = "1992-10-31",
            };

            _testUser.Emails.Add("ashutoshtutwani@gmail.com");
            _testUser.Emails.Add("ashututwani@gmail.com");

            _testUser.PhoneNumbers.Add("8368011119");
        }

        #endregion

        #region Test Methods

        [TestCaseSource("InsertUsersTestCases")]
        [Order(1)]
        [Test]
        public async Task InsertUser(UserDetail userDetail)
        {
            var userController = new UserController(null, new Repository());
            var result = await userController.InsertUser(userDetail);
            Assert.AreNotEqual(0,result.Value);
        }

        [Test]
        [Order(3)]
        public async Task GetAllUsers()
        {
            var userController = new UserController(null,new Repository());
            var result = await userController.GetAllUsers();
            Assert.AreEqual(true, result.Value!=null);
        }

        [Test]
        [Order(2)]
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

        #endregion

        #region Methods

        private static IEnumerable<UserDetail> InsertUsersTestCases()
        {
            yield return new UserDetail()
            {
                FirstName = "Jack",
                LastName = "Nicholson",
                DateOfBirth = "1981-09-10",
                Emails = new List<string>()
                {
                    "jack.nick@gmail.com",
                    "jack2.3nick@gmail.com",
                },
                PhoneNumbers = new List<string>()
                {
                    "9810895395",
                    "4423221322",
                },
            };

            yield return new UserDetail()
            {
                FirstName = "Tom",
                LastName = "Hanks",
                DateOfBirth = "1991-12-19",
                Emails = new List<string>()
                {
                    "tom.hanks@reddif.com",
                    "tomactor.hanks@gmail.com",
                },
                PhoneNumbers = new List<string>()
                {
                    "1043895395",
                    "7423521322",
                },
            };

            yield return new UserDetail()
            {
                FirstName = "David",
                LastName = "Miller",
                DateOfBirth = "1971-09-10",
                Emails = new List<string>()
                {
                    "david.miller@gmail.com",
                },
                PhoneNumbers = new List<string>()
                {
                    "7723221322",
                },
            };
        }

        #endregion
    }
}