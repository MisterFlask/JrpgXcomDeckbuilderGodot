using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections;

namespace Assets.CodeAssets.Cards.ArchonCards
{
    public class GiveGround : AbstractCard
    {
        public GiveGround()
        {
            ProtoSprite = ProtoGameSprite.ArchonIcon("cavalry");
            this.SetCommonCardAttributes(
                "Get stuck in!",
                Rarity.BASIC,
                TargetType.ALLY,
                CardType.SkillCard,
                1
                );
        }

        public override string DescriptionInner()
        {
            return "Apply Advanced to an ally.  That ally gains +1 Temporary Strength.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new AdvancedStatusEffect());
            action().ApplyStatusEffect(target, new TemporaryStrengthStatusEffect());
        }
    }
}