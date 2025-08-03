using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext nzWalksDbContext)
        {
            this.dbContext = nzWalksDbContext;
        }
        [HttpGet]
        public IActionResult GetAllRegion()
        {
            var regions = dbContext.Regions.ToList();

            return Ok(regions);
        }
    }
}
