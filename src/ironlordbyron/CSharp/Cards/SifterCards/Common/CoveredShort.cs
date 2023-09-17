namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.SifterCards.Common
{
    public class CoveredShort : AbstractCard
    {
        // Apply 6 block.  Sly: then do it again.  Stash 2.

        public CoveredShort()
        {
            SetCommonCardAttributes("Covered Short", Rarity.COMMON, TargetType.ALLY, CardType.SkillCard, 1);
            BaseDefenseValue = 8;

            ProtoSprite =
                ProtoGameSprite.ArchonIcon("shorts");
            AddSticker(new GildedCardSticker(2));
        }

        public override string DescriptionInner()
        {
            return $"Apply {DisplayedDefense()} block.  Sly: Then do it again.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyDefenseToTarget(target);
            this.Sly(() =>
            {
                Action_ApplyDefenseToTarget(target);
            });
        }
    }
}