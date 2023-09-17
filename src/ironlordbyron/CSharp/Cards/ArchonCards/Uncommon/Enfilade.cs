﻿using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Special;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Uncommon
{
    public class Enfilade : AbstractCard
    {
        public Enfilade()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes(
                "Enfilade",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.SkillCard,
                1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("shield-bash")
                );
            BaseDamage = 5;
        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage.  Add a Manuever to your hand.  Gain 1 Dexterity.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            action().CreateCardToHand(new Manuever());
            Action_ApplyStatusEffectToOwner(new DexterityStatusEffect(), 1);

        }
    }
}