using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.BlackhandCards.Powers
{
    public class GorillaTactics : AbstractCard
    {
        // Cost 1, Rare power.    Whenever you exhaust a card, gain 4 Temporary Strength

        public GorillaTactics()
        {
            SetCommonCardAttributes("Gorilla Tactics", Rarity.COMMON, TargetType.NO_TARGET_OR_SELF, CardType.PowerCard, 1);
            ProtoSprite = ProtoGameSprite.BlackhandIcon("gorilla");
        }

        public override string DescriptionInner()
        {
            return "Whenever you exhaust a card, gain 4 temporary strength.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(Owner, new GorillaTacticsStatusEffect(), 4);
        }
    }
    public class GorillaTacticsStatusEffect : AbstractStatusEffect
    {
        public GorillaTacticsStatusEffect()
        {
            Name = "Gorilla Tactics";
        }

        public override string Description => $"Whenever you exhaust a card, gain {DisplayedStacks()} Temporary Power.";


        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is ExhaustedCardProc)
            {
                ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new TemporaryStrengthStatusEffect(), Stacks);
            }
        }
        // todo: exhaust trigger
    }
}