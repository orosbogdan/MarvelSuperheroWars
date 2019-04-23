using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperheroWars;

namespace MarvelTests
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void AtLeastOneHeroShouldDie()
        {

            Planet planet = new Planet()
            {
                Id = 1,
                Description = "Very cold",
                Name = "Pluto",
                Modifiers = new Modifier()
                {
                    heroAttackModifier = 0,
                    heroHealthModifier = 0,
                    villainAttackModifier = 0,
                    villainHealthModifier = 0

                }
            };


            Character villain = new Character()
            {
                Id = 1,
                Name = "Loki",
                Description = "Norse",
                Attack = 100,
                Health = 100,
                IsVillain = true

            };

            Character hero = new Character()
            {
                Id = 2,
                Name = "Superman",
                Description = "Powerful",
                Attack = 1,
                Health = 60,
                IsVillain = true

            };


            Character hero2 = new Character()
            {
                Id = 2,
                Name = "Superman",
                Description = "Powerful",
                Attack = 1,
                Health = 360,
                IsVillain = true

            };

            // even with the lowest roll of 60% of dmg the villain should kill the hero in 1 shot


            BattleWorld battleWorld = new BattleWorld();

            List<Character> heroes = new List<Character>();
            heroes.Add(hero);

            BattleWorld.Battle(heroes, villain, planet);

            int heroesSurviving = heroes.Where(x => x.Health > 0).Count();
            Assert.IsTrue(heroesSurviving < 2);

        }




        [TestMethod]
        public void VillainShouldAlwaysWin()
        {
            Planet planet = new Planet()
            {
                Id = 1,
                Description = "Very cold",
                Name = "Pluto",
                Modifiers = new Modifier()
                {
                    heroAttackModifier = 0,
                    heroHealthModifier = 0,
                    villainAttackModifier = 0,
                    villainHealthModifier = 0

                }
            };


            Character villain = new Character()
            {
                Id = 1,
                Name = "Loki",
                Description = "Norse",
                Attack = 100,
                Health = 100,
                IsVillain = true

            };

            Character hero = new Character()
            {
                Id = 2,
                Name = "Superman",
                Description = "Powerful",
                Attack = 1,
                Health = 60,
                IsVillain = true

            };

            // even with the lowest roll of 60% of dmg the villain should kill the hero in 1 shot


            BattleWorld battleWorld = new BattleWorld();

            List<Character> heroes = new List<Character>();
            heroes.Add(hero);

            BattleWorld.Battle(heroes, villain, planet);

            Assert.IsFalse(heroes.Any(x => x.Health > 0));
        }
    }
}
