using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Rare
{
    public class Impenetrable : AbstractCard
    {
        // Gain 15 block.  Taunt an enemy.  It gains Weak this turn.

        // Brute: Taunt ALL enemies.


        public Impenetrable()
        {
            SoldierClassCardPools.Add(typeof(HammerSoldierClass));

            SetCommonCardAttributes("Impenetrable", Rarity.RARE, TargetType.ENEMY, CardType.SkillCard, 2);
            BaseDefenseValue = 15;
            Stickers.Add(new BasicDefendSelfSticker());
            ProtoSprite = ProtoGameSprite.HammerIcon("stone-block");

        }

        public override string DescriptionInner()
        {
            return "Taunt target enemy.  It gains Weak this turn.  Brute: Taunt ALL enemies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(Owner);
            action().TauntEnemy(target, Owner);
            this.Brute(() =>
            {
                foreach (var enemy in state().EnemyUnitsInBattle)
                {
                    action().TauntEnemy(enemy, Owner);
                }
            });
        }

    }
}