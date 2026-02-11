using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using To_Do.Models.Entities;

namespace To_Do.Data.Context
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext() : base("name=conexao")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}