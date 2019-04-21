using Microsoft.VisualStudio.TestTools.UnitTesting;
using Concierge;
using Concierge.Divide_Loot;
using System;

namespace ConciergeTests
{
    [TestClass]
    public class DivideLootPageTests
    {
        [TestMethod]
        public void Distribute_MultiplePlayers_ShouldWork()
        {
            DivideLootPage divideLootPage = new DivideLootPage();
            Player loot = new Player(0, 0, 0, 100, 0);

            divideLootPage.players.Add(new Player("Test 1"));
            divideLootPage.players.Add(new Player("Test 2"));
            divideLootPage.players.Add(new Player("Test 3"));
            divideLootPage.players.Add(new Player("Test 4"));

            divideLootPage.Distribute(loot);

            Assert.AreEqual(25, divideLootPage.players[0].Gold);
            Assert.AreEqual(25, divideLootPage.players[1].Gold);
            Assert.AreEqual(25, divideLootPage.players[2].Gold);
            Assert.AreEqual(25, divideLootPage.players[3].Gold);
        }

        [TestMethod]
        public void Distribute_EqualMultipleCurrencies_ShouldWork()
        {
            DivideLootPage divideLootPage = new DivideLootPage();
            Player loot = new Player(10, 10, 10, 10, 10);

            divideLootPage.players.Add(new Player("Test 1"));
            divideLootPage.players.Add(new Player("Test 2"));

            divideLootPage.Distribute(loot);

            Assert.AreEqual(5, divideLootPage.players[0].Copper);
            Assert.AreEqual(5, divideLootPage.players[0].Silver);
            Assert.AreEqual(5, divideLootPage.players[0].Electrum);
            Assert.AreEqual(5, divideLootPage.players[0].Gold);
            Assert.AreEqual(5, divideLootPage.players[0].Platinum);

            Assert.AreEqual(5, divideLootPage.players[1].Copper);
            Assert.AreEqual(5, divideLootPage.players[1].Silver);
            Assert.AreEqual(5, divideLootPage.players[1].Electrum);
            Assert.AreEqual(5, divideLootPage.players[1].Gold);
            Assert.AreEqual(5, divideLootPage.players[1].Platinum);
        }

        [TestMethod]
        public void Distribute_UnequalMultipleCurrencies_ShouldWork()
        {
            DivideLootPage divideLootPage = new DivideLootPage();
            Player loot = new Player(10, 11, 0, 1, 0);

            divideLootPage.players.Add(new Player("Test 1"));
            divideLootPage.players.Add(new Player("Test 2"));

            divideLootPage.Distribute(loot);

            Assert.AreEqual(0, divideLootPage.players[0].Copper);
            Assert.AreEqual(1, divideLootPage.players[0].Silver);
            Assert.AreEqual(0, divideLootPage.players[0].Electrum);
            Assert.AreEqual(1, divideLootPage.players[0].Gold);
            Assert.AreEqual(0, divideLootPage.players[0].Platinum);

            Assert.AreEqual(10, divideLootPage.players[1].Copper);
            Assert.AreEqual(10, divideLootPage.players[1].Silver);
            Assert.AreEqual(0, divideLootPage.players[1].Electrum);
            Assert.AreEqual(0, divideLootPage.players[1].Gold);
            Assert.AreEqual(0, divideLootPage.players[1].Platinum);
        }

        [TestMethod]
        public void Total_AllCurrencies_ShouldWork()
        {
            Player loot = new Player(10, 10, 10, 10, 10);

            Assert.AreEqual(113.10, loot.Total);
        }
    }
}
