﻿namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Uncommon
{
    public class KnownCheat : AbstractCard
    {
        public KnownCheat()
        {
            SetCommonCardAttributes("Known Cheat", Rarity.UNCOMMON, TargetType.ENEMY, CardType.SkillCard, 0);

            ProtoSprite =
                ProtoGameSprite.ArchonIcon("card-random");
        }

        // Apply 2 Weak to an enemy.  Ambush: Gain 5 gold.  Cost 0.
        public override string DescriptionInner()
        {
            return "Apply 2 Weakened to target.  Ambush: Gain 5 credits.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToTarget(new WeakenedStatusEffect(), 2, target);
            this.Ambush(() =>
            {
                CardAbilityProcs.ChangeMoney(5);
            });
        }
    }
}