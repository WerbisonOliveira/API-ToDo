using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Configuration;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using To_Do.Data.Context;
using To_Do.Data.Entity;
using To_Do.DTOs;
using To_Do.Logger;
using To_Do.Models.Entities;
using To_Do.Repositories;

namespace To_Do.Controllers
{
    [RoutePrefix("api/conta")]
    public class ContasController : ApiController
    {
        private readonly Log log;
        private readonly ApplicationContext db;
        private readonly ContaRepository contaRepo;
        private readonly UserManager<ApplicationUser> userManager;
        public ContasController() 
        {
            log = new Log(ConfigurationManager.AppSettings["Path"]);
            contaRepo = new ContaRepository();
            db = new ApplicationContext();
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> Register([FromBody]Conta conta)
        {
            if (conta == null)
                return BadRequest("Preencha todos os campos");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await contaRepo.CreateAsync(conta);

                IHttpActionResult error = GetErrorResult(result);

                if (error != null)
                    return error;

                return Content(HttpStatusCode.Created, new {message = "Conta criada com sucesso"});
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IHttpActionResult> Login([FromBody]ContaDTO conta)
        {
            if (conta == null)
                return BadRequest("Preencha todos os campos");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                ApplicationUser user = await userManager.FindAsync(conta.Email, conta.Senha);

                if (user == null)
                    return Content(HttpStatusCode.NotFound, new {message = "Usuário não encontrado"});

                var identity = await userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, conta.Email));

                var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());

                var token = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);

                var cookie = new HttpCookie("access_token", token)
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddMinutes(30)
                };

                HttpContext.Current.Response.Cookies.Add(cookie);

                return Ok(new {message = "Login realizado com sucesso"});
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public async Task<IHttpActionResult> Logout()
        {
            try
            {
                var cookie = new HttpCookie("access_token")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                HttpContext.Current.Response.Cookies.Add(cookie);

                return Ok(new {message = "Logout realizado com sucesso"});
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Authorize]
        [Route("auth")]
        public async Task<IHttpActionResult> IsAuthenticated()
        {
            try
            {
                return Ok(new {auth = true});
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("excluir/conta")]
        public async Task<IHttpActionResult> DeleteAccount([FromBody]ContaDTO conta)
        {
            if (conta == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                IdentityResult result = await contaRepo.DeleteUserAsync(conta);

                if (result == null)
                    return Content(HttpStatusCode.NotFound, new {message = "Conta não encontrada"});

                var cookie = new HttpCookie("access_token")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };

                HttpContext.Current.Response.Cookies.Add(cookie);

                return Ok(new {message = "Conta apagada com sucesso"});
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        private IHttpActionResult GetErrorResult(IdentityResult resultado)
        {
            if (resultado == null)
                return InternalServerError();

            if (!resultado.Succeeded)
            {
                if (resultado.Errors != null)
                {
                    foreach (string error in resultado.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                    return BadRequest();

                return BadRequest(ModelState);
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contaRepo.Dispose();
                db.Dispose();
                userManager.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
