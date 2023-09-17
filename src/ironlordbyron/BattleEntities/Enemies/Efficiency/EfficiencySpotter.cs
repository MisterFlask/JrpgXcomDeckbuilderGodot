using System.Collections;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    // rotate between attack-with-debuff (adds Targeting Reticle to deck) and shielding self
    public class EfficiencySpotter : AbstractEnemyUnit
    {
        public EfficiencySpotter()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Slime Holyiii");
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.EFFICIENCY;
            CharacterNicknameOrEnemyName = "Spotter W-203-series";
            this.MaxHp = 11;
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.RandomIntent(
                IntentsFromPercentBase.AttackRandomPcWithCardToDiscardPile(
                    new TargetingReticle(),
                    this,
                    4, 
                    1));
        }
    }
}