using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.Security.Claims;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PropertiesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("PropertyList")]
        [Authorize]
        public IActionResult GetProperties(int categoryId)
        {
            var propertiesResult = _db.Properties.Where(p => p.CategoryId == categoryId);
            if (propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }

        [HttpGet("PropertyDetail")]
        [Authorize]
        public IActionResult GetPropertyDetail(int id)
        {
            var propertyResult = _db.Properties.Find(id);
            if (propertyResult == null)
            {
                return NotFound();
            }
            return Ok(propertyResult);
        }

        [HttpGet("TrendingProperties")]
        [Authorize]
        public IActionResult GetTrendingProperties()
        {
            var PropertiesResult = _db.Properties.Where(p => p.IsTrending == true);
            if (PropertiesResult == null)
            {
                return NotFound();
            }
            return Ok(PropertiesResult);
        }

        [HttpGet("SearchProperties")]
        [Authorize]
        public IActionResult GetSearchProperties(string address)
        {
            var PropertiesResult = _db.Properties.Where(p => p.Address.Contains(address));
            if (PropertiesResult == null)
            {
                return NotFound();
            }
            return Ok(PropertiesResult);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Models.Property property)
        {
            if (property == null)
            {
                return NoContent();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _db.Users.SingleOrDefault(u => u.Email == userEmail);
                if (user == null)
                {
                    return NotFound();
                }
                property.IsTrending = false;
                property.UserId = user.Id;
                _db.Properties.Add(property);
                _db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Models.Property property)
        {
            var propertyResult = _db.Properties.Find(id);
            if (propertyResult == null)
            {
                return NotFound();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _db.Users.SingleOrDefault(u => u.Email == userEmail);
                if (user == null)
                {
                    return NotFound();
                }
                if(propertyResult.UserId == user.Id)
                {
                    propertyResult.Name = property.Name;
                    propertyResult.Detail = property.Detail;
                    propertyResult.Address= property.Address;
                    propertyResult.Price = property.Price;
                    propertyResult.IsTrending = false;
                    _db.SaveChanges();
                    return Ok("The record has been updated");
                }
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var propertyResult = _db.Properties.Find(id);
            if (propertyResult == null)
            {
                return NotFound();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _db.Users.SingleOrDefault(x => x.Email == userEmail);
                if (user == null)
                {
                    return NotFound();
                }
                if (propertyResult.UserId == user.Id)
                {
                    _db.Properties.Remove(propertyResult);
                    _db.SaveChanges();
                    return Ok("The record has been deleted successfully");
                }
                return BadRequest();
            }
            
            
            
            
            
        }
        
    }
}
