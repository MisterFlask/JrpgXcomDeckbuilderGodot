/* using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class CardStickerHolder : Node2D
{
	public Node2D ParentCard;
	public CardStickerPrefab PrefabTemplate;

	public List<CardStickerPrefab> Prefabs = new List<CardStickerPrefab>();
	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (ParentCard == null)
		{
			return;
		}
		if (ParentCard.LogicalCard == null)
		{
			return;
		}
		var stickersThatShouldExist = ParentCard.LogicalCard.Stickers;
		var stickersThatCurrentlyExist = Prefabs.Select(item => item.Sticker);

		// remove any that don't belong
		foreach (var sticker in stickersThatCurrentlyExist)
		{
			if (!stickersThatShouldExist.Contains(sticker))
			{
				Prefabs.Remove(sticker.Prefab);
				sticker.Prefab.gameObject.Despawn();
				continue;
			}
		}

		foreach(var sticker in stickersThatShouldExist)
		{
			if (!stickersThatCurrentlyExist.Contains(sticker))
			{
				var newPrefab = PrefabTemplate.Spawn(this.transform);
				newPrefab.Sticker = sticker;
				sticker.Prefab = newPrefab;
				Prefabs.Add(sticker.Prefab);
			}
		}

	}
}
 */