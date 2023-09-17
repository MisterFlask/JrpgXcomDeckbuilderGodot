using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;
using GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic.BattleRules;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.DiabolistCards.Uncommon
{
    public class FinalCurse : AbstractCard
    {
        // cost 1
        // Apply 2 Weak and 2 Vulnerable to an enemy.
        // if this character dies while this is in your deck, deal 40 damage to ALL enemies.

        public FinalCurse()
        {

            SoldierClassCardPools.Add(typeof(DiabolistSoldierClass));
            SetCommonCardAttributes("Final Curse", Rarity.RARE, TargetType.ENEMY, CardType.SkillCard, 0);
            ProtoSprite = ProtoGameSprite.DiabolistIcon("skull-staff");
        }

        public override string DescriptionInner()
        {
            return $"Apply 2 Weak and 2 Vulnerable to an enemy.  If {Owner?.GetDisplayName(DisplayNameType.SHORT_NAME)} dies while this is in your deck, deal 40 damage to ALL enemies.  Sacrifice.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            action().ApplyStatusEffect(target, new VulnerableStatusEffect(), 2);
            action().ApplyStatusEffect(target, new WeakenedStatusEffect(), 2);
            SacrificeBattleRules.RunSacrificeRules(this);
        }

        public override void OnProcWhileThisIsInDeck(AbstractProc proc)
        {
            if (proc is CharacterDeathProc)
            {
                var death = (CharacterDeathProc)proc;
                if (death.CharacterDead == Owner)
                {
                    foreach (var enemy in state().EnemyUnitsInBattle)
                    {
                        action().DamageUnitNonAttack(enemy, Owner, 40);
                    }
                }
            }
        }

    }
}