using Assets.CodeAssets.BattleEntities.Augmentations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.CodeAssets.BattleEntities
{
    public static class PerkAndAugmentationRegistrar
    {

        public static List<AbstractSoldierPerk> TotalPerkAndAugmentationList
            = new List<AbstractSoldierPerk>()
            .Concat(BasicAugmentations.BasicAugmentationsList)
            .ToList();

        public static AbstractSoldierPerk GetRandomPerkForNewSoldier()
        {
            throw new NotImplementedException();
        }

        public static AbstractSoldierPerk GetRandomAugmentation(Rarity rarity)
        {
            if (rarity == Rarity.ANY)
            {
                return TotalPerkAndAugmentationList.PickRandom();
            }
            else
            {
                return TotalPerkAndAugmentationList.PickRandomWhere(item => item.Rarity == rarity);
            }
        }
    }
}