using System;

namespace PokemonTyping
{
    public class Pokemon
    {
        public PokemonType FirstType { get; private set; }
        public PokemonType SecondType { get; private set; }
        public string Name { get; private set; }
        public int IdNumber { get; private set; }

        /// <summary>
        /// Builds a Pokemon from string representations of its properties.
        /// </summary>
        /// <param name="nationalDexNumber"></param>
        /// <param name="name"></param>
        /// <param name="firstType"></param>
        /// <param name="secondType"></param>
        public Pokemon(string nationalDexNumber, string name, string firstType, string secondType = "Invalid")
        {
            PokemonType pokemonType;
            Enum.TryParse(firstType, true, out pokemonType);
            FirstType = pokemonType;
            Enum.TryParse(secondType, true, out pokemonType);
            SecondType = pokemonType;
            IdNumber = (nationalDexNumber[1] - '0')*100 + 
                (nationalDexNumber[2] - '0')*10 + 
                (nationalDexNumber[3] - '0');
            Name = name;
        }
        
        /// <summary>
        /// Initializes a Pokemon object with all of its properties set to zero/empty strings, as applicable.
        /// Primary use is as a placeholder; Game Freak did not zero-index their Pokemon list.
        /// </summary>
        public Pokemon()
        {
            FirstType = PokemonType.Invalid;
            SecondType = PokemonType.Invalid;
            Name = "";
            IdNumber = 0;
        }

        public override string ToString()
        {
            return string.Format("#{0} : {1}, Type: {2}", IdNumber, Name, 
                (SecondType != PokemonType.Invalid)? string.Format("{0}/{1}", FirstType, SecondType) : FirstType.ToString());
        }
    }
}