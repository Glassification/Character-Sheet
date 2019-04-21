using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Concierge;
using Concierge.SkillsNamespace;

namespace ConciergeTests.Skills_Tests
{
    [TestClass]
    public class SkillsTests
    {
        private void SetupCharacter()
        {
            Program.Character.CreateCharacterSheet();
        }

        [TestMethod]
        public void Bonus_AcrobaticsSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills acrobatics = new Acrobatics(true, true);

            Assert.AreEqual(-5, acrobatics.Bonus);
            Assert.IsTrue(acrobatics.Proficiency);
            Assert.IsTrue(acrobatics.Expertise);
        }

        [TestMethod]
        public void Bonus_AnimalHandlingSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills animalHandling = new AnimalHandling(true, true);

            Assert.AreEqual(-5, animalHandling.Bonus);
            Assert.IsTrue(animalHandling.Proficiency);
            Assert.IsTrue(animalHandling.Expertise);
        }

        [TestMethod]
        public void Bonus_ArcanaSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills arcana = new Arcana(true, true);

            Assert.AreEqual(-5, arcana.Bonus);
            Assert.IsTrue(arcana.Proficiency);
            Assert.IsTrue(arcana.Expertise);
        }

        [TestMethod]
        public void Bonus_AthleticsSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills athletics = new Athletics(true, true);

            Assert.AreEqual(-5, athletics.Bonus);
            Assert.IsTrue(athletics.Proficiency);
            Assert.IsTrue(athletics.Expertise);
        }

        [TestMethod]
        public void Bonus_DeceptionSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills deception = new Deception(true, true);

            Assert.AreEqual(-5, deception.Bonus);
            Assert.IsTrue(deception.Proficiency);
            Assert.IsTrue(deception.Expertise);
        }

        [TestMethod]
        public void Bonus_HistorySpecified_ShouldWork()
        {
            SetupCharacter();

            Skills history = new History(true, true);

            Assert.AreEqual(-5, history.Bonus);
            Assert.IsTrue(history.Proficiency);
            Assert.IsTrue(history.Expertise);
        }

        [TestMethod]
        public void Bonus_InsightSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills insight = new Insight(true, true);

            Assert.AreEqual(-5, insight.Bonus);
            Assert.IsTrue(insight.Proficiency);
            Assert.IsTrue(insight.Expertise);
        }

        [TestMethod]
        public void Bonus_IntimidationSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills intimidation = new Intimidation(true, true);

            Assert.AreEqual(-5, intimidation.Bonus);
            Assert.IsTrue(intimidation.Proficiency);
            Assert.IsTrue(intimidation.Expertise);
        }

        [TestMethod]
        public void Bonus_InvestigationSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills investigation = new Investigation(true, true);

            Assert.AreEqual(-5, investigation.Bonus);
            Assert.IsTrue(investigation.Proficiency);
            Assert.IsTrue(investigation.Expertise);
        }

        [TestMethod]
        public void Bonus_MedicineSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills medicine = new Medicine(true, true);

            Assert.AreEqual(-5, medicine.Bonus);
            Assert.IsTrue(medicine.Proficiency);
            Assert.IsTrue(medicine.Expertise);
        }

        [TestMethod]
        public void Bonus_NatureSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills nature = new Nature(true, true);

            Assert.AreEqual(-5, nature.Bonus);
            Assert.IsTrue(nature.Proficiency);
            Assert.IsTrue(nature.Expertise);
        }

        [TestMethod]
        public void Bonus_PerceptionSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills perception = new Perception(true, true);

            Assert.AreEqual(-5, perception.Bonus);
            Assert.IsTrue(perception.Proficiency);
            Assert.IsTrue(perception.Expertise);
        }

        [TestMethod]
        public void Bonus_PerformanceSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills performance = new Performance(true, true);

            Assert.AreEqual(-5, performance.Bonus);
            Assert.IsTrue(performance.Proficiency);
            Assert.IsTrue(performance.Expertise);
        }

        [TestMethod]
        public void Bonus_PersuasionSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills persuasion = new Persuasion(true, true);

            Assert.AreEqual(-5, persuasion.Bonus);
            Assert.IsTrue(persuasion.Proficiency);
            Assert.IsTrue(persuasion.Expertise);
        }

        [TestMethod]
        public void Bonus_ReligionSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills religion = new Religion(true, true);

            Assert.AreEqual(-5, religion.Bonus);
            Assert.IsTrue(religion.Proficiency);
            Assert.IsTrue(religion.Expertise);
        }

        [TestMethod]
        public void Bonus_SlightOfHandSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills sleightOfHand = new SleightOfHand(true, true);

            Assert.AreEqual(-5, sleightOfHand.Bonus);
            Assert.IsTrue(sleightOfHand.Proficiency);
            Assert.IsTrue(sleightOfHand.Expertise);
        }

        [TestMethod]
        public void Bonus_StealthSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills stealth = new Stealth(true, true);

            Assert.AreEqual(-5, stealth.Bonus);
            Assert.IsTrue(stealth.Proficiency);
            Assert.IsTrue(stealth.Expertise);
        }

        [TestMethod]
        public void Bonus_SurvivalSpecified_ShouldWork()
        {
            SetupCharacter();

            Skills survival = new Survival(true, true);

            Assert.AreEqual(-5, survival.Bonus);
            Assert.IsTrue(survival.Proficiency);
            Assert.IsTrue(survival.Expertise);
        }
    }
}
