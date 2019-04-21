using Microsoft.VisualStudio.TestTools.UnitTesting;
using Concierge;
using Concierge.Characters;
using System;

namespace ConciergeTests.Characters_Tests
{
    [TestClass]
    public class CharacterTests
    {
        private string GetTestXml()
        {
            string xml = "";

            xml = Properties.Resources.CalestonTest;

            return xml;
        }

        [TestMethod]
        public void IsDead_MinDamage_ShouldNotWork()
        {
            Character character = new Character();

            character.LoadCharacterSheetFromString(GetTestXml());
            character.HitPoints.HP = character.HitPoints.MaxHP;

            Assert.IsFalse(character.IsDead(1));
        }

        [TestMethod]
        public void IsDead_MaxDamage_ShouldWork()
        {
            Character character = new Character();

            character.LoadCharacterSheetFromString(GetTestXml());
            character.HitPoints.HP = 1;

            Assert.IsTrue(character.IsDead(character.HitPoints.MaxHP*-2));
        }

        [TestMethod]
        public void ValidTotalLevel_IsValid_ShouldWork()
        {
            Program.Loading = false;
            Program.Character.LoadCharacterSheetFromString(GetTestXml());

            Program.Character.PlayerClass1.ClassLevel = 1;
            Program.Character.PlayerClass2.ClassLevel = 1;
            Program.Character.PlayerClass3.ClassLevel = 0;

            Assert.IsTrue(Program.Character.ValidTotalLevel(1, 2));
        }

        [TestMethod]
        public void ValidTotalLevel_IsNotValid_ShouldNotWork()
        {
            Program.Loading = false;
            Program.Character.LoadCharacterSheetFromString(GetTestXml());

            Program.Character.PlayerClass1.ClassLevel = 10;
            Program.Character.PlayerClass2.ClassLevel = 10;
            Program.Character.PlayerClass3.ClassLevel = 0;

            Assert.IsFalse(Program.Character.ValidTotalLevel(1, 2));
        }

        [TestMethod]
        public void ValidName_IsValid_ShouldWork()
        {
            Program.Loading = false;
            Program.Character.LoadCharacterSheetFromString(GetTestXml());

            Program.Character.PlayerClass1.ClassName = "Test 1";
            Program.Character.PlayerClass2.ClassName = "Test 2";

            Assert.IsTrue(Program.Character.ValidName("Test 3", 2));
        }

        [TestMethod]
        public void ValidName_IsNotValid_ShouldNotWork()
        {
            Program.Loading = false;
            Program.Character.LoadCharacterSheetFromString(GetTestXml());

            Program.Character.PlayerClass1.ClassName = "Test 1";
            Program.Character.PlayerClass2.ClassName = "Test 2";

            Assert.IsFalse(Program.Character.ValidName("Test 2", 2));
        }

        [TestMethod]
        public void Copy_AllFields_ShouldWork()
        {
            Character character = new Character();
            Character copyCharacter = new Character();

            character.LoadCharacterSheetFromString(GetTestXml());

            character.Copy(copyCharacter);

            // Test Properties
            Assert.AreEqual(character.Age, copyCharacter.Age);
            Assert.AreEqual(character.Alignment, copyCharacter.Alignment);
            Assert.AreEqual(character.Armor, copyCharacter.Armor);
            Assert.AreEqual(character.Background, copyCharacter.Background);
            Assert.AreEqual(character.Bond, copyCharacter.Bond);
            Assert.AreEqual(character.Charisma, copyCharacter.Charisma);
            Assert.AreEqual(character.ClassResource, copyCharacter.ClassResource);
            Assert.AreEqual(character.Constitution, copyCharacter.Constitution);
            Assert.AreEqual(character.CP, copyCharacter.CP);
            Assert.AreEqual(character.Dexterity, copyCharacter.Dexterity);
            Assert.AreEqual(character.EP, copyCharacter.EP);
            Assert.AreEqual(character.EXP, copyCharacter.EXP);
            Assert.AreEqual(character.EyeColour, copyCharacter.EyeColour);
            Assert.AreEqual(character.Flaw, copyCharacter.Flaw);
            Assert.AreEqual(character.Gender, copyCharacter.Gender);
            Assert.AreEqual(character.GP, copyCharacter.GP);
            Assert.AreEqual(character.HairColour, copyCharacter.HairColour);
            Assert.AreEqual(character.Height, copyCharacter.Height);
            Assert.AreEqual(character.Ideal, copyCharacter.Ideal);
            Assert.AreEqual(character.InitiativeBonus, copyCharacter.InitiativeBonus);
            Assert.AreEqual(character.Intelligence, copyCharacter.Intelligence);
            Assert.AreEqual(character.Language, copyCharacter.Language);
            Assert.AreEqual(character.Marks, copyCharacter.Marks);
            Assert.AreEqual(character.Movement, copyCharacter.Movement);
            Assert.AreEqual(character.Name, copyCharacter.Name);
            Assert.AreEqual(character.PassivePerceptionBonus, copyCharacter.PassivePerceptionBonus);
            Assert.AreEqual(character.PersonalityBackground, copyCharacter.PersonalityBackground);
            Assert.AreEqual(character.PersonalityNotes, copyCharacter.PersonalityNotes);
            Assert.AreEqual(character.Pool, copyCharacter.Pool);
            Assert.AreEqual(character.PP, copyCharacter.PP);
            Assert.AreEqual(character.Race, copyCharacter.Race);
            Assert.AreEqual(character.Shields, copyCharacter.Shields);
            Assert.AreEqual(character.SkinColour, copyCharacter.SkinColour);
            Assert.AreEqual(character.SP, copyCharacter.SP);
            Assert.AreEqual(character.Spent, copyCharacter.Spent);
            Assert.AreEqual(character.Strength, copyCharacter.Strength);
            Assert.AreEqual(character.Tools, copyCharacter.Tools);
            Assert.AreEqual(character.Trait1, copyCharacter.Trait1);
            Assert.AreEqual(character.Trait2, copyCharacter.Trait2);
            Assert.AreEqual(character.Vision, copyCharacter.Vision);
            Assert.AreEqual(character.Weapons, copyCharacter.Weapons);
            Assert.AreEqual(character.Weight, copyCharacter.Weight);
            Assert.AreEqual(character.Wisdom, copyCharacter.Wisdom);

            // Test classes
            Assert.IsNotNull(copyCharacter.ArmorClass);
            Assert.IsNotNull(copyCharacter.HitPoints);
            Assert.IsNotNull(copyCharacter.Spellcasting);
            Assert.IsNotNull(copyCharacter.Companion);

            // Test Lists
            Assert.AreEqual(character.oWeapons.Count, copyCharacter.oWeapons.Count);
            Assert.AreEqual(character.oAmmo.Count, copyCharacter.oAmmo.Count);
            Assert.AreEqual(character.oInventory.Count, copyCharacter.oInventory.Count);
            Assert.AreEqual(character.oAbility.Count, copyCharacter.oAbility.Count);
            Assert.AreEqual(character.Spellcasting.oMagic.Count, copyCharacter.Spellcasting.oMagic.Count);
            Assert.AreEqual(character.Spellcasting.oSpells.Count, copyCharacter.Spellcasting.oSpells.Count);
        }
    }
}
