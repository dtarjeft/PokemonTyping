using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokemonTyping
{
    public class CommaSeparatedFilePairPokemon: IPairPokemon
    {
        public Dictionary<PokemonType, double> EvaluatePokemon(Pokemon pokemon)
        {
            Dictionary<PokemonType, double> output;
            _pairMasterDictionary.TryGetValue(pokemon.FirstType, out output);
            if (pokemon.SecondType == PokemonType.Invalid)
            {
                return output;
            }
            for (var i = PokemonType.Normal; i < PokemonType.Fairy; i++)
            {
                output[i] *= _pairMasterDictionary[pokemon.SecondType][i];
            }
            return output;
        }

        private readonly Dictionary<PokemonType, Dictionary<PokemonType, double>> _pairMasterDictionary; 

        public CommaSeparatedFilePairPokemon(string filePath)
        {
            _pairMasterDictionary = new Dictionary<PokemonType, Dictionary<PokemonType, double>>();
            var strings = File.ReadAllText(filePath).Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var matches in strings.Select(s => s.Split('|')))
            {
                PokemonType matchType;
                Enum.TryParse(matches[0], out matchType);
                var matchdict = new Dictionary<PokemonType, double>();
                for (var i = PokemonType.Normal; i < PokemonType.Fairy; i++)
                {
                    var split = matches[(int)i].Split(',');
                    matchdict.Add(i, Convert.ToDouble(split[1]));
                }
                _pairMasterDictionary.Add(matchType, matchdict);
            }
        }
    }
}