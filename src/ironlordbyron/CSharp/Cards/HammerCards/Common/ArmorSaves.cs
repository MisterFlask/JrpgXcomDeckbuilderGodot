using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects;
using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.HammerCards.Common
{
    public class ArmorSaves : AbstractCard
    {
        // Apply 5 Barricade. [damage resist, halves each turn]  Exhaust.  Draw a card.

        public ArmorSaves()
        {
            SoldierClassCardPools.Add(typeof(HammerSoldierClass));
            SetCommonCardAttributes("Armor Save", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
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
            Action_Exhaust();
        }
    }
}