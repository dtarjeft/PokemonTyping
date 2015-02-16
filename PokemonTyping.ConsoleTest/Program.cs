using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace PokemonTyping.ConsoleTest
{
    class Program
    {
        static void Main()
        {
            var pokeDex = new BulbapediaPokedex();
            var pairer = new SerebiiPairUp();

            var testPokemon = pokeDex.GetPokemon("Shuckle");
            var output = pairer.EvaluatePokemon(testPokemon);
            var outputList = output.ToList();
            outputList.Sort((x, y) => y.Value.CompareTo(x.Value));
            Console.WriteLine(testPokemon.ToString());
            foreach (var pair in outputList)
            {
                Console.WriteLine(pair);
            }
        }
    }
}
