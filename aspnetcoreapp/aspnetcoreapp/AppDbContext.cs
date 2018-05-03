using aspnetcoreapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcoreapp
{
    public class AppDbContext : DbContext
    {

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Tenant> Tenants { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelbuilder)
    //{

    //  var types = modelbuilder.Model.GetEntityTypes().ToList();


    //}

    }
}
