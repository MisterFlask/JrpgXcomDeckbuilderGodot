using Godot;
using System;
using System.Collections.Generic;
using System.IO;
public class ImageUtils
{
    public const string MeepleImagePath = "res://Sprites/3d-meeple.png";
    public const string CrossedSwordsImagePath = "res://Sprites/crossed-swords.png";

    public static ProtoGameSprite ProtoGameSpriteFromGameIcon(
        string path = MeepleImagePath,
        Color? color = null)
    {
        return new GameIconProtoSprite { Color = color ?? new Color(1, 1, 1, 1), SpritePath = path };
    }
    public static Texture LoadSprite(string imageName)
    {
        var loaded = GD.Load<Texture>(imageName);
        if (loaded == null)
        {
            GD.PrintErr("Could not load sprite from image path: " + imageName);
            loaded = GD.Load<Texture>(MeepleImagePath);
        }
        return loaded;
    }
}



public abstract class ProtoGameSprite
{
    public abstract GameSprite ToGameSpriteImage();

    public Texture ToTexture()
    {
        return ToGameSpriteImage().Sprite;
    }

    // convenience methods follow
    public static ProtoGameSprite Default => ImageUtils.ProtoGameSpriteFromGameIcon();

    public string SpritePath { get; set; }

    public static ProtoGameSprite CogIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Cog/" + name);
    }

    public static ProtoGameSprite FromGameIcon(
        string path = ImageUtils.MeepleImagePath,
        Color? color = null)
    {
        if (color == null) color = Color.white;
        return ImageUtils.ProtoGameSpriteFromGameIcon(path, color);
    }

    /// <summary>
    /// Sets a sprite renderer to a particular sprite, then resizes it so that it maintains the dimensions of sizeX*sizeY.
    /// </summary>
    /// <summary>
    /// Sets a sprite renderer to a particular sprite, then resizes it so that it maintains the dimensions of sizeX*sizeY.
    /// </summary>
    /// <summary>
    /// Sets a sprite renderer to a particular sprite, then resizes it so that it maintains the dimensions of sizeX*sizeY.
    /// </summary>
    public static void SetSpriteRendererToSpriteWhileMaintainingSize(float sizeX, float sizeY, Texture texture, Sprite spriteNode)
    {
        float currentScaleX = spriteNode.Scale.x;
        float currentScaleY = spriteNode.Scale.y;

        spriteNode.Texture = texture;

        Vector2 textureSize = texture.GetSize();

        Vector3 parentScale = spriteNode.GetParent() != null ? spriteNode.GetParent().GlobalScale : new Vector3(1, 1, 1);

        float scaleFactorX = (sizeX / (textureSize.x / currentScaleX)) / parentScale.x;
        float scaleFactorY = (sizeY / (textureSize.y / currentScaleY)) / parentScale.y;

        spriteNode.Scale = new Vector2(scaleFactorX, scaleFactorY);
    }


    public static ProtoGameSprite BlackhandIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Blackhand/" + name);
    }

    public static ProtoGameSprite DiabolistIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Diabolist/" + name);
    }

    public static ProtoGameSprite ArchonIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Archon/" + name);
    }
    public static ProtoGameSprite SifterIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Sifter/" + name);
    }
    public static ProtoGameSprite HammerIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Hammer/" + name);
    }
    public static ProtoGameSprite RookieIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Rookie/" + name);
    }

    internal static ProtoGameSprite TerrainIcon(string v)
    {
        return FromGameIcon("Sprites/MissionTerrain/" + v);
    }

    public static ProtoGameSprite OtherIcons(string name)
    {
        return FromGameIcon("Sprites/OtherIcons/" + name);
    }
    public static ProtoGameSprite AttributeOrAugmentIcon(string name)
    {
        return FromGameIcon("Sprites/AttributesAndAugments/" + name);
    }
    public static ProtoGameSprite MadnessIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/Madness/" + name);
    }
    public static ProtoGameSprite VisualTagIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/CardVisualTags/" + name);
    }

    internal static ProtoGameSprite StatusCardIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/StatusCards/" + name);

    }

    internal static ProtoGameSprite EmblemIcon(string name)
    {
        return FromGameIcon("Sprites/Cards/ClassEmblems/" + name);
    }
    public static ProtoGameSprite MissionIcon(string name)
    {
        return FromGameIcon("Sprites/MissionIcons/" + name);
    }

    internal static ProtoGameSprite MachineBattler(string name)
    {
        return FromGameIcon("Sprites/Enemies/Machines/" + name);
    }
    internal static ProtoGameSprite MapIcon(string name)
    {
        return FromGameIcon("Sprites/MapIcons/" + name);
    }
}

public static class ImageExtensions
{

    public static Texture SpriteToTexture(this Sprite sprite)
    {
        return Texture2D.CreateExternalTexture(
            (int)sprite.rect.width,
            (int)sprite.rect.height,
            TextureFormat.RGBA32,
            false,
            false,
            sprite.texture.GetNativeTexturePtr()
        );
    }

    public static void SetProtoSprite(this Image image, ProtoGameSprite protoSprite)
    {
        image.sprite = protoSprite.ToSprite();
        image.color = protoSprite.ToGameSpriteImage().Color;

    }
    /// <summary>
    /// Attempts to keep the same sprite size
    /// </summary>
    /// <param name="sprite">The sprite node to modify</param>
    /// <param name="protoSprite">The new proto game sprite data</param>
    public static void SetProtoSprite(this Sprite sprite, ProtoGameSprite protoSprite)
    {
        var originalTexture = sprite.Texture;
        var originalTextureSize = new Vector2(originalTexture.GetWidth(), originalTexture.GetHeight());

        // Assuming ToSprite() returns a Texture and ToGameSpriteImage().Color returns a Godot.Color
        sprite.Texture = protoSprite.ToSprite();
        sprite.Modulate = protoSprite.ToGameSpriteImage().Color;

        var newTexture = sprite.Texture;
        var newTextureSize = new Vector2(newTexture.GetWidth(), newTexture.GetHeight());

        var scaleFactorWidth = originalTextureSize.x / newTextureSize.x;
        var scaleFactorHeight = originalTextureSize.y / newTextureSize.y;

        sprite.Scale = new Vector2(scaleFactorWidth * sprite.Scale.x, scaleFactorHeight * sprite.Scale.y);
    }
}

public class GameSprite
{
    public Sprite Sprite { get; set; }
    public Color Color { get; set; }
}

public class GameIconProtoSprite : ProtoGameSprite
{
    public bool ReverseXAxis { get; set; } = false;

    public Color Color { get; set; }

    public override GameSprite ToGameSpriteImage()
    {
        var loaded = GD.Load<Texture>(SpritePath);
        if (loaded == null)
        {
            GD.PrintErr("Could not load sprite from image path: " + SpritePath + "; using meeple instead");
            loaded = GD.Load<Texture>(ImageUtils.MeepleImagePath);
        }

        return new GameSprite
        {
            Color = Color,
            Sprite = loaded
        };
    }


    public static List<string> GetImagesPathsInFolderRecursively(String folderName)
    {

        DirectoryInfo Folder;
        FileInfo[] Images;
        var files = new List<string>();

        Folder = new DirectoryInfo(folderName);
        Images = Folder.GetFiles();

        foreach (var file in Folder.GetFiles())
        {
            // if file ends with jpg, add it to list
        }

        // foreach folder, get images paths


        var imagesList = new List<string>();

        for (int i = 0; i < Images.Length; i++)
        {
            imagesList.Add(string.Format(@"{0}/{1}", folderName, Images[i].Name));
        }

        return imagesList;
    }

}