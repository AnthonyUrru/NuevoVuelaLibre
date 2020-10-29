using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VuelaLibre.Models.Maps
{
    public class AerolineaMap: IEntityTypeConfiguration<Aerolinea>
    {
        public void Configure(EntityTypeBuilder<Aerolinea> builder)
        {
            builder.ToTable("Aerolinea");
            builder.HasKey(o => o.Id);
        }
    }
}
