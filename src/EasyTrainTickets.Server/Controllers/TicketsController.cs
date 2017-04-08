using EasyTrainTickets.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyTrainTickets.Server.Models;
using EasyTrainTickets.Common.DTOs;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Data.Entity.Infrastructure;

namespace EasyTrainTickets.Server.Controllers
{
    public class TicketsController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;
        private TicketsModel ticketsModel = new TicketsModel();

        public TicketsController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }
        // GET: api/Tickets
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Tickets/5
        [ResponseType(typeof(List<TicketDTO>))]
        public async Task<IHttpActionResult> Get([FromUri] string id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                List<TicketDTO> tickets = await Task.Run(() => ticketsModel.GetTicketsUser(id, dbcontext));
                if (tickets == null)
                {
                    return NotFound();
                }
                return Ok(tickets);
            }
        }

        //POST: api/Tickets
       [ResponseType(typeof(TicketDTO))]
        public async Task<IHttpActionResult> Post(TicketDTO value)
        {

            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                try
                {
                    await Task.Run(() => ticketsModel.DeleteTicket(value, dbcontext));
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Content(HttpStatusCode.Conflict, value);
                }

                return Ok(value);
            }
        }

        // PUT: api/Tickets
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(TicketDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            using (var dbcontext = unitOfWorkFactory.CreateTransactionalUnitOfWork())
            {
                try
                {
                    var answer = await Task.Run(() => ticketsModel.BuyTicket(value, dbcontext));
                    if(!answer)
                    {
                        return Conflict();
                    }
                    //await dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return StatusCode(HttpStatusCode.Conflict);
                }
                dbcontext.Accept();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // DELETE: api/Tickets/5
        //[ResponseType(typeof(TicketDTO))]
        //public async Task<IHttpActionResult> Delete(TicketDTO value)
        //{
        //    using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
        //    {
        //        try
        //        {
        //            await Task.Run(() => ticketsModel.DeleteTicket(value, dbcontext));
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            return Content(HttpStatusCode.Conflict, value);
        //        }

        //        return Ok(value);
        //    }
        //}
    }
}
