using System.Collections.Generic;
using System.Threading.Tasks;
using Contact.Domain.Interfaces;
using Contact.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly ILogger<UserController> _logger;
        private readonly IRepository _repository;

        #endregion

        #region Ctor

        public UserController(ILogger<UserController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #endregion

        #region Action Methods

        [Route("Insert")]
        [HttpPost]
        public async Task<ActionResult<int>> InsertUser(UserDetail userDetail)
        {
            var taskResult = await _repository.Insert(userDetail);
            if (taskResult.result)
            {
                return taskResult.userId;
            }
            
            return this.BadRequest();

        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDetail userDetail)
        {
            var taskResult = await _repository.Update(userDetail);
            if (taskResult)
            {
                return this.Ok();
            }
            
            return this.BadRequest();
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<UserDetail>>> GetAllUsers()
        {
            var taskResult = await _repository.GetAll();
            if (taskResult==null)
            {
                return this.BadRequest();
            }

            return taskResult;
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var taskResult = await _repository.Delete(id);
            if (!taskResult)
            {
                return this.BadRequest();
            }

            return true;
        }

        #endregion
    }
}