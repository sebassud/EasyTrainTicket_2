using EasyTrainTickets.Common.DTOs;
using EasyTrainTickets.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EasyTrainTickets.Domain.Model;
using EasyTrainTickets.Server.Models;

namespace EasyTrainTickets.Server.Controllers
{
    public class LoginController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;
        private UserModel userModel = new UserModel();

        public LoginController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }
        // GET: api/Login
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Login/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Login
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> Post(UserDTO userDTO)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                userDTO = await Task.Run(() => userModel.SignIn(userDTO, dbcontext));

                if (userDTO != null) return Ok(userDTO);
                else
                    return NotFound();
            }
        }

        // PUT: api/Login/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Login/5
        //public void Delete(int id)
        //{
        //}
    }
}
