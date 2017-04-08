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

namespace EasyTrainTickets.Server.Controllers
{
    public class ConnectionsController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;

        public ConnectionsController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }

        // GET: api/Connections
        public IEnumerable<ConnectionDTO> GetConnections()
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return AutoMapper.Mapper.Map<IEnumerable<Connection>, IEnumerable<ConnectionDTO>>(dbcontext.Connections);
            }
        }

        // GET: api/Connections/5
        [ResponseType(typeof(ConnectionDTO))]
        public async Task<IHttpActionResult> GetConnection(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                Connection connection = await Task.Run(() => dbcontext.Connections.Find(id));
                if (connection == null)
                {
                    return NotFound();
                }
                ConnectionDTO conDTO = AutoMapper.Mapper.Map<Connection, ConnectionDTO>(connection);
                return Ok(conDTO);
            }
        }

        // POST: api/Connections
        [ResponseType(typeof(Connection))]
        public async Task<IHttpActionResult> PostConnection(ConnectionDTO connectionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Connection connection = AutoMapper.Mapper.Map<ConnectionDTO, Connection>(connectionDTO);

            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                dbcontext.Connections.Add(connection);
                await dbcontext.SaveChangesAsync();

                connectionDTO = AutoMapper.Mapper.Map<Connection, ConnectionDTO>(connection);
                return CreatedAtRoute("DefaultApi", new { id = connectionDTO.Id }, connectionDTO);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool ConnectionExists(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return dbcontext.Connections.Count(e => e.Id == id) > 0;
            }
        }
    }
}