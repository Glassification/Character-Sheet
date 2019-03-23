using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCharacterSheet;
using MyCharacterSheet.Characters;
using System;

namespace MyCharacterSheetTests.Characters_Tests
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
        public void Copy_AllFields_ShouldWork()
        {
            Character character = new Character();
            Character copyCharacter = new Character();

            character.LoadCharacterSheetFromString(GetTestXml());

            character.Copy(copyCharacter);

            Assert.AreEqual(character.Age, copyCharacter.Age);
        }
    }
}
