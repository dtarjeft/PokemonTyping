using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace PokemonTyping.ConsoleTest
{
    class Program
    {
        static void CommaValuedPokemonFile(string filePath, IPokedex pokedex)
        {
            var pokemonString = new StringBuilder();
            foreach (var pokemon in pokedex.Pokemons)
            {
                pokemonString.AppendFormat("{0},{1},{2},{3}"+'\n', pokemon.IdNumber, pokemon.Name, pokemon.FirstType,
                    pokemon.SecondType);
            }
            File.WriteAllText(filePath, pokemonString.ToString());
        }

        static void CommaValuedPairUps(string filePath, IPokedex pokedex, IPairPokemon pairPokemon)
        {
            // Pure Pokemon of each type.
            var pureTypes = new List<string>
            {
                "missingno",
                "Rattata",
                "Charmander",
                "Squirtle",
                "Pikachu",
                "Bulbasaur",
                "Glalie",
                "Machop",
                "Ekans",
                "Sandshrew",
                "Tornadus", //Holy hell, there's actually only one pure flying Pokemon?
                "Alakazam",
                "Caterpie",
                "Sudowoodo",
                "Mismagius",
                "Dratini",
                "Umbreon",
                "Registeel",
                "Clefairy"
            };

            var toFile = new StringBuilder();
            for (var i = 1; i < pureTypes.Count; i++)
            {
                var pokemon = pokedex.GetPokemon(pureTypes[i]);
                toFile.Append(pokemon.FirstType);
                var pairResult = pairPokemon.EvaluatePokemon(pokemon).ToList();
                foreach (var keyValuePair in pairResult)
                {
                    toFile.AppendFormat("|{0},{1}", keyValuePair.Key, keyValuePair.Value);
                }
                toFile.AppendLine();
            }
            File.WriteAllText(filePath,toFile.ToString());
        }


        static void Main()
        {
            const string pokemonsTxt = ".\\Pokemons.txt";
            const string matchTxt = ".\\Match.txt";

            if (!File.Exists(pokemonsTxt) || !File.Exists(matchTxt))
            {
                var bulbaPokedex = new BulbapediaPokedex();
                CommaValuedPokemonFile(pokemonsTxt, bulbaPokedex);
                var csvPairUp = new SerebiiPairUp();
                CommaValuedPairUps(matchTxt, bulbaPokedex, csvPairUp);
            }

            var pokeDex = new CommaSeparatedFilePokedex(pokemonsTxt);
            var pairer = new CommaSeparatedFilePairPokemon(matchTxt);

            var testPokemon = pokeDex.GetPokemon("Sableye");
            var outputList = pairer.EvaluatePokemon(testPokemon).ToList();
            outputList.Sort((x, y) => y.Value.CompareTo(x.Value));
            Console.WriteLine(testPokemon.ToString());
            foreach (var pair in outputList)
            {
                Console.WriteLine(pair);
            }
        }
    }
}
