using System.Collections;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class PhoningHomeStatusEffect : AbstractStatusEffect
    {
        public PhoningHomeStatusEffect()
        {
            Name = "Phoning Home";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("phone");
        }

        public override string Description => $"At the end of turn, if this hasn't been attacked in the turn, increment the Doom Counter by 1.";

        public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard card, int totalDamageTaken)
        {
            SecondaryStacks++;
        }

        public override void OnTurnStart()
        {
            SecondaryStacks = 0;
        }

        public override void OnTurnEnd()
        {
            if (SecondaryStacks == 0)
            {
                ActionManager.Instance.IncrementDoomCounter(1);
            }
        }
    }
}