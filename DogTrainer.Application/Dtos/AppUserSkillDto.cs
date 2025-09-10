using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Dtos
{
    public class AppUserSkillDto
    {
        public required string  AppUserId { get; set; }
        public required int SkillId { get; set; }
    }
}
