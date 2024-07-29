using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TaskManagementSystem.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class TarefasController : ApiController
    {
        private readonly Repositories.Tarefa repositorioTarefa;

        public TarefasController()
        {
            repositorioTarefa = new Repositories.Tarefa(Configurations.Database.GetConnectionString());
        }

        [HttpGet]
        // GET: api/Tarefas
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await repositorioTarefa.Select());
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpGet]
        // GET: api/Tarefas/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                return Ok(await repositorioTarefa.Select(id));
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpGet]
        // GET: api/Tarefas?titulo=aaa
        public async Task<IHttpActionResult> Get(string titulo)
        {
            try
            {
                return Ok(await repositorioTarefa.Select(titulo));
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpGet]
        // GET: api/Tarefas?prazo=aaa
        public async Task<IHttpActionResult> GetByPrazo(DateTime prazo)
        {
            try
            {
                return Ok(await repositorioTarefa.SelectByPrazo(prazo));
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpPost]
        // POST: api/Tarefas
        public async Task<IHttpActionResult> Post([FromBody]Models.Tarefa tarefa)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await repositorioTarefa.Insert(tarefa);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpPut]
        // PUT: api/Tarefas/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Models.Tarefa tarefa)
        {
            try
            {
                if (id != tarefa.Id)
                    return BadRequest("O id da requisição não coiecide com o id do corpo da requisição!");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await repositorioTarefa.Update(tarefa);
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpDelete]
        // DELETE: api/Tarefas/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                return Ok(!await repositorioTarefa.Delete(id));
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }
    }
}
