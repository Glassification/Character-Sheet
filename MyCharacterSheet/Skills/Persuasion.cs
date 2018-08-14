﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.SkillsNamespace
{
    class Persuasion : Skills
    {
        private int bonus;

        public Persuasion(bool proficiency, bool expertise)
        {
            Proficiency = proficiency;
            Expertise = expertise;
        }

        public override int Bonus
        {
            get
            {
                bonus = 0;

                if (Proficiency)
                    bonus += Program.Character.ProficiencyBonus;
                if (Expertise)
                    bonus += Program.Character.ProficiencyBonus;

                bonus += (int)Math.Floor((Program.Character.Charisma - 10) / 2.0);

                return bonus;
            }
        }
    }
}