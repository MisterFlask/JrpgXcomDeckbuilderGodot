﻿using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.EnemyPassiveAbilities;
using System.Collections.Generic;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Summer
{
    public class DisdainfulNymph : AbstractBattleUnit
    {
        public DisdainfulNymph()
        {
            CharacterNicknameOrEnemyName = "Disdainful Nymph";
            EnemyFaction = EnemyFaction.SUMMER;
            SquadRole = EnemySquadRole.SMALL;
            ProtoSprite = ImageUtils.ProtoGameSpriteFromGameIcon(path: "Sprites/Enemies/v2/Boss Dryad Yggdrasil");

            MaxHp = 20;
        }

        public override List<AbstractIntent> GetNextIntents()
        {
            return IntentRotation.FixedRotation(
                IntentsFromBaseDamage.AttackRandomPc(this, 10, 1),
                IntentsFromBaseDamage.DefendSelf(this, 10),
                IntentsFromPercentBase.AttackRandomPcWithDebuff(new WeakenedStatusEffect { Stacks = 3 }, this, 5, 1));
        }

        public override void AssignStatusEffectsOnCombatStart()
        {
            StatusEffects.Add(new EruditionStatusEffect
            {
                Stacks = 10
            });
        }
    }
}