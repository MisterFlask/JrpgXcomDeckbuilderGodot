using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Rare
{
    public class BanquetOfMiseries : AbstractCard
    {
        // Cost 1
        // Deal 7 damage twice.  If an ally dies while this is in your deck, gain the Powerful perk.

        public BanquetOfMiseries()
        {
            SetCommonCardAttributes("Banquet of Miseries", Rarity.RARE, TargetType.ENEMY, CardType.AttackCard, 1, typeof(DiabolistSoldierClass));

            ProtoSprite = ProtoGameSprite.DiabolistIcon("despair");
        }

        public override string DescriptionInner()
        {
            return $"Deal {displayedDamage()} damage twice.  If an ally dies while this is in your draw, hand or discard piles," +
                $" gain the Powerful perk PERMANENTLY.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            for (int i = 0; i < 2; i++)
            {
                action().AttackUnitForDamage(target, Owner, BaseDamage, this);
            }
        }

        public override void OnProcWhileThisIsInDeck(AbstractProc proc)
        {
            // todo
            // this.Owner.ApplyAugmentation(new PowerfulPerk());



        }
    }
}