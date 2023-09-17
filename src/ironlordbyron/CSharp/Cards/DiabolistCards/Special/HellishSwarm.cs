using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Special
{
    public class HellishSwarm : AbstractCard
    {
        public HellishSwarm()
        {
            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Hellish Swarm", Rarity.NOT_IN_POOL, TargetType.ENEMY, CardType.AttackCard, 1);
            Stickers.Add(new HazardousCardSticker());
            BaseDamage = 2;
            ProtoSprite = ProtoGameSprite.DiabolistIcon("spider-eye");
        }

        // Deal 4 damage and apply 1 Vulnerable.  Draw a card.  Exhaust.  Cost 1.
        // Swarm.
        public override string DescriptionInner()
        {
            return "Deal 2 damage and apply 1 Vulnerable.  Light.  Nascent.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 1);
            Action_Exhaust();
        }
    }
}