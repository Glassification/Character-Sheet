﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concierge.SavingThrowsNamespace
{
    public class Charisma : SavingThrows
    {
        private int bonus;

        public Charisma(bool proficiency)
        {
            Proficiency = proficiency;
        }

        public override int Bonus
        {
            get
            {
                bonus = 0;

                if (Proficiency)
                    bonus += Program.Character.ProficiencyBonus;

                bonus += (int)Math.Floor((Program.Character.Charisma - 10) / 2.0);

                return bonus;
            }
        }
    }
}
