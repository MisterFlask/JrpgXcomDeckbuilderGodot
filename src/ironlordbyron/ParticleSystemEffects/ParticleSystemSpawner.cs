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
        var screenLocationOfIntendedPosition = uiCamera.UnprojectPosition(intendedBoundingBox.origin.ToVector3()).ToVector2();
        var worldLocationInParticleCameraOfIntendedPosition = particleCamera.ProjectPosition(screenLocationOfIntendedPosition.ToVector3()).ToVector2();
        systemToNormalize.GlobalPosition = worldLocationInParticleCameraOfIntendedPosition;

        GD.Print($"INTENDED: {intendedBoundingBox.origin.x}, {intendedBoundingBox.origin.y} ACTUAL: {systemToNormalize.GlobalPosition.x}, {systemToNormalize.GlobalPosition.y}");
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
        if (parent == null)
        {
            parent = particleCamera;
        }

        PackedScene loadedPrefab = (PackedScene)GD.Load(particleSystem.PrefabPath);
        if (loadedPrefab == null)
        {
            GD.PrintErr("Could not load particles from location: " + particleSystem.PrefabPath + "; using meeple instead");
            loadedPrefab = (PackedScene)GD.Load(DefaultParticleSystemPath);
        }

        Particles2D instance = (Particles2D)loadedPrefab.Instance();
        parent.AddChild(instance);

        MoveParticleSystemToUiBoundingBox(instance, intendedBoundingBox);
        instance.Scale = new Vector2(particleSystem.SizeRatio, particleSystem.SizeRatio);
        instance.Emitting = true;

        // Implement your layer setting logic here
        // ...

        var container = new ParticleSystemContainer
        {
            Particles = instance,
            GoodUntil = DateTime.Now + TimeSpan.FromSeconds(particleSystem.KillAfterNumSeconds)
        };
        
        // ...
        // (Continue as in your original script, adjusting for Godot's APIs)
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
            if (!Particles.isPlaying)
            {
                return true;
            }
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
            GameObject.Destroy(Particles.gameObject);
            AfterAnimationIsFinishedAction();
        }
    }
}
    // ...
    // (Continue with the rest of your classes, adjusting for Godot's APIs)