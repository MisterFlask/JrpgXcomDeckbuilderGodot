﻿namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Uncommon
{
    public class HastyDefenses : AbstractCard
    {

        public HastyDefenses()
        {
            SetCommonCardAttributes("Hasty Defenses", Rarity.UNCOMMON, TargetType.ALLY, CardType.SkillCard, 1);
            ProtoSprite =
                ProtoGameSprite.ArchonIcon("stick-frame");
            BaseDefenseValue = 6;
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()}.  Ambush: Apply it to ALL characters instead.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            var ambush = this.Ambush(() =>
            {
                foreach (var ally in state().AllyUnitsInBattle)
                {
                    Action_ApplyDefenseToTarget(ally);
                }

            });
            if (!ambush)
            {
                Action_ApplyDefenseToTarget(target);
            }
        }


        // Apply 6 defense.  Ambush: Apply 6 defense to ALL characters instead.
    }
}