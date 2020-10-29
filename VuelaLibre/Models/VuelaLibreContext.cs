using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VuelaLibre.Models.Maps;

namespace VuelaLibre.Models
{
    public class VuelaLibreContext : DbContext
    {
        //Esto se hace por cada tabla de base de datos
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Vuelo> Vuelos { get; set; }
        public DbSet<Flights> ListVuelo { get; set; }
        public DbSet<Aerolinea> Aerolineas { get; set; }
        public DbSet<DetalleVuelo> DetalleVuelos { get; set; }

        public VuelaLibreContext(DbContextOptions<VuelaLibreContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Esto se hace por cada tabla de base de datos
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new VueloMap());
            modelBuilder.ApplyConfiguration(new FlightsMap());
            modelBuilder.ApplyConfiguration(new AerolineaMap());
            modelBuilder.ApplyConfiguration(new DetalleVueloMap());
        }
    }
}
