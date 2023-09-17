using Assets.CodeAssets.BattleEntities.StatusEffects;
using Assets.CodeAssets.BattleEntities.Units.PlayerUnitClasses;
using System.Collections;

namespace Assets.CodeAssets.Cards.HammerCards
{
    public class ArmorSaves : AbstractCard
    {
        // Apply 5 Barricade. [damage resist, halves each turn]  Exhaust.  Draw a card.

        public ArmorSaves()
        {
            this.SoldierClassCardPools.Add(typeof(HammerSoldierClass));
            this.SetCommonCardAttributes("Armor Save", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1); 
            ProtoSprite = ProtoGameSprite.HammerIcon("shield-reflect");

        }

        public override string DescriptionInner()
        {
            return $"Apply 5 Barricade.  Draw a card.  Exhaust.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToTarget(new BarricadeStatusEffect(), 5, target);
            action().DrawCards(1);
            this.Action_Exhaust();
        }
    }
}