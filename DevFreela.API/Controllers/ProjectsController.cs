using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;

        public ProjectsController(IProjectService projectService, IMediator mediator)
        {
            _projectService = projectService;
            _mediator = mediator;
        }

        // api/projects?query=net core GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string? query, int page)
        {
            var getAllProjectQuery = new GetAllProjectsQuery(query, page);

            var projects = await _mediator.Send(getAllProjectQuery);

            return Ok(projects);
        }

        // api/projects/1 GET
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id) 
        {
            var query = new GetProjectByIdQuery(id);

            var project = await _mediator.Send(query);

            if (project is null)
                return NotFound();

            return Ok(project);
        }

        // api/projects POST
        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            int id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        // api/projects/2 PUT
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
                return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/3 DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/1/comments POST
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent(); 
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);

            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command) 
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest("O pagamento não pode ser processado");
            }
            
            return Accepted(); 
        }
    }
}
