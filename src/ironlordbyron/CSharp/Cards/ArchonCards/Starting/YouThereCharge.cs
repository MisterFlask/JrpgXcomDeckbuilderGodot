using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.StatusEffects;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Starting
{
	public class GiveGround : AbstractCard
	{
		public GiveGround()
		{
			ProtoSprite = ProtoGameSprite.ArchonIcon("cavalry");
			SetCommonCardAttributes(
				"Get stuck in!",
				Rarity.BASIC,
				TargetType.ALLY,
				CardType.SkillCard,
				1
				);
		}

		public override string DescriptionInner()
		{
			return "Apply Advanced to an ally.  That ally gains +1 Temporary Strength.";
		}

		public override void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid)
		{
			action().ApplyStatusEffect(target, new AdvancedStatusEffect());
			action().ApplyStatusEffect(target, new TemporaryStrengthStatusEffect());
		}
	}
}
