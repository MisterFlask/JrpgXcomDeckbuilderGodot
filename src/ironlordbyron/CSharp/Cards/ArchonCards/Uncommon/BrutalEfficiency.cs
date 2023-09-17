using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Units.PlayerUnitClasses;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Uncommon
{
    public class BrutalEfficiency : AbstractCard
    {
        public BrutalEfficiency()
        {
            SoldierClassCardPools.Add(typeof(ArchonSoldierClass));
            SetCommonCardAttributes(
                "Brutal Efficiency",
                Rarity.UNCOMMON,
                TargetType.ALLY,
                CardType.SkillCard,
                1,

                protoGameSprite: ProtoGameSprite.ArchonIcon("factory")
                );
        }

        public override string DescriptionInner()
        {
            return $"The next time you play ANY card, play it again.  Apply 8 stress to all other allies.";
        }

        public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
        {
            Action_ApplyStatusEffectToOwner(new DuplicateNextCardPlayed(), 1);
        }
    }


    public class DuplicateNextCardPlayed : AbstractStatusEffect
    {
        public DuplicateNextCardPlayed()
        {
            Name = "Duplicate Next Card Played";
        }

        public override string Description => "Duplicates the next card played by ANY character.";

        public override void OnAnyCardPlayed(AbstractCard cardPlayed, AbstractBattleUnit targetOfCard, bool isMine)
        {
            var target = targetOfCard;
            if (target.IsDead)
            {
                target = CardTargeting.RandomTargetableEnemy();
            }
            cardPlayed.EvokeCardEffect(target, new EnergyPaidInformation());
            Stacks--;
        }
    }
}