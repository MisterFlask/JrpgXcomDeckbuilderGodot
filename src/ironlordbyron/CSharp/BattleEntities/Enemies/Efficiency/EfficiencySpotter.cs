using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Efficiency
{
    // rotate between attack-with-debuff (adds Targeting Reticle to deck) and shielding self
    public class EfficiencySpotter : AbstractEnemyUnit
    {
        public EfficiencySpotter()
        {
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Slime Holyiii");
            Description = "???";
            EnemyFaction = EnemyFaction.EFFICIENCY;
            CharacterNicknameOrEnemyName = "Spotter W-203-series";
            MaxHp = 11;
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