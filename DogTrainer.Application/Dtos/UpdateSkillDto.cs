using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogTrainer.Application.Dtos
{
    public class UpdateSkillDto
    {
        required
        public int Id
        { get; set; }
        required public string Name { get; set; } = string.Empty;
    }
}
