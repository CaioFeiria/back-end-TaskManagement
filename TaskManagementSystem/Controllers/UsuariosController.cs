using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TaskManagementSystem.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuariosController : ApiController
    {
        private readonly Repositories.Usuario repositorioUsuario;

        public UsuariosController()
        {
            repositorioUsuario = new Repositories.Usuario(Configurations.Database.GetConnectionString());
        }

        [HttpGet]
        // GET: api/Usuarios
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await repositorioUsuario.Select());
            }
            catch (Exception e)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), e);

                return InternalServerError();
            }
        }

        [HttpGet]
        // GET: api/Usuarios/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                return Ok(await repositorioUsuario.Select(id));
            }
            catch (Exception e)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), e);

                return InternalServerError();
            }
        }

        [HttpGet]
        // GET: api/Usuarios?nome=Caio
        public async Task<IHttpActionResult> Get(string nome)
        {
            try
            {
                return Ok(await repositorioUsuario.Select(nome));
            }
            catch (Exception e)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), e);

                return InternalServerError();
            }
        }

        [HttpGet]
        // GET: api/Usuarios?cargo=Dev
        public async Task<IHttpActionResult> GetByCargo(string cargo)
        {
            try
            {
                return Ok(await repositorioUsuario.SelectByCargo(cargo));
            }
            catch (Exception e)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), e);

                return InternalServerError();
            }
        }

        [HttpPost]
        // POST: api/Usuarios
        public async Task<IHttpActionResult> Post([FromBody]Models.Usuario usuario)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                await repositorioUsuario.Insert(usuario);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), e);

                return InternalServerError();
            }
        }

        [HttpPut]
        // PUT: api/Usuarios/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Models.Usuario usuario)
        {

            try
            {
                if (!Validations.Requisicao.IdRequisicaoIgualIdCorpoRequisicao(id, usuario.Id))
                    return BadRequest("O id da requisição não coiecide com o id do corpo da requisição.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await repositorioUsuario.Update(usuario);
                return Ok(usuario);
            }
            catch (Exception e)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), e);

                return InternalServerError();
            }
        }

        [HttpDelete]
        // DELETE: api/Usuarios/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                return Ok(await repositorioUsuario.Delete(id));
            }
            catch (Exception e)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), e);

                return InternalServerError();
            }
        }
    }
}
