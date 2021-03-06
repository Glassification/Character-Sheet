﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concierge.SavingThrowsNamespace
{
    public class Constitution : SavingThrows
    {
        private int bonus;

        public Constitution(bool proficiency)
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

                bonus += (int)Math.Floor((Program.Character.Constitution - 10) / 2.0);

                return bonus;
            }
        }
    }
}
