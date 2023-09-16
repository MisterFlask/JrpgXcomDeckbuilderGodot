using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntentPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CustomGuiText Text;
    public Image Picture;
    public AbstractIntent UnderlyingIntent { get; set; }

    private Color OriginalImageColor { get; set; }
    private Color BrighterImageColor { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BattleScreenPrefab.IntentMousedOver = this.UnderlyingIntent;
        Highlight(this.Picture);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BattleScreenPrefab.IntentMousedOver = null;
        RemoveHighlights(this.Picture);
    }

    public void SetText(string text)
    {
        Text.SetText(text);
    }
    public void Init()
    {
        OriginalImageColor = Picture.color;
        BrighterImageColor = Picture.color * 1.5f; // TODO: This isn't actually brighter.
    }

    public void Update()
    {
        if (UnderlyingIntent == null)
        {
            return;
        }

        var currentMousedOverUnit = BattleScreenPrefab.BattleUnitMousedOver;
        if (this.UnderlyingIntent.Source == currentMousedOverUnit || this.UnderlyingIntent.UnitsTargeted.Contains(currentMousedOverUnit))
        {
            Debug.Log("Highlighting intent");
            Highlight(this.Picture);
        }
        else
        {
            RemoveHighlights(this.Picture);
        }

        if (this.UnderlyingIntent.Source.IsDead)
        {
            HideAndDestroy();
        }

        this.Text.SetText(UnderlyingIntent.GetOverlayText());
    }
    private void RemoveHighlights(Image spriteImage)
    {
        spriteImage.color = OriginalImageColor;
    }

    private void Highlight(Image spriteImage)
    {
        spriteImage.color = Color.yellow;
    }

    public void HideAndDestroy()
    {
        this.transform.parent = null;
        Destroy(this.gameObject);
    }

    // mouseover behavior:  Highlight unit

}
