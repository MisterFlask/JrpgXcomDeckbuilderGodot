using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assets.CodeAssets.GameLogic
{
    /// <summary>
    /// Refers to anything we want to manufacture tooltips on observing in a card description or something.
    /// </summary>
    public class MagicWord
    {
        public virtual string MagicWordTitle { get; }
        public virtual string MagicWordDescription { get; }

        private static List<MagicWord> MagicWordsRegistered = new List<MagicWord>();

        private static void RegisterMagicWord(MagicWord word)
        {
            if (MagicWordsRegistered.Any(item => item.MagicWordTitle == word.MagicWordTitle))
            {
                return;
            }
            MagicWordsRegistered.Add(word);
        }

        public static void RegisterMagicWordsReflectively()
        {
            var types = Assembly
              .GetExecutingAssembly()
              .GetTypes();

            foreach (var t in types)
            {
                if (t.IsSubclassOf(typeof(MagicWord)) && !t.IsAbstract)
                {
                    RegisterMagicWord(Activator.CreateInstance(t) as MagicWord);
                }
            }
        }


        public static string FormatMagicWords(List<MagicWord> magicWords)
        {
            string value = "";
            foreach(var word in magicWords)
            {
                value += $"<color=green>{word.MagicWordTitle}</color>: {word.MagicWordDescription}\n";
            }
            return value;
        }

        public static List<MagicWord> GetApplicableMagicWordsForString(string stringToAnalyze)
        {
            if (stringToAnalyze == null)
            {
                return new List<MagicWord>();
            }
            var relevantMagicWords = MagicWordsRegistered
                .Where(item => item.MagicWordTitle != null && stringToAnalyze.Contains(item.MagicWordTitle));
            return relevantMagicWords.ToList();
        }
        public static List<MagicWord> GetMagicWordsApplicableToCard(AbstractCard card)
        {
            return GetApplicableMagicWordsForString(card.DescriptionInner());
        }

        public static List<MagicWord> GetMagicWordsApplicableToStatusEffect(AbstractStatusEffect effect)
        {
            return GetApplicableMagicWordsForString(effect.Description);
        }
        public static string GetFormattedMagicWordsForCard(AbstractCard card)
        {
            return FormatMagicWords(GetMagicWordsApplicableToCard(card));
        }

    }
}