using System.Collections;

namespace Assets.CodeAssets.Cards.CogCards.Common
{
    public class Vivisect : AbstractCard
    {
        // Deal 6 damage.  Add two Autocannon Sentries to your hand.
        // Lethal: Gain 3 Data Points.
        // Exhaust.  Cost 1.

        public Vivisect()
        {
            Stickers.Add(new BasicAttackTargetSticker());
            Stickers.Add(new ExhaustCardSticker());
            BaseDamage = 6;
            DamageModifiers.Add(new GainDataPointsOnSlayDamageModifier { DataPointsToAcquire = 3 });
            ProtoSprite = ProtoGameSprite.CogIcon("split-body");
        }

        public override string DescriptionInner()
        {
            return $"Add two autocannon sentries to your hand.  Lethal: Gain 3 data points." ;
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            throw new System.NotImplementedException();
        }

    }
}