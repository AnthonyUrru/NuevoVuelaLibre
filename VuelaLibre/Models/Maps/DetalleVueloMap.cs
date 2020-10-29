using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VuelaLibre.Models.Maps
{
    public class DetalleVueloMap : IEntityTypeConfiguration<DetalleVuelo>
    {
        public void Configure(EntityTypeBuilder<DetalleVuelo> builder)
        {
            builder.ToTable("DetalleVuelo");
            builder.HasKey(o => o.Id);
        }
    }
}
