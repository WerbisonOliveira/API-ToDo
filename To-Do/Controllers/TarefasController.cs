using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Configuration;
using To_Do.Repositories;
using To_Do.DTOs;
using To_Do.Models.Entities;
using System;
using Mapster;
using To_Do.Logger;

namespace To_Do.Controllers
{
    [Authorize]
    [RoutePrefix("api/tarefas")]
    public class TarefasController : ApiController
    {
        private readonly TarefaRepository tarefaRepo;
        private readonly Log log;
        public TarefasController()
        {
            tarefaRepo = new TarefaRepository();
            log = new Log(ConfigurationManager.AppSettings["Path"]);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                return Ok(await tarefaRepo.GetAllAsync());
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var tarefa = await tarefaRepo.GetAsync(id);

                if (tarefa == null)
                    return Content(HttpStatusCode.NotFound, new {message = "Tarefa não encontrada"});

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create(Tarefa tarefa)
        {
            if (tarefa == null)
                return BadRequest("Todos os campos são obrigatórios");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tarefaDTO = tarefa.Adapt<TarefaDTO>();

                return Content(HttpStatusCode.Created, await tarefaRepo.CreateAsync(tarefaDTO));
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        [Route("update/{id:int}")]
        [HttpPatch]
        public async Task<IHttpActionResult> Update(int id, TarefaDTO tarefa)
        {
            if (id != tarefa.Id)
                return BadRequest("O id do endpoint é diferente do id enviado no objeto da requisição");

            if (tarefa == null)
                return BadRequest("Todos os campos são obrigatórios");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var tarefaDTO = await tarefaRepo.PatchAsync(tarefa);

                if (tarefaDTO == null)
                    return Content(HttpStatusCode.NotFound, new {message = "Tarefa não encontrada"});

                return Ok(new {message = "Tarefa atualizada com sucesso"});
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }

        }

        [Route("delete/{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var tarefa = await tarefaRepo.DeleteAsync(id);

                if (tarefa == null)
                    return Content(HttpStatusCode.NotFound, new {message = "Tarefa não encontrada"});

                return Ok(new {message = "Tarefa excluída com sucesso"});
            }
            catch (Exception ex)
            {
                await log.Logger(ex);
                return InternalServerError();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tarefaRepo.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}