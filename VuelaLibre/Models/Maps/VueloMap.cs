using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VuelaLibre.Models.Maps
{
    public class VueloMap: IEntityTypeConfiguration<Vuelo>
    {
        public void Configure(EntityTypeBuilder<Vuelo> builder)
        {
                builder.ToTable("Vuelo");
                builder.HasKey(o => o.Id);
        }
        
    }
}
