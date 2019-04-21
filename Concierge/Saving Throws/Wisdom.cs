using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concierge.SavingThrowsNamespace
{
    public class Wisdom : SavingThrows
    {
        private int bonus;

        public Wisdom(bool proficiency)
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

                bonus += (int)Math.Floor((Program.Character.Wisdom - 10) / 2.0);

                return bonus;
            }
        }
    }
}
