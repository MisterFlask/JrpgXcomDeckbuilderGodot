using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Uncommon
{
    public class Insensitive : AbstractCard
    {
        // gain Barricade 4.  Add a Hurtful Words and Gestures to your hand.  Cost 2.
        // Refund one energy.

        public Insensitive()
        {
            SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("Insensitive", Rarity.UNCOMMON, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 2);
            ProtoSprite = ProtoGameSprite.HammerIcon("giant");

        }

        public override string DescriptionInner()
        {
            return "gain Barricade 4.  Add a Hurtful Words and Gestures to your hand.  Refund 1.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new BarricadeStatusEffect(), 4);
            action().CreateCardToHand(new HurtfulWordsAndGestures());
            CardAbilityProcs.Refund(this);
        }
    }
}