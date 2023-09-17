using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Uncommon
{
    public class PawnBroker : AbstractCard
    {
        public PawnBroker()
        {
            SetCommonCardAttributes("Pawnbroker", Rarity.UNCOMMON, TargetType.ALLY, CardType.SkillCard, 1);

            ProtoSprite =
                ProtoGameSprite.ArchonIcon("pawn");
        }

        // Gain 5 block.  Whenever you trigger Sacrifice, ALL characters gain 1 Charged.  Cost 1.
        public override string DescriptionInner()
        {
            return "Apply 5 block.  Whenever sacrifice is triggered, ALL allies gain 1 Charged.  Exhaust.";

        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(Owner, BaseDefenseValue);
            Action_ApplyStatusEffectToOwner(new PawnBrokerStatusEffect(), 1);
            Action_Exhaust();
        }
    }

    public class PawnBrokerStatusEffect : AbstractStatusEffect
    {
        public PawnBrokerStatusEffect()
        {
            Name = "Pawn Broker";
        }

        public override string Description => $"Whenever you trigger Sacrifice, ALL allies gain {DisplayedStacks()} Charged.";


        public override void ProcessProc(AbstractProc proc)
        {
            if (proc is SacrificeProc)
            {
                foreach (var ally in state().AllyUnitsInBattle)
                {
                    action().ApplyStatusEffect(ally, new ChargedStatusEffect(), Stacks);
                }
            }
        }
    }

}