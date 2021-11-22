using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {

        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //Post
        // api/Restaurant
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant([FromBody] Restaurant model)
        {
            if (model is null)
            {
                return BadRequest("Nice try, your entity is null");
            }
            if (ModelState.IsValid)
            {
                //store in hte databasee 
                _context.Restaurants.Add(model);
                int changeCount = await _context.SaveChangesAsync();
                return Ok("Restaurant added!");
            }

            //vlns mdl
            return BadRequest(ModelState);
        }

        //Get All
        // api/Restaurant
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);

        }

        //Get By ID
        // api/Restaurant/{id}
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }

        //PUT (update)
        // api/Restaurant/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updatedRestaurant)
        {
            if(id != updatedRestaurant?.Id)
            {
                return BadRequest("the Id's do not match...");
            }

            if (!ModelState.IsValid)
                return BadRequest("I declare this invalid!");

            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant is null)
                return NotFound();

            restaurant.Name = updatedRestaurant.Name;
           
            restaurant.Address = updatedRestaurant.Address;

            await _context.SaveChangesAsync();

            return Ok("Restaurant Updated.");
        }

        //DELETE (Delete)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant is null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("Restaurant deleted");
            }

            return InternalServerError();
        }
    }
}
