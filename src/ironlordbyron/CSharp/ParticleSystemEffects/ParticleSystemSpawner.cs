using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class ParticleSystemSpawner : Node
{
    public static ParticleSystemSpawner Instance;
    public override void _Ready()
    {
        Instance = this;
    }

    public List<ParticleSystemContainer> SpecialEffectsInProgress { get; } = new List<ParticleSystemContainer>();

    public Camera uiCamera;
    public Camera particleCamera;

    private void MoveParticleSystemToUiBoundingBox(Particles2D systemToNormalize, Transform2D intendedBoundingBox)
    {
        throw new NotImplementedException();

    }

    const string DefaultParticleSystemPath = "res://SpecialEffects/Ricochet_normal.tscn";

    public static string GetSpecialEffectPath(string path)
    {
        return "res://SpecialEffects/" + path + ".tscn";
    }

    public ParticleSystemContainer GenerateSpecialEffectAtCharacter(AbstractBattleUnit unit, ProtoParticleSystem particleSystem, HardpointLocation locationToHit, Action afterAnimationIsFinishedAction = null)
    {
        // Implement your transformation from HardpointLocation to Transform2D here
        Transform2D intendedBoundingBox = new Transform2D(); // placeholder
        return PlaceParticleSystem(particleSystem, intendedBoundingBox, afterAnimationIsFinishedAction);
    }

    public ParticleSystemContainer PlaceParticleSystem(ProtoParticleSystem particleSystem, Transform2D intendedBoundingBox, Action afterAnimationIsFinishedAction = null, Node parent = null)
    {
        throw new NotImplementedException();

    }

    public override void _Process(float delta)
    {
        var expired = SpecialEffectsInProgress.Where(item => item.ShouldKill()).ToList();
        foreach (var item in expired)
        {
            item.KillIfApplicable();
        }
        SpecialEffectsInProgress.RemoveAll(item => expired.Contains(item));
    }
}

public enum HardpointLocation
{
    CENTER,
    LEFT,
    BOTTOM
}

public class ProtoParticleSystem
{
    public string PrefabPath { get; set; }
    public float SizeRatio { get; set; } = 0.1f;
    public float KillAfterNumSeconds { get; set; } = 5;
    public HardpointLocation Location { get; set; } = HardpointLocation.CENTER;

    public static ProtoParticleSystem MuzzleFlash = new ProtoParticleSystem
    {
        PrefabPath = ParticleSystemSpawner.GetSpecialEffectPath("ef_10_red"),
        SizeRatio = .1f
    };

    public static ProtoParticleSystem GreenSlash = new ProtoParticleSystem
    {
        PrefabPath = ParticleSystemSpawner.GetSpecialEffectPath("green_circular_slash"),
        SizeRatio = .1f
    };
    public static ProtoParticleSystem Richochet = new ProtoParticleSystem
    {
        PrefabPath = ParticleSystemSpawner.GetSpecialEffectPath("Ricochet_normal"),
        SizeRatio = .1f
    };


}

public class ParticleSystemContainer
{
    public bool OnlyRunTillFinished { get; set; } = true;
    public DateTime GoodUntil { get; set; }
    public Particles2D Particles { get; set; }

    public Action AfterAnimationIsFinishedAction { get; set; } = () => { };

    public bool ShouldKill()
    {
        throw new NotImplementedException();

        if (Particles == null)
        {
            return true;
        }

        if (GoodUntil < DateTime.Now)
        {
            return true;
        }

        if (OnlyRunTillFinished)
        {
        }

        return false;
    }

    public void KillIfApplicable()
    {
        if (Particles == null)
        {
            return;
        }

        if (ShouldKill())
        {
            //GameObject.Destroy(Particles.gameObject);
            AfterAnimationIsFinishedAction();
        }
    }
}
// ...
// (Continue with the rest of your classes, adjusting for Godot's APIs)