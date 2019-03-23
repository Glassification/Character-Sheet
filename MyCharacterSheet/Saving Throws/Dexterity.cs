using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.SavingThrowsNamespace
{
    public class Dexterity : SavingThrows
    {
        private int bonus;

        public Dexterity(bool proficiency)
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

                bonus += (int)Math.Floor((Program.Character.Dexterity - 10) / 2.0);

                return bonus;
            }
        }
    }
}
