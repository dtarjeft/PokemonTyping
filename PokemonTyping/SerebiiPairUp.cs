using System.Collections.Generic;
using HtmlAgilityPack;

namespace PokemonTyping
{
    public class SerebiiPairUp : IPairPokemon
    {
        private const string PokemonTypeWebsite = "http://www.serebii.net/xy/typechart.shtml";
        public Dictionary<PokemonType, double> EvaluatePokemon(Pokemon pokemon)
        {
            var result = new Dictionary<PokemonType, double>();
            var serebiiTable = new HtmlWeb();
            var matchupPage = serebiiTable.Load(PokemonTypeWebsite);

            for (var i = PokemonType.Normal; i <= PokemonType.Fairy; i++)
            {
                result.Add(i, EvaluatePokemonHelper(pokemon, i, matchupPage));
            }
            return result;
        }

        /// <summary>
        /// Calls DoMatch once (or twice if pokemon has a SecondType != Invalid). Calculates potency of an attack type against a pokemon.
        /// </summary>
        /// <param name="pokemon">Pokemon being attacked.</param>
        /// <param name="attackType" />
        /// <param name="matchupPage"></param>
        /// <returns>Efficacy of attackType against pokemon.</returns>
        private static double EvaluatePokemonHelper(Pokemon pokemon, PokemonType attackType, HtmlDocument matchupPage)
        {
            var primaryType = pokemon.FirstType;
            var secondaryType = pokemon.SecondType;
            var matchup = 1.0;
            matchup *= DoMatch(primaryType, attackType, matchupPage);
            if (secondaryType != PokemonType.Invalid)
            {
                matchup *= DoMatch(secondaryType, attackType, matchupPage);
            }
            return matchup;
        }

        /// <summary>
        /// Goes to serebii.net and pulls an appropriate damage multiplier from their type matchup chart.
        /// </summary>
        /// <param name="passedType">One of the defending Pokemon's Types</param>
        /// <param name="attackType" />
        /// <param name="matchupPage"></param>
        /// <returns>Potency multiplier of attack.</returns>
        private static double DoMatch(PokemonType passedType, PokemonType attackType, HtmlDocument matchupPage)
        {
            if (passedType == PokemonType.Invalid)
            {
                return 1.0;
            }
            var pathString = string.Format("//tr[{0}]/td[{1}]//img", (int)(1 + passedType),
                (int)(1 + attackType));

            var childNode = matchupPage.DocumentNode.SelectSingleNode(pathString);
            if (childNode == null)
            {
                return 1.0;
            }
            if (childNode.Attributes["title"] == null)
            {
                return 1.0;
            }
            var multiplier = childNode.Attributes["title"].Value;

            switch (multiplier)
            {
                case ("No Damage"):
                    return 0;
                case ("*2 Damage"):
                    return 2.0;
                case ("*0.5 Damage"):
                    return 0.5;
                default:
                    return 1.0;
            }
            /*/html/body/table[2]/tbody/tr[2]/td[2]/font/div[3]/table/tbody/tr[2]/td/table/tbody/tr[2]/td[2] Serebii's XPath for Normal/Normal (1/1)
            * /html/body/table[2]/tbody/tr[2]/td[2]/font/div[3]/table/tbody/tr[2]/td/table/tbody/tr[2]/td[3] Normal/Fire (1/2)
            * /html/body/table[2]/tbody/tr[2]/td[2]/font/div[3]/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3] Fire/Fire (2/2) 
            */
        }
    }
}