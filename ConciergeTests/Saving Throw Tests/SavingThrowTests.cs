using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCharacterSheet;
using MyCharacterSheet.SavingThrowsNamespace;
using System;

namespace MyCharacterSheetTests.Saving_Throw_Tests
{
    [TestClass]
    public class SavingThrowTests
    {
        private void SetupCharacter()
        {
            Program.Character.CreateCharacterSheet();
        }

        [TestMethod]
        public void Bonus_CharismaSpecified_ShouldWork()
        {
            SetupCharacter();

            SavingThrows charisma = new Charisma(true);

            Assert.AreEqual(-5, charisma.Bonus);
            Assert.IsTrue(charisma.Proficiency);
        }

        [TestMethod]
        public void Bonus_ConstitutionSpecified_ShouldWork()
        {
            SetupCharacter();

            SavingThrows constitution = new Constitution(true);

            Assert.AreEqual(-5, constitution.Bonus);
            Assert.IsTrue(constitution.Proficiency);
        }

        [TestMethod]
        public void Bonus_DexteritySpecified_ShouldWork()
        {
            SetupCharacter();

            SavingThrows dexterity = new Dexterity(true);

            Assert.AreEqual(-5, dexterity.Bonus);
            Assert.IsTrue(dexterity.Proficiency);
        }

        [TestMethod]
        public void Bonus_IntelligenceSpecified_ShouldWork()
        {
            SetupCharacter();

            SavingThrows intelligence = new Intelligence(true);

            Assert.AreEqual(-5, intelligence.Bonus);
            Assert.IsTrue(intelligence.Proficiency);
        }

        [TestMethod]
        public void Bonus_StrengthSpecified_ShouldWork()
        {
            SetupCharacter();

            SavingThrows strength = new Strength(true);

            Assert.AreEqual(-5, strength.Bonus);
            Assert.IsTrue(strength.Proficiency);
        }

        [TestMethod]
        public void Bonus_WisdomSpecified_ShouldWork()
        {
            SetupCharacter();

            SavingThrows wisdom = new Wisdom(true);

            Assert.AreEqual(-5, wisdom.Bonus);
            Assert.IsTrue(wisdom.Proficiency);
        }
    }
}
