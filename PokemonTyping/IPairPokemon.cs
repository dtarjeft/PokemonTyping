using System.Collections.Generic;

namespace PokemonTyping
{
    public interface IPairPokemon
    {
        /// <summary>
        /// Evaluates efficacy of attacks against a passed Pokemon.
        /// </summary>
        /// <param name="pokemon">Pokemon to be evaluated.</param>
        /// <returns>A dictionary containing efficacy of PokemonType attacks against passed pokemon.</returns>
        Dictionary<PokemonType, double> EvaluatePokemon(Pokemon pokemon);
    }
}