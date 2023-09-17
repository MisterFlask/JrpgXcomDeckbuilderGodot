
using System;
using System.Collections.Generic;
using System.Linq;
public static class ExtensionMethods
{

    public static void ForEachCreateActionToBack<T>(this IEnumerable<T> items, string name, Action<T> toPerform)
    {
        foreach (var item in items)
        {
            ActionManager.Instance.PushActionToBack("ForEachCreateActionToBack_" + name, () => toPerform(item));
        }
    }

    public static bool IsExhausted(this AbstractCard card)
    {
        return GameState.Instance.Deck.ExhaustPile.Contains(card);
    }
    public static bool IsInDrawPile(this AbstractCard card)
    {
        return GameState.Instance.Deck.DrawPile.Contains(card);
    }
    public static bool IsInDiscardPile(this AbstractCard card)
    {
        return GameState.Instance.Deck.DiscardPile.Contains(card);
    }

    public static IEnumerable<T> TakeUpTo<T>(this IEnumerable<T> list, int num)
    {
        var count = list.Count();
        return list.Take(count);
    }

    public static void InsertIntoBeginning<T>(this List<T> list, T item)
    {
        list.Insert(0, item);
    }

    public static void InsertIntoRandomLocation<T>(this List<T> list, T item)
    {
        var randomIndex = new Random().Next(0, list.Count);

        list.Insert(randomIndex, item);
    }

    public static List<AbstractBattleUnit> ConvertGuidsToSoldiers(this IEnumerable<string> guids)
    {
        return guids
         .Select(item => GameState.Instance.PersistentCharacterRoster
         .Single(soldierInRoster => soldierInRoster.UniqueId == item))
         .ToList();
    }
    public static List<AbstractCard> ConvertToCards(this IEnumerable<string> guids)
    {
        return guids
         .Select(item => GameState.Instance.PersistentCharacterRoster.SelectMany(character => character.CardsInPersistentDeck)
         .Single(card => card.Id == item))
         .ToList();
    }




    #region collections
    public static bool In<T>(this T item, IEnumerable<T> coll)
    {
        return coll.Contains(item);
    }
    public static bool NotIn<T>(this T item, IEnumerable<T> coll)
    {
        return !coll.Contains(item);
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).SingleOrDefault();
    }
    public static T PickRandomWhere<T>(this IEnumerable<T> source, Predicate<T> required)
    {
        return source.Where(item => required(item)).PickRandom(1).SingleOrDefault();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        if (count > source.Count())
        {
            count = source.Count();
        }

        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }

    public static T PopFirstElement<T>(this IList<T> items)
    {
        if (items.Count == 0)
        {
            throw new Exception("No items left in this list");
        }
        var last = items.First();
        items.RemoveAt(0);
        return last;
    }

    public static bool IsEmpty<T>(this IEnumerable<T> items)
    {
        if (items == null) return true;
        if (items.Count() == 0)
        {
            return true;
        }
        return false;
    }

    public static List<T> Multiply<T>(this IEnumerable<T> items, int multiplyBy)
    {
        var newList = new List<T>();
        for (int i = 0; i < multiplyBy; i++)
        {
            newList.AddRange(items);
        }
        return newList;
    }

    public static List<T> TakeUnique<T>(this List<T> items, int numItems)
    {
        var newList = new List<T>();
        int itemsLeft = numItems;
        for (int i = 0; i < items.Count(); i++)
        {
            if (itemsLeft == 0)
            {
                return newList;
            }
            if (!newList.Contains(items[i]))
            {
                newList.Add(items[i]);
                itemsLeft--;
            }
        }
        return newList;
    }

    #endregion


    public static bool ContainsAll<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        foreach (var item in second)
        {
            if (!first.Contains(item))
            {
                return false;
            }
        }
        return true;

    }

    public static bool EquivalentMembers<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        return first.ContainsAll(second) && second.ContainsAll(first);
    }

    public static string GetCardNames(this IEnumerable<AbstractCard> cards)
    {
        var cardNames = cards.Select(item => item.Name).ToList();
        var aggregated = cardNames.Aggregate((item1, item2) => item1 + ", " + item2);
        return $"[{aggregated}]";

    }

    public static List<T> DedupeAndReorder<T>(this IEnumerable<T> toDedupe)
    {
        return toDedupe.ToHashSet().ToList();
    }


    public static List<T> SelectRandomFraction<T>(this IEnumerable<T> source, float percentageAsFraction)
    {
        if (percentageAsFraction > 1 || percentageAsFraction < 0)
        {
            throw new Exception("Required to express random fraction as a number between 0 and 1");
        }
        var list = new List<T>();
        foreach (var item in source)
        {
            if (new Random().NextDouble() < percentageAsFraction)
            {
                list.Add(item);
            }
        }
        return list;
    }



    public static string AsString<T>(this IEnumerable<T> list, Func<T, string> stringifyFunction)
    {
        if (list == null)
        {
            return "null";
        }

        var ret = "{";
        foreach (var item in list)
        {
            ret += stringifyFunction(item) + ", ";
        }
        ret += "}";
        return ret;
    }
    public static string AsString<T>(this IEnumerable<T> list)
    {
        if (list == null)
        {
            return "null";
        }

        var ret = "{";
        foreach (var item in list)
        {
            ret += item.ToString() + ", ";
        }
        ret += "}";
        return ret;
    }



    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
    {
        var set = new HashSet<T>();
        foreach (var item in source)
        {
            set.Add(item);
        }
        return set;

    }

    public static List<T> WhereNotNull<T>(this IEnumerable<T> itemCollection)
    {
        return itemCollection.Where(item => item != null).ToList();
    }

    public static List<T> ToSingletonList<T>(this T item)
    {
        if (item == null)
        {
            return new List<T>();
        }
        return new List<T> { item };
    }

}