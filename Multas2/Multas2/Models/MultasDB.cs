using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Multas2.Models
{
    public class MultasDB :DbContext
    {
        public MultasDB () : base("MultasDBConnectionString") { }
        // Definir as tabelas
        public DbSet<Condutores> Condutores { get; set; }
        public DbSet<Viaturas> Carros { get; set; }
        public DbSet<Agentes> Agentes { get; set; }
        public DbSet<Multas> Multas { get; set; }


    }
}