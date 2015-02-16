using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace PokemonTyping
{
    public interface IPokedex
    {
        /// <summary>
        /// Exposes a collection of Pokemon objects.
        /// </summary>
        List<Pokemon> Pokemons { get; }

        /// <summary>
        /// Searches Pokemons for a Pokemon corresponding to pokemonName. 
        /// Example: GetPokemon("Charmander") should return a Pokemon object with Pokemon.Name == "Charmander".
        /// </summary>
        /// <param name="pokemonName">String representation of a Pokemon's name.</param>
        /// <returns>A Pokemon corresponding to the string passed.</returns>
        Pokemon GetPokemon(string pokemonName);
    }
}