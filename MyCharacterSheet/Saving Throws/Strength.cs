using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.SavingThrowsNamespace
{
    class Strength : SavingThrows
    {
        private int bonus;

        public Strength(bool proficiency)
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

                bonus += (int)Math.Floor((Program.Character.Strength - 10) / 2.0);

                return bonus;
            }
        }
    }
}
