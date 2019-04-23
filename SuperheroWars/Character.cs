using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroWars
{
    public class Character
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
        public bool IsVillain { get; set; }
        private static Random randomDamageGenerator = new Random();
        private readonly int MIN_DAMAGE_PERCENTAGE = 60, MAX_DAMAGE_PERCENTAGE = 100;
   
        public void AttackSingleTarget(Character character)
        {
      
            int damage = randomDamageGenerator.Next(MIN_DAMAGE_PERCENTAGE, MAX_DAMAGE_PERCENTAGE) * Attack / 100;
            character.Health -= damage;
            Console.WriteLine("{0} dealt {1} damage to {2}( remaining health {3})", this.Name, damage, character.Name,character.Health);
            if (character.Health <= 0) Console.WriteLine("{0} died", character.Name);
        }


        public void AttackMultipleTargets(List<Character> characters)
        {

            foreach(Character character in characters)
            {
                AttackSingleTarget(character);
            }

        }
    }
}
