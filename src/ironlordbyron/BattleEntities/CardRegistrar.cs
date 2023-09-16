using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class CardRegistrar 
{

    private static bool initialized = false;

    private static Dictionary<string, List<AbstractCard>> ReflectiveCardCache = new Dictionary<string, List<AbstractCard>>();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static void InitCardsReflectively()
    {
        ReflectiveCardCache.Clear();
        var types = Assembly
          .GetExecutingAssembly()
          .GetTypes();

        foreach (var t in types)
        {
            if (t.IsSubclassOf(typeof(AbstractCard)))
            {
                var cardType = t; 
                var card = Activator.CreateInstance(cardType) as AbstractCard;

                if (card.SoldierClassCardPools == null)
                {
                    continue;
                }

                foreach (var soldierClass in card.SoldierClassCardPools)
                {
                    if (soldierClass == null || soldierClass.Name == null)
                    {
                        continue;
                    }
                    if (!ReflectiveCardCache.ContainsKey(soldierClass.Name))
                    {
                        ReflectiveCardCache[soldierClass.Name] = new List<AbstractCard>();
                    }

                    ReflectiveCardCache[soldierClass.Name].Add(card);
                }
                Debug.Log($"Registered card {t?.Name} with soldier classes {card?.SoldierClassCardPools?.AsString(item => item?.Name)}");
            }
        }
        foreach (var clazz in ReflectiveCardCache.Keys)
        {
            Debug.Log($"Cards for {clazz}: {ReflectiveCardCache[clazz].Count} cards");
        }
        initialized = true;
    }

    public static List<AbstractCard> GetCardPool(Type soldierClass)
    {
        if (!initialized)
        {
            InitCardsReflectively();
        }

        if (!soldierClass.IsSubclassOf(typeof(AbstractSoldierClass)))
        {
            throw new Exception($"Can't get card pool for non-soldier-class {soldierClass.Name}");
        }

        if (ReflectiveCardCache[soldierClass.Name] == null){
            ReflectiveCardCache[soldierClass.Name] = new List<AbstractCard>();
        }

        return ReflectiveCardCache[soldierClass.Name];
    }
}