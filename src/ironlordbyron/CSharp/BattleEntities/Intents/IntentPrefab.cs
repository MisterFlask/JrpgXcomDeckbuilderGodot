using Godot;

public class IntentPrefab : Node2D
{
    public Label Text;
    public Image Picture;
    public AbstractIntent UnderlyingIntent { get; set; }

    private Color OriginalImageColor { get; set; }
    private Color BrighterImageColor { get; set; }

    // GDScript signals equivalent in C#
    public override void _Ready()
    {
        this.Connect("mouse_entered", this, nameof(OnMouseEntered));
        this.Connect("mouse_exited", this, nameof(OnMouseExited));
        Init();
    }

    public void OnMouseEntered()
    {
        BattleScreenPrefab.IntentMousedOver = this.UnderlyingIntent;
        Highlight(this.Picture);
    }

    public void OnMouseExited()
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
        OriginalImageColor = Picture.Modulate;
        BrighterImageColor = Picture.Modulate * 1.5f; // TODO: Adjust this to correctly brighten the color
    }

    public override void _Process(float delta)
    {
        if (UnderlyingIntent == null)
        {
            return;
        }

        var currentMousedOverUnit = BattleScreenPrefab.BattleUnitMousedOver;
        if (this.UnderlyingIntent.Source == currentMousedOverUnit || this.UnderlyingIntent.UnitsTargeted.Contains(currentMousedOverUnit))
        {
            GD.Print("Highlighting intent");
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
        spriteImage.Modulate = OriginalImageColor;
    }

    private void Highlight(Image spriteImage)
    {
        spriteImage.Modulate = new Color(1, 1, 0, 1); // Yellow color
    }

    public void HideAndDestroy()
    {
        this.GetParent().RemoveChild(this);
        this.QueueFree();
    }
}
