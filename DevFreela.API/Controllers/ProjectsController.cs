using DevFreela.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly OpenigTimeOption _option;

        public ProjectsController(IOptions<OpenigTimeOption> option)
        {
            _option = option.Value;
        }

        // api/projects?query=net core GET
        [HttpGet]
        public IActionResult Get(string query)
        {
            // Buscar todos ou filtrar
            return Ok();
        }

        // api/projects/1 GET
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            // Buscar o projeto

            // return NotFound();

            return Ok();
        }

        // api/projects POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectModel createProjectModel)
        {
            if (createProjectModel.Title.Length > 50)
            {
               return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = createProjectModel.Id }, createProjectModel);
        }

        // api/projects/2 PUT
        [HttpPut]
        public IActionResult Put(int id, [FromBody] UpdateProjectModel updateProjectModel)
        {
            if (updateProjectModel.Description.Length > 200)
            {
                return BadRequest();
            }

            // Atualizar o objeto

            return NoContent();
        }

        // api/projects/3 DELETE
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            // Buscar, se não existir, retornar NotFound

            // Remover

            return NoContent();
        }

        // api/projects/1/comments POST
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentModel createCommentModel)
        {
            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        // api/projects/1/finish
        [HttpPost("{id}/finish")]
        public IActionResult Finish(int id) 
        { 
            return NoContent(); 
        }
    }
}
