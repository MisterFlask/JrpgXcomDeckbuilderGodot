namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards
{
    public class SampleCardTemplate : AbstractCard
    {

        public SampleCardTemplate()
        {
            //SoldierClassCardPools.Add(typeof(RookieClass));
            Name = "Bayonet";
            BaseDamage = 10;
            TargetType = TargetType.ENEMY;
            CardType = CardType.AttackCard;
            ProtoSprite = ProtoGameSprite.FromGameIcon(path: "Sprites/bayonet");
        }

        public override string DescriptionInner()
        {
            throw new System.NotImplementedException();
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            throw new System.NotImplementedException();
        }
    }
}