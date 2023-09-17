using Assets.CodeAssets.BattleEntities.StatusEffects;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace Assets.CodeAssets.BattleEntities.Enemies.Summer
{
	public class Delicious : AbstractStatusEffect
	{
		public Delicious()
		{
			this.Name = "Delicious";
			this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon("Sprites\\Enemies\\v2\\Breakfast Nightmares Bacon Beast");
		}

		public override string Description => $"Whenever this takes attack damage, all allied characters gain {Stacks} temporary strength.";

		public override void OnStruck(AbstractBattleUnit unitStriking, AbstractCard cardUsedIfAny, int totalDamageTaken)
		{
			if (totalDamageTaken <= 0)
			{
				return;
			}

			foreach (var ally in GameState.Instance.EnemyUnitsInBattle)
			{
				var tempStrengthEffect = ally.GetStatusEffect<TemporaryStrengthStatusEffect>();
				if (tempStrengthEffect != null)
				{
					tempStrengthEffect.Stacks += Stacks;
				}
				else
				{
					ally.StatusEffects.Add(new TemporaryStrengthStatusEffect { Stacks = Stacks });
				}
			}
		}
	}

	public class Baconbeast : AbstractBattleUnit
	{
		public Baconbeast()
		{
			CharacterNicknameOrEnemyName = "Baconbeast";
			this.ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/PathToBaconbeastSprite");
			MaxHp = 70; // Please specify the HP value here

			// Adding the Delicious status effect
			this.StatusEffects.Add(new Delicious());
		}

		public override List<AbstractIntent> GetNextIntents()
		{
			return IntentRotation.FixedRotation(
				IntentsFromBaseDamage.DebuffRandomOtherOnAttack(this, new VulnerableStatusEffect(), 2, 20),
				IntentsFromBaseDamage.AttackAllPcs(this, 15, 1)
			) ;
		}
	}
}
