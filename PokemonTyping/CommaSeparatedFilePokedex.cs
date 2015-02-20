using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokemonTyping
{
    public class CommaSeparatedFilePokedex : IPokedex
    {
        public List<Pokemon> Pokemons { get; private set; }

        public CommaSeparatedFilePokedex(string filePath)
        {
            Pokemons = new List<Pokemon>();

            var pokemonText = File.ReadAllText(filePath).Split(new []{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var input in pokemonText.Select(rawPokemon => rawPokemon.Split(',')))
            {
                Pokemons.Add(new Pokemon(input[0],input[1],input[2],input[3]));
            }
        }

        public Pokemon GetPokemon(string pokemonName)
        {
            return Pokemons.Find(pokemon => String.Equals(pokemon.Name, pokemonName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}