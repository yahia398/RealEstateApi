using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db)
        {
            _db= db;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var CategoriesList = _db.Categories.ToList();
            return Ok(CategoriesList);
        }
    }
}
