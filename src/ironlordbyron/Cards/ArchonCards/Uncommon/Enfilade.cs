using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using Assets.CodeAssets.Cards.ArchonCards.Effects;
using Assets.CodeAssets.Cards.ArchonCards.Special;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.ArchonCards.Uncommon
{
    public class Enfilade : AbstractCard
    {
        public Enfilade()
        {
            this.SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            this.SetCommonCardAttributes(
                "Enfilade",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.SkillCard,
                1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("shield-bash")
                );
            this.BaseDamage = 5;
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