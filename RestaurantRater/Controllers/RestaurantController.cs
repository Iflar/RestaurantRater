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
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        //Get By ID
        // api/Restaurant/{id}
        public async Task<IHttpActionResult> GetById([FromBody] int id)
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
        public async Task<IHttpActionResult> UpdateRestaurant([FromBody] int id, [FromBody] Restaurant updatedRestaurant)
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
            restaurant.Rating = updatedRestaurant.Rating;
            restaurant.Address = updatedRestaurant.Address;

            await _context.SaveChangesAsync();

            return Ok("Restaurant Updated!");
        }
    }
}
