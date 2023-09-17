using System.Collections;

namespace Assets.CodeAssets.BattleEntities.Enemies.EnemyPassiveAbilities
{
    public class Momentum : AbstractStatusEffect
    {
        public Momentum()
        {
            Name = "Momentum";
            this.ProtoSprite = ProtoGameSprite.AttributeOrAugmentIcon("bottom-right-3d-arrow");

        }

        public override string Description => "Each turn, gain [Stacks] strength.";

        public override void OnTurnStart()
        {
            ActionManager.Instance.ApplyStatusEffect(OwnerUnit, new StrengthStatusEffect(), Stacks);
        }
    }
}