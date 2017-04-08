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
    public class RoutesController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;

        public RoutesController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }

        // GET: api/Routes
        public IEnumerable<RouteDTO> GetRoutes()
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return Mapper.Map<IEnumerable<Route>, IEnumerable<RouteDTO>>(dbcontext.Routes);
            }
        }

        // GET: api/Routes/5
        [ResponseType(typeof(RouteDTO))]
        public async Task<IHttpActionResult> GetRoute(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                Route route = await Task.Run(() => dbcontext.Routes.Find(id));
                if (route == null)
                {
                    return NotFound();
                }

                RouteDTO routeDTO = Mapper.Map<Route, RouteDTO>(route);
                return Ok(routeDTO);
            }
        }

        //// PUT: api/Routes/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutRoute(int id, Route route)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != route.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(route).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RouteExists(id))
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

        //// POST: api/Routes
        //[ResponseType(typeof(Route))]
        //public async Task<IHttpActionResult> PostRoute(Route route)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Routes.Add(route);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = route.Id }, route);
        //}

        //// DELETE: api/Routes/5
        //[ResponseType(typeof(Route))]
        //public async Task<IHttpActionResult> DeleteRoute(int id)
        //{
        //    Route route = await db.Routes.FindAsync(id);
        //    if (route == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Routes.Remove(route);
        //    await db.SaveChangesAsync();

        //    return Ok(route);
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool RouteExists(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return dbcontext.Routes.Count(e => e.Id == id) > 0;
            }
        }
    }
}