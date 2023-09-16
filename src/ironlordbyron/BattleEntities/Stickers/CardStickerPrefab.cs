using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardStickerPrefab : MonoBehaviour
{
    public Image image;

    public AbstractCardSticker Sticker { get; set; }

    public void Update()
    {
        if (Sticker == null)
        {
            return;
        }
        image.SetProtoSprite(Sticker.ProtoSprite);
    }
}
