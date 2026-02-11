using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using To_Do.Data.Context;
using To_Do.Data.Entity;
using To_Do.DTOs;
using To_Do.Models.Entities;

namespace To_Do.Repositories
{
    public class ContaRepository : IDisposable
    {
        private readonly ApplicationContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public ContaRepository()
        {
            db = new ApplicationContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public async Task<IdentityResult> CreateAsync(Conta conta)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = conta.Email,
                Email = conta.Email,
                Name = conta.Nome
            };

            IdentityResult result = await userManager.CreateAsync(user, conta.Senha);

            return result;
        }       

        public async Task<IdentityUser> FindUserAsync(string nome, string senha)
        {
            IdentityUser user = await userManager.FindAsync(nome, senha);

            if (user == null) return user;

            return user;
        }

        public async Task<IdentityResult> DeleteUserAsync(ContaDTO conta)
        {
            ApplicationUser user = await userManager.FindAsync(conta.Email, conta.Senha);

            if (user == null) return null;

            IdentityResult result = await userManager.DeleteAsync(user);

            return result;
        }

        public void Dispose()
        {
            db.Dispose();
            userManager.Dispose();
        }
    }
}