using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCharacterSheet.Characters;
using MyCharacterSheet.Persistence;
using System;

namespace MyCharacterSheetTests.Persistence_Tests
{
    [TestClass]
    public class SaveTests
    {
        private string GetCharacterSheetXml()
        {
            string xml = "";

            xml = Properties.Resources.CalestonTest;

            return xml;
        }

        [TestMethod]
        public void SaveCharacterSheetXml_ShouldWork()
        {
            Character character = new Character();
            string newXml;

            character.LoadCharacterSheetFromString(GetCharacterSheetXml());

            newXml = Save.SaveCharacterSheetToString(character);

            Assert.AreEqual(GetCharacterSheetXml(), newXml);
        }
    }
}
