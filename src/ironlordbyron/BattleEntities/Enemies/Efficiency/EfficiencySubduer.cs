using System.Collections;
using System.Collections.Generic;

namespace Assets.CodeAssets.BattleEntities.Enemies.Efficiency
{
    public class EfficiencySubduer : AbstractEnemyUnit
    {
        public EfficiencySubduer()
        {
            this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Slime Iceii");
            this.Description = "???";
            this.EnemyFaction = EnemyFaction.EFFICIENCY;
            CharacterNicknameOrEnemyName = "Subduer XSML";
            this.MaxHp = 11;
            //Big attack, but has to charge up first
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromPercentBase.Charging(this),
                IntentsFromPercentBase.Charging(this),
                IntentsFromPercentBase.AttackRandomPcWithCardToDiscardPile(
                    new TargetingReticle(),
                    this,
                    200,
                    1),
                IntentsFromPercentBase.DefendSelf(this, 50));
        }
    }
}