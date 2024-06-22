using DevFreela.API.Model;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // api/projects?query=net core GET
        [HttpGet]
        public IActionResult Get(string query)
        {
            List<ProjectViewModel> projects = _projectService.GetAll(query);

            return Ok(projects);
        }

        // api/projects/1 GET
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            // Buscar o projeto
            ProjectDetailsViewModel project = _projectService.GetById(id);

            if (project is null)
                return NotFound();

            return Ok(project);
        }

        // api/projects POST
        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel inputModel)
        {
            if (inputModel.Title.Length > 50)
            {
               return BadRequest();
            }

            int id = _projectService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id = id }, inputModel);
        }

        // api/projects/2 PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
        {
            if (inputModel.Description.Length > 200)
            {
                return BadRequest();
            }

            _projectService.Update(inputModel);

            return NoContent();
        }

        // api/projects/3 DELETE
        [HttpPost("{id}")]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);

            return NoContent();
        }

        // api/projects/1/comments POST
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel inputModel)
        {
            _projectService.CreateComment(inputModel);
            
            return NoContent(); 
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);

            return NoContent();
        }

        // api/projects/1/finish
        [HttpPost("{id}/finish")]
        public IActionResult Finish(int id) 
        { 
            _projectService.Finish(id);

            return NoContent(); 
        }
    }
}
