﻿using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Common;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Rare
{
    public class Abrasive : AbstractCard
    {
        // Apply 2 Stress to all OTHER allies.  Lose 10 stress.  Draw two cards.  
        // Add a Hurtful Words and Gestures to your hand.
        // Exhaust.  Cost 1.

        public Abrasive()
        {
            SoldierClassCardPools.Add(typeof(HammerSoldierClass));
            ProtoSprite = ProtoGameSprite.HammerIcon("stone-pile");

            SetCommonCardAttributes("Abrasive", Rarity.RARE, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
        }

        public override string DescriptionInner()
        {
            return $"Apply 2 Stress to all OTHER allies.  Lose 10 stress. Draw 2 cards.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStress(Owner, -10);
            foreach (var ally in state().AllyUnitsInBattle)
            {
                if (ally == Owner) continue;
                action().ApplyStress(ally, 2);
            }

            action().DrawCards(2);
            Action_Exhaust();
            action().CreateCardToHand(new HurtfulWordsAndGestures());
        }
    }
}