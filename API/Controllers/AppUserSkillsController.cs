
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DogTrainer.Application.Dtos;
using DogTrainer.Application.Features.AppUserSkill.Commands;
using DogTrainer.Application.Features.AppUserSkill.Queries;// Annahme: Es gibt entsprechende Commands/Queries in diesem Namespace


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserSkillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUserSkillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Weist einem Benutzer einen Skill zu.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AssignSkillToUser([FromBody] AppUserSkillDto dto)
        {
            // Annahme: Es gibt einen AddAppUserSkillCommand
            var appUserSkill = await _mediator.Send(new AddAppUserSkill.Command(dto));
            // Da es keine "GetById"-Aktion für eine Zuweisung gibt, ist Ok() hier eine gute Wahl.
            return Ok(appUserSkill);
        }

        /// <summary>
        /// Ruft alle Skills für einen bestimmten Benutzer ab.
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetSkillsForUser(string userId)
        {
            // Annahme: Es gibt einen GetSkillsForUserQuery
            var skills = await _mediator.Send(new GetSkillsForUser.Query(userId));
            return Ok(skills);
        }

        /// <summary>
        /// Entfernt eine Skill-Zuweisung von einem Benutzer.
        /// </summary>
        [HttpDelete("user/{userId}/skill/{skillId}")]
        public async Task<IActionResult> RemoveSkillFromUser(string userId, int skillId)
        {
            // Annahme: Es gibt einen RemoveAppUserSkillCommand
            var appUserSkillDto = new AppUserSkillDto { AppUserId = userId, SkillId = skillId };
            await _mediator.Send(new RemoveAppUserSkill.Command(appUserSkillDto));
            return NoContent();
        }
    }
}