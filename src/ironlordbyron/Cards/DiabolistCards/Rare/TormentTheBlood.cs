using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.Cards.DiabolistCards.Rare
{
    public class TormentTheBlood : AbstractCard
    {
        // Startup: Gain 1 Dexterity and 3 stress.
        // Apply 10 defense to target.  Cost 1.

        public TormentTheBlood()
        {
            SetCommonCardAttributes("Torment the Blood", Rarity.RARE, TargetType.ALLY, CardType.SkillCard, 1, typeof(DiabolistSoldierClass));
            Stickers.Add(new ExhaustCardSticker());
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} defense to target. Startup: Gain 2 Dexterity and 3 stress.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(target);
        }

        public override void OnStartup()
        {
            action().ApplyStress(this.Owner, 3);
            Action_ApplyStatusEffectToOwner(new DexterityStatusEffect(), 2);
        }
    }
}