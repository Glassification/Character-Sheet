using System;
using System.ComponentModel;

namespace MyCharacterSheet.Characters
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Companion : ExpandableObjectConverter
    {

        #region Constructor

        public Companion()
        {
            Name = "";
            AC = 0;
            HitDice = "";
            HP = 0;
            CurrentHP = 0;
            Speed = "";
            Strength = 0;
            Dexterity = 0;
            Constitution = 0;
            Intelligence = 0;
            Wisdom = 0;
            Charisma = 0;
            Perception = 0;
            Senses = "";
            Attack = new Pair();
            Type = new Pair();
            AtkBonus = new Pair();
            Damage = new Pair();
            DmgType = new PresetPair();
            Reach = new Pair();
            Notes = new Pair();
        }

        public Companion(   string name, 
                            int ac, 
                            string hitDice, 
                            int hp, 
                            int currentHP, 
                            string speed, 
                            int strength, 
                            int dexterity, 
                            int constitution, 
                            int intelligence,         
                            int wisdom, 
                            int charisma, 
                            int perception, 
                            string senses, 
                            Pair attack, 
                            Pair type, 
                            Pair atkBonus, 
                            Pair damage, 
                            PresetPair dmgType,   
                            Pair reach, 
                            Pair notes)
        {
            Name = name;
            AC = ac;
            HitDice = hitDice;
            HP = hp;
            CurrentHP = currentHP;
            Speed = speed;
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
            Perception = perception;
            Senses = senses;
            Attack = attack.Copy();
            Type = type.Copy();
            AtkBonus = atkBonus.Copy();
            Damage = damage.Copy();
            DmgType = dmgType.Copy();
            Reach = reach.Copy();
            Notes = notes.Copy();
        }

        #endregion

        #region Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Strength")]
        [Description("Strength score of the animal companion.")]
        public int Strength
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Dexterity")]
        [Description("Dexterity score of the animal companion.")]
        public int Dexterity
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Constitution")]
        [Description("Constitution score of the animal companion.")]
        public int Constitution
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Intelligence")]
        [Description("Intelligence score of the animal companion.")]
        public int Intelligence
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Wisdom")]
        [Description("Wisdom score of the animal companion.")]
        public int Wisdom
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Charisma")]
        [Description("Charisma score of the animal companion.")]
        public int Charisma
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Name")]
        [Description("Animal companions name.")]
        public string Name
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("AC")]
        [Description("Total armor class of animal companion")]
        public int AC
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Hit Dice")]
        [Description("Total amount of hit dice.")]
        public string HitDice
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Total HP")]
        [Description("Maximum Companion HP.")]
        public int HP
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Current HP")]
        [Description("Current companion HP.")]
        public int CurrentHP
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Speed")]
        [Description("Animal companion movement speed.")]
        public string Speed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Perception")]
        [Description("Animal companion perception score.")]
        public int Perception
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Senses")]
        [Description("Special senses for animal companion.")]
        public string Senses
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Attack")]
        [Description("Name of companion attack.")]
        public Pair Attack
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Type")]
        [Description("Type of attack the companion will perform.")]
        public Pair Type
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Attack Bonus")]
        [Description("Bonus to attack roll.")]
        public Pair AtkBonus
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Damage")]
        [Description("Damage for attack roll.")]
        public Pair Damage
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Damage Type")]
        [Description("Type of damage attack will cause.")]
        public PresetPair DmgType
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Reach")]
        [Description("Animal companion's total reach.")]
        public Pair Reach
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Companion Details")]
        [DisplayName("Notes")]
        [Description("Any notes for the companion.")]
        public Pair Notes
        {
            get;
            set;
        }

        #endregion

    }
}
