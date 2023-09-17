namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Uncommon
{
    public class FusionGrenade : AbstractCard
    {
        //  Deal 3 damage and draw a card.  Discharge:  Deal 20 damage to a random target, and gain 1 energy.  Cost 0.

        public FusionGrenade()
        {
            SetCommonCardAttributes("Fusion Grenade", Rarity.UNCOMMON, TargetType.ENEMY, CardType.AttackCard, 0);
            Stickers.Add(new ExhaustCardSticker());
            BaseDamage = 3;
            ProtoSprite = ProtoGameSprite.CogIcon("stick-grenade");

        }

        public override string DescriptionInner()
        {
            return $"Deal {DisplayedDamage()} damage and draw a card.  Discharge: Deal another {DisplayedDamage(20)}.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_AttackTarget(target);
            CardAbilityProcs.Discharge(this, () =>
            {
                action().AttackUnitForDamage(target, Owner, 20, this);
            });
        }
    }
}