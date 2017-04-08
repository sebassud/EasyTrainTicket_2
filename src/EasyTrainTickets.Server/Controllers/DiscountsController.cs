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
    public class DiscountsController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;

        public DiscountsController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }

        // GET: api/Discounts
        public IEnumerable<DiscountDTO> GetDiscounts()
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return Mapper.Map<IEnumerable<Discount>, IEnumerable<DiscountDTO>>(dbcontext.Discounts);
            }
        }

        // GET: api/Discounts/5
        [ResponseType(typeof(DiscountDTO))]
        public async Task<IHttpActionResult> GetDiscount(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                Discount discount = await Task.Run(() => dbcontext.Discounts.Find(id));
                if (discount == null)
                {
                    return NotFound();
                }

                DiscountDTO discountDTO = Mapper.Map<Discount, DiscountDTO>(discount);
                return Ok(discountDTO);
            }
        }

        // PUT: api/Discounts/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutDiscount(int id, Discount discount)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != discount.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(discount).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DiscountExists(id))
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

        //// POST: api/Discounts
        //[ResponseType(typeof(Discount))]
        //public async Task<IHttpActionResult> PostDiscount(Discount discount)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Discounts.Add(discount);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = discount.Id }, discount);
        //}

        //// DELETE: api/Discounts/5
        //[ResponseType(typeof(Discount))]
        //public async Task<IHttpActionResult> DeleteDiscount(int id)
        //{
        //    Discount discount = await db.Discounts.FindAsync(id);
        //    if (discount == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Discounts.Remove(discount);
        //    await db.SaveChangesAsync();

        //    return Ok(discount);
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool DiscountExists(int id)
        {
            using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            {
                return dbcontext.Discounts.Count(e => e.Id == id) > 0;
            }
        }
    }
}