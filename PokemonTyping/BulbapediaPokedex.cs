using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace PokemonTyping
{
    public class BulbapediaPokedex: IPokedex
    {
        private const string PokemonListWebsite =
            "http://bulbapedia.bulbagarden.net/wiki/List_of_Pok%C3%A9mon_by_National_Pok%C3%A9dex_number";

        
        
        public Pokemon GetPokemon(string pokemonName)
        {
             return Pokemons.Find(pokemon => String.Equals(pokemon.Name, pokemonName, StringComparison.CurrentCultureIgnoreCase));
        }

        public List<Pokemon> Pokemons { get; private set; }


         /*  
         * //*[@id="mw-content-text"]/table[1+Generation]  
         */
        /// <summary>
        /// Bulbapedia's website divides their Pokemon by generation; this function acquires one generation of
        /// Pokemon and adds it to Pokemons.
        /// </summary>
        /// <param name="generationNumber">Generation of Pokemon to grab. (1-6)</param>
        /// <param name="dexPage">The webpage to grab from.</param>
        void PopulateGeneration(int generationNumber, HtmlDocument dexPage)
        {
            var path =
                "//*[@id='mw-content-text']/table[" + (generationNumber + 1) +
                "]";
            var pokemon = dexPage.DocumentNode.SelectSingleNode(path);
            if (pokemon.ChildNodes == null)
            {
                return;
            }
            foreach (var row in pokemon.ChildNodes)
            {   
                var fields = row.ChildNodes;
                var pokemonPropertiesList = new List<string>();
                if (fields != null)
                {
                    foreach (var content in fields.Select(tableDivision => tableDivision.ChildNodes)) 
                    {
                        pokemonPropertiesList.AddRange(content.Select(
                            text =>
                                text.InnerText.Split(new[] { '\n', '\0', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .FirstOrDefault()).Where(output => output != null));
                    }
                }
                if (pokemonPropertiesList.Count <= 0)
                {
                    continue;
                }
                if (pokemonPropertiesList[1] == "Ndex")
                {
                    continue;
                }
                if (pokemonPropertiesList.Count == 5)
                {
                    Pokemons.Add(new Pokemon(pokemonPropertiesList[1], pokemonPropertiesList[2],
                        pokemonPropertiesList[3], pokemonPropertiesList[4]));
                }
                else
                {
                    Pokemons.Add(new Pokemon(pokemonPropertiesList[1], pokemonPropertiesList[2],
                        pokemonPropertiesList[3]));
                }
            }
        }

        public BulbapediaPokedex()
        {
            var dexWebPageHelper = new HtmlWeb();
            var dexWebPage = dexWebPageHelper.Load(PokemonListWebsite);
            Pokemons = new List<Pokemon> {new Pokemon()};

            for (var i = 1; i < 7; i++)
            {
                PopulateGeneration(i, dexWebPage);
            }
        }
    }
}