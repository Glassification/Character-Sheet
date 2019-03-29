﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.SkillsNamespace
{
    public class Arcana : Skills
    {
        private int bonus;

        public Arcana(bool proficiency, bool expertise)
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

                bonus += (int)Math.Floor((Program.Character.Intelligence - 10) / 2.0);

                return bonus;
            }
        }
    }
}