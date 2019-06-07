﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Multas2.Models;
using Owin;

namespace Multas2 {

    // O código desta classe é iniciada uma única vez
    // e apenas no início do projecto 
    public partial class Startup {

        // funciona como a função 'main' do C
        public void Configuration(IAppBuilder app){

            ConfigureAuth(app);

            // executar o método para configurar a aplicação
            iniciaAplicacao();
        }

        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Agente, Funcionario, Condutor
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao() {

            // identifica a base de dados de serviço à aplicação
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Agente'
            if (!roleManager.RoleExists("Agente")) {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Agente";
                roleManager.Create(role);
            }

            // criar a Role 'Recursos Humanos'
            if (!roleManager.RoleExists("RecursosHumanos")) {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "RecursosHumanos";
                roleManager.Create(role);
            }

            // criar a Role 'Gestor de Multas'
            if (!roleManager.RoleExists("GestorMultas")) {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "GestorMultas";
                roleManager.Create(role);
            }

            //Adicionar o Utilizador à respetiva Role-Agente-
            if (chkUser.Succeeded) {
                var result1 = userManager.AddToRole(user.Id, "Agente");
            }
        }

        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97




    }
}
