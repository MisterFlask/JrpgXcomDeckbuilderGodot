using GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.Stickers;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.CogCards.Special
{
    public class ShieldDrone : AbstractCard
    {
        // Apply 3 block (scales with dex).  Apply 3 block next turn.  Cost 0.

        public ShieldDrone()
        {
            SetCommonCardAttributes("Shield Drone", Rarity.NOT_IN_POOL, TargetType.NO_TARGET_OR_SELF, CardType.SkillCard, 1);
            BaseDefenseValue = 6;
            AddSticker(new LightCardSticker());
            ProtoSprite = ProtoGameSprite.CogIcon("shield-drone");

        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block, twice.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(target);
            Action_ApplyDefenseToTarget(target);
        }
    }
}