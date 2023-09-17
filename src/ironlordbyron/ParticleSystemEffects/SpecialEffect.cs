using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.CodeAssets.ParticleSystemEffects
{
    public interface SpecialEffect
    {
        void BeginSpecialEffect();

        bool IsFinished();

        Action Afterward_SetByActionManager { get; set; }

    }
    public class CompositeSpecialEffect : SpecialEffect
    {
        List<SpecialEffect> Effects = new List<SpecialEffect>();

        public Action Afterward_SetByActionManager { get; set; }

        public static SpecialEffect DefaultAttackEffect_WithMuzzleFlash(AbstractBattleUnit target, AbstractBattleUnit source)
        {
            return new CompositeSpecialEffect()
            {
                Effects = new List<SpecialEffect>
                {
                    SimpleSpecialEffect.DefaultAttackEffect(target),
                    SimpleSpecialEffect.DefaultMuzzleFlashEffect(source)
                }
            };
        }

        public void BeginSpecialEffect()
        {
            foreach(var effect in Effects)
            {
                effect.BeginSpecialEffect();
            }
        }

        public bool IsFinished()
        {
            return Effects.All(item => item.IsFinished());
        }
    }



    public class SimpleSpecialEffect : SpecialEffect
    {
        public static SpecialEffect DefaultAttackEffect(AbstractBattleUnit target)
        {
            return new SimpleSpecialEffect()
            {
                ProtoSystem = ProtoParticleSystem.Richochet,
                SpawnAt = target
            };
        }

        public static SpecialEffect DefaultMuzzleFlashEffect(AbstractBattleUnit source)
        {
            return new SimpleSpecialEffect()
            {
                ProtoSystem = ProtoParticleSystem.MuzzleFlash,
                SpawnAt = source,
                LocationToHit = HardpointLocation.LEFT
            };
        }

        public ProtoParticleSystem ProtoSystem { get; set; }
        public AbstractBattleUnit SpawnAt { get; set; }
        public HardpointLocation LocationToHit { get; set; } = HardpointLocation.CENTER;
        public Action Afterward_SetByActionManager { get; set; }

        ParticleSystemContainer Container;
        public void BeginSpecialEffect()
        {
            var container = ParticleSystemSpawner.Instance.GenerateSpecialEffectAtCharacter(SpawnAt, ProtoSystem, LocationToHit, Afterward_SetByActionManager);
            Container = container;
        }

        public bool IsFinished()
        {
            return Container.ShouldKill();
        }
    }
}