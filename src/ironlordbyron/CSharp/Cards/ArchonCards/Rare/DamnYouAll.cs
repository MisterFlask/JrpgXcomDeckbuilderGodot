using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Rare
{
    public class DamnYouAll : AbstractCard
    {

        public DamnYouAll()
        {
            DamageModifiers.Add(new DamnYouAllDamageModifier());
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes("D*** You All", Rarity.RARE, TargetType.NO_TARGET_OR_SELF,
                CardType.AttackCard, 1,
                protoGameSprite: ProtoGameSprite.ArchonIcon("lightning-shout"));
            BaseDamage = 3;
        }

        // Deals 5 damage to each of 3 different targets at random.
        // 
        public override string DescriptionInner()
        {
            return $"Deals {DisplayedDamage()} damage to each of 3 different targets at random.  This card gains 3 damage " +
                $"for EVERY Soldier that has died this campaign [{GetNumDeadSoldiers()} soldiers have died].  Exert.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().AttackWithCard(this, CardTargeting.RandomTargetableEnemy());
            action().AttackWithCard(this, CardTargeting.RandomTargetableEnemy());
            action().AttackWithCard(this, CardTargeting.RandomTargetableEnemy());
            this.ProcExert();
        }

        private int GetNumDeadSoldiers()
        {
            return GameState.Instance.PersistentCharacterRoster.Where(item => item.IsDead).Count();
        }
    }

    public class DamnYouAllDamageModifier : DamageModifier
    {
        public DamnYouAllDamageModifier()
        {
            TargetInvariant = true;
            TooltipDescription = "+3 damage for each soldier died.";
        }

        private int GetNumDeadSoldiers()
        {
            return GameState.Instance.PersistentCharacterRoster.Where(item => item.IsDead).Count();
        }
        public override int GetIncrementalDamageAddition(int currentBaseDamage, AbstractCard damageSource, AbstractBattleUnit target)
        {
            return 3 * GetNumDeadSoldiers();
        }

    }
}