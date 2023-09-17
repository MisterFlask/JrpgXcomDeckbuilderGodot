using System.Collections;

namespace Assets.CodeAssets.Cards
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
            this.ProtoSprite = GameIconProtoSprite.FromGameIcon(path: "Sprites/bayonet");
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