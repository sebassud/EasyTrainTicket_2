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

namespace EasyTrainTickets.Server.Controllers
{
    public class TrainsController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;

        public TrainsController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }

        // GET: api/Trains
        public IEnumerable<TrainDTO> GetTrains()
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return Mapper.Map<IEnumerable<Train>, IEnumerable<TrainDTO>>(dbcontext.Trains);
            }
        }

        // GET: api/Trains/5
        [ResponseType(typeof(TrainDTO))]
        public async Task<IHttpActionResult> GetTrain(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                Train train = await Task.Run(() => dbcontext.Trains.Find(id));
                if (train == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<Train, TrainDTO>(train));
            }
        }

        // PUT: api/Trains/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutTrain(int id, Train train)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != train.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(train).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TrainExists(id))
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

        //// POST: api/Trains
        //[ResponseType(typeof(Train))]
        //public async Task<IHttpActionResult> PostTrain(Train train)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Trains.Add(train);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = train.Id }, train);
        //}

        //// DELETE: api/Trains/5
        //[ResponseType(typeof(Train))]
        //public async Task<IHttpActionResult> DeleteTrain(int id)
        //{
        //    Train train = await db.Trains.FindAsync(id);
        //    if (train == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Trains.Remove(train);
        //    await db.SaveChangesAsync();

        //    return Ok(train);
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool TrainExists(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return dbcontext.Trains.Count(e => e.Id == id) > 0;
            }
        }
    }
}