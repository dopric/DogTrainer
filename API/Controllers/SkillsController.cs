using DogTrainer.Application.Dtos;
using DogTrainer.Application.Interfaces;
using DogTrainer.Application.Skills.Commands;
using DogTrainer.Application.Skills.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> CreateSkill([FromBody] SkillDto dto)
        {
            return Ok(await _mediator.Send(new AddSkillCommand.Command { Skill = dto }));
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var skills = await _mediator.Send(new GetAllSkillsQuery.Query());
            return Ok(skills);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            await _mediator.Send(new DeleteSkillCommand.Command(id));
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetSkillById(int id)
        {
            var skill = await _mediator.Send(new GetSkillByIdQuery.Query { Id = id });
            if(skill == null)
            {
                return NotFound();
            }
            return Ok(skill);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateSkill(int id, [FromBody] UpdateSkillDto dto)
        {
            if(id != dto.Id)
            {
                return BadRequest("ID mismatch");
            }
            var updatedSkill = await _mediator.Send(new UpdateSkillCommand.Command { Skill = dto });
            return Ok(updatedSkill);
        }
    }
}
