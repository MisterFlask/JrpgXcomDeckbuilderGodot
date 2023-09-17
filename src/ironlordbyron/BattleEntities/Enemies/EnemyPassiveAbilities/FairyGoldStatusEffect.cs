using System.Collections;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    /// <summary>
    /// if you kill this within 3 turns, gain [stacks] credits.
    /// </summary>
    public class FairyGoldStatusEffect : AbstractStatusEffect
    {
        public FairyGoldStatusEffect()
        {
            Name = "Fairy Gold";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("gold-bar");
        }

        public override string Description => "If you kill this within 3 turns of combat starting, gain [stacks] credits";

        public override void OnTurnStart()
        {
            SecondaryStacks++;
            if (SecondaryStacks > 3)
            {
                Stacks = 0;
            }
        }

        public override void OnDeath(AbstractBattleUnit unitThatKilledMe, AbstractCard cardUsedIfAny)
        {
            GameState.Instance.Credits += Stacks;
        }
    }
}