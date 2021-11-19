using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext() : base("Default Connection")
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}