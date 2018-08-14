using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.SkillsNamespace
{
    public abstract class Skills
    {
        public bool Proficiency
        {
            get;
            set;
        }

        public bool Expertise
        {
            get;
            set;
        }

        public abstract int Bonus
        {
            get;
        }
    }
}
