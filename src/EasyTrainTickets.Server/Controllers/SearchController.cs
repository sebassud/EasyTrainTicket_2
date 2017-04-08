using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyTrainTickets.Common.DTOs;
using System.Web.Http.Description;
using EasyTrainTickets.Server.Models;
using EasyTrainTickets.Domain.Data;
using System.Threading.Tasks;

namespace EasyTrainTickets.Server.Controllers
{
    public class SearchController : ApiController
    {
        private IUnitOfWorkFactory unitOfWorkFactory;
        SearchModel searchModel = new SearchModel();
        public SearchController(IUnitOfWorkFactory _unitOfWorkFactory)
        {
            unitOfWorkFactory = _unitOfWorkFactory;
        }
        // POST: api/Search
        [ResponseType(typeof(IEnumerable<ConnectionPathDTO>))]
        public async Task<IHttpActionResult> PostSearch(FilterPathsDTO fp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //using (var dbcontext = unitOfWorkFactory.CreateUnitOfWork())
            //{
                //IEnumerable<ConnectionPathDTO> conPaths = await Task.Run(() => searchModel.Search(fp, dbcontext));
                IEnumerable<ConnectionPathDTO> conPaths = await Task.Run(() => searchModel.Search(fp,unitOfWorkFactory));

                return Ok(conPaths);
            //}
        }

    }
}
