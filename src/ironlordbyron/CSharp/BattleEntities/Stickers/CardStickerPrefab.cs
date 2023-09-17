/* using Godot;
using System;

public class CardStickerPrefab : Node2D
{
	public Image Image;

	public AbstractCardSticker Sticker { get; set; }

	public override void _Process(float delta)
	{
		if (Sticker == null)
		{
			return;
		}
		Image.SetProtoSprite(Sticker.ProtoSprite);
	}
}
 */