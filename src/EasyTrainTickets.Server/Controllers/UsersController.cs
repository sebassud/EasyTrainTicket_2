using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EasyTrainTickets.Domain.Data;
using EasyTrainTickets.Domain.Model;
using AutoMapper;
using EasyTrainTickets.Common.DTOs;
using EasyTrainTickets.Server.Models;

namespace EasyTrainTickets.Server.Controllers
{
    public class UsersController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;
        private UserModel userModel = new UserModel();

        public UsersController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }

        //// GET: api/Users
        //public IEnumerable<UserDTO> GetUsers()
        //{
        //    using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
        //    {
        //        return Mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(dbcontext.Users);
        //    }
        //}

        //// GET: api/Users/5
        //[ResponseType(typeof(UserDTO))]
        //public async Task<IHttpActionResult> GetUser(int id)
        //{
        //    using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
        //    {
        //        User user = await Task.Run(() => dbcontext.Users.Find(id));
        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(Mapper.Map<User, UserDTO>(user));
        //    }
        //}

        // PUT: api/Users/5
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> PutUser(ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                try
                {
                    UserDTO userDTO = await Task.Run(() => userModel.ChangePassword(changePasswordDTO, dbcontext));
                    if (userDTO == null) return NotFound();
                    await dbcontext.SaveChangesAsync();
                    return Ok(userDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Conflict();
                }
            }
        }

        // POST: api/Users
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> PostUser(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                userDTO = await Task.Run(() => userModel.Registration(userDTO, dbcontext));

                if (userDTO == null) return Conflict(); 

                await dbcontext.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { id = userDTO.Id }, userDTO);
            }
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                User user = await Task.Run(() => dbcontext.Users.Find(id));
                if (user == null)
                {
                    return NotFound();
                }

                dbcontext.Users.Remove(user);
                await dbcontext.SaveChangesAsync();

                UserDTO userDTO = Mapper.Map<User, UserDTO>(user);
                return Ok(userDTO);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return dbcontext.Users.Count(e => e.Id == id) > 0;
            }
        }
    }
}