using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroWars
{
    public class BattleWorld
    {
        private static string charactersFilename = "characters.json";

        private static string planetsFileName = "planets.json";

        /***
         * This function returns true if 
         * the heroes won, else it returns false
         ***/       
        public static bool Battle(IEnumerable<Character> heroes, Character villain, Planet planet)
        {
            bool alternation = false;

            heroes.ToList().ForEach(x => {
                x.Health += planet.Modifiers.heroHealthModifier;
                x.Attack += planet.Modifiers.heroAttackModifier;
            });


            villain.Attack += planet.Modifiers.villainAttackModifier;
            villain.Health += planet.Modifiers.villainHealthModifier;


            while (heroes.Any(x => x.Health > 0) && villain.Health > 0)
            {
                if (alternation)
                {
                    foreach (Character hero in heroes)
                    {
                        if (hero.Health > 0 && villain.Health>0)
                            hero.AttackSingleTarget(villain);
                    }
                }
                else
                {
                    villain.AttackMultipleTargets(heroes.Where(x => x.Health > 0).ToList());
                }

                alternation = !alternation;
            }

            if (villain.Health <= 0) { Console.WriteLine("The hero(es) won");  return true; }
            else { Console.WriteLine("The villain won"); return false; }


        }

        private static Character SelectVillain(IEnumerable<Character> villains)
        {
            Character villainSelected = new Character();

            Console.WriteLine("Pick a villain by id");

            foreach (Character character in villains)
            {
                Console.WriteLine("{0} {1}", character.Id, character.Name);
            }



            Boolean pickingVillain = true;
            while (pickingVillain)
            {

                int villainIdSelected = 0;
                if (int.TryParse(Console.ReadLine(), out villainIdSelected))
                {
                    if (villains.Any(x => x.Id == villainIdSelected))
                    {
                        pickingVillain = false;
                        villainSelected = villains.Single(x => x.Id == villainIdSelected);
                    }
                    else
                        Console.WriteLine("The villain id is invalid, please select from the list above.");
                }
                else
                    Console.WriteLine("Invalid number entered, please pick a valid planet id");

            }

            return villainSelected;
        }

        private static Planet SelectPlanet(IEnumerable<Planet> planets)
        {
            Planet planetSelected = new Planet();
            Console.WriteLine("Pick a planet");

            foreach (Planet planet in planets)
            {
                Console.Write(planet.ToString());
            }

            Boolean pickingPlanet = true;
            while (pickingPlanet)
            {
                int planetIdRead = 0;
                if (int.TryParse(Console.ReadLine(), out planetIdRead))
                {
                    if (planets.Any(x => x.Id == planetIdRead))
                    {
                        pickingPlanet = false;
                        planetSelected = planets.Single(x => x.Id == planetIdRead);

                    }
                    else Console.WriteLine("Planet id does not exist");
                }
                else Console.WriteLine("Invalid number entered, please pick a valid planet id");

            }

            Console.WriteLine("The fight will take place on {0}", planetSelected.Name);
            return planetSelected;
        }

        private static IEnumerable<Character> SelectHeroes(IEnumerable<Character> heroes)
        {
            List<Character> heroesSelected = new List<Character>();
            int noOfHeroesSelected = 0;
            bool selecting = true;

            foreach (Character character in heroes)
            {
                Console.WriteLine("{0} {1}", character.Id, character.Name);
            }


            Console.WriteLine("Enter the number of heroes you want to use");


            while (selecting)
                if (int.TryParse(Console.ReadLine(), out noOfHeroesSelected))
                {

                    selecting = false;
                }
                else Console.WriteLine("Please enter a number between 1 and total number of heroes");

            Console.WriteLine("Now select {0} heroes by id", noOfHeroesSelected);

            while (noOfHeroesSelected > 0)
            {
                int heroIdSelected = 0;
                if (int.TryParse(Console.ReadLine(), out heroIdSelected))
                {

                    if (heroes.Any(x => x.Id == heroIdSelected))
                    {
                        heroesSelected.Add(heroes.Single(x => x.Id == heroIdSelected));
                        noOfHeroesSelected--;
                    }
                    else
                        Console.WriteLine("The hero id is invalid, please select from the list above.");

               
                }
                else Console.WriteLine("Invalid number entered, please pick a valid hero id");

            }

            return heroesSelected;
        }


        static void Main(string[] args)
        {
            IEnumerable<Character> characters = JsonConvert.DeserializeObject<IEnumerable<Character>>(File.ReadAllText(charactersFilename));
            IEnumerable<Planet> planets = JsonConvert.DeserializeObject<IEnumerable<Planet>>(File.ReadAllText(planetsFileName));

            IEnumerable<Character> villains = characters.AsQueryable().Where(x => x.IsVillain == true).OrderBy(x => x.Name);
            IEnumerable<Character> heroes = characters.AsQueryable().Where(x => x.IsVillain == false).OrderBy(x => x.Name);

            Planet planetSelected = SelectPlanet(planets);
            Character villainSelected = SelectVillain(villains);
            IEnumerable<Character> heroesSelected = SelectHeroes(heroes);

            Battle(heroesSelected, villainSelected, planetSelected);

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
