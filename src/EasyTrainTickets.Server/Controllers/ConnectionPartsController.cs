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
using EasyTrainTickets.Common.DTOs;
using AutoMapper;
using EasyTrainTickets.Server.Models;

namespace EasyTrainTickets.Server.Controllers
{
    public class ConnectionPartsController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;

        public ConnectionPartsController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }

        // GET: api/ConnectionParts
        //public IEnumerable<ConnectionPartDTO> GetConnectionParts()
        //{
        //    return Mapper.Map<IEnumerable<ConnectionPart>,IEnumerable<ConnectionPartDTO>>(dbcontext.ConnectionParts);
        //}

        // GET: api/ConnectionParts/5
        [ResponseType(typeof(ConnectionPartDTO))]
        public async Task<IHttpActionResult> GetConnectionPart(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                ConnectionPart connectionPart = await Task.Run(() => dbcontext.ConnectionParts.Where(con => con.Id == id).First());
                if (connectionPart == null)
                {
                    return NotFound();
                }
                var q = Mapper.Map<ConnectionPart, ConnectionPartDTO>(connectionPart);

                return Ok(q);
            }
                
        }

        // PUT: api/ConnectionParts/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutConnectionPart(int id, ConnectionPartDTO connectionPartDTO)
        //{
        //    ConnectionPart connectionPart = Mapper.Map<ConnectionPartDTO, ConnectionPart>(connectionPartDTO);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != connectionPart.Id)
        //    {
        //        return BadRequest();
        //    }

        //    dbcontext.Entry(connectionPart).State = EntityState.Modified;

        //    try
        //    {
        //        await dbcontext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ConnectionPartExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/ConnectionParts
        //[ResponseType(typeof(ConnectionPartDTO))]
        //public async Task<IHttpActionResult> PostConnectionPart(ConnectionPartDTO connectionPartDTO)
        //{
        //    ConnectionPart connectionPart = Mapper.Map<ConnectionPartDTO, ConnectionPart>(connectionPartDTO);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    dbcontext.ConnectionParts.Add(connectionPart);
        //    await dbcontext.SaveChangesAsync();

        //    connectionPartDTO = Mapper.Map<ConnectionPart, ConnectionPartDTO>(connectionPart);
        //    return CreatedAtRoute("DefaultApi", new { id = connectionPartDTO.Id }, connectionPartDTO);
        //}

        // DELETE: api/ConnectionParts/5
        //[ResponseType(typeof(ConnectionPart))]
        //public async Task<IHttpActionResult> DeleteConnectionPart(int id)
        //{
        //    ConnectionPart connectionPart = await dbcontext.ConnectionParts.FindAsync(id);
        //    if (connectionPart == null)
        //    {
        //        return NotFound();
        //    }

        //    dbcontext.ConnectionParts.Remove(connectionPart);
        //    await dbcontext.SaveChangesAsync();

        //    return Ok(connectionPart);
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool ConnectionPartExists(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return dbcontext.ConnectionParts.Count(e => e.Id == id) > 0;
            }
        }
    }
}