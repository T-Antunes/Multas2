using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Multas2.Models
{
    public class MultasDB : DbContext
    {
        public MultasDB() : base("MultasDBConnectionString") { }
        // Definir as tabelas

        // vamos colocar, aqui, as instruções relativas às tabelas do 'negócio'
        // descrever os nomes das tabelas na Base de Dados
        public DbSet<Condutores> Condutores { get; set; } //Tabela Condutores
        public DbSet<Viaturas> Viaturas { get; set; } //Tabela Viaturas
        public DbSet<Agentes> Agentes { get; set; } //Tabela Agentes
        public DbSet<Multas> Multas { get; set; } // Tabela Multas


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

        }
    }

}