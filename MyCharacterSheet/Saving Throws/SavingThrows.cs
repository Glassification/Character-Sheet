﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.SavingThrowsNamespace
{
    public abstract class SavingThrows
    {
        public bool Proficiency
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