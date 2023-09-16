using Godot;
using System;

public class BattleBoard : Board
{
    private Control counters;
    private CheckBox fancyMovementToggle;
    private CheckBox ovalHandToggle;
    private OptionButton scalingFocusOptions;
    private CheckBox debug;
    private Label seedLabel;
    private PopupDialog deckBuilderPopup;
    private YourCFCType cfc;  // Replace `YourCFCType` with the actual type of `cfc`
    
    // Called when the node enters the scene tree for the first time
    public override void _Ready()
    {
        counters = GetNode<Control>("Counters");
        fancyMovementToggle = GetNode<CheckBox>("FancyMovementToggle");
        ovalHandToggle = GetNode<CheckBox>("OvalHandToggle");
        scalingFocusOptions = GetNode<OptionButton>("ScalingFocusOptions");
        debug = GetNode<CheckBox>("Debug");
        seedLabel = GetNode<Label>("SeedLabel");
        deckBuilderPopup = GetNode<PopupDialog>("DeckBuilderPopup");
        cfc = /* ... initialize or fetch your cfc instance ... */;

        // Connect signals
        fancyMovementToggle.Pressed = cfc.game_settings.fancy_movement;
        ovalHandToggle.Pressed = cfc.game_settings.hand_use_oval_shape;
        scalingFocusOptions.Selected = cfc.game_settings.focus_style;
        debug.Pressed = cfc._debug;
        debug.Pressed = cfc._debug;
        deckBuilderPopup = GetNode<PopupDialog>("DeckBuilderPopup");
        cfc = (CFControl)GD.Global("cfc");

        // Map node and other initializations
        cfc.MapNode(this);
        fancyMovementToggle.Pressed = cfc.game_settings.fancy_movement;
        ovalHandToggle.Pressed = cfc.game_settings.hand_use_oval_shape;
        scalingFocusOptions.Selected = cfc.game_settings.focus_style;
        debug.Pressed = cfc._debug;

        if (!cfc.ut)
        {
            cfc.game_rng_seed = CFUtils.GenerateRandomSeed();  // Ensure CFUtils.GenerateRandomSeed method exists
            seedLabel.Text = "Game Seed is: " + cfc.game_rng_seed;
        }
        if (!GetTree().Root.HasNode("Gut"))
        {
            LoadTestCards(false);
        }

        // Connect signals
        deckBuilderPopup.Connect("popup_hide", this, nameof(OnDeckBuilderHide));

        fancyMovementToggle.Connect("toggled", this, nameof(OnFancyMovementToggleToggled));
        ovalHandToggle.Connect("toggled", this, nameof(OnOvalHandToggleToggled));
    }
    public void OnFancyMovementToggleToggled(bool buttonPressed)
    {
        cfc.SetSetting("fancy_movement", fancyMovementToggle.Pressed);
    }

    public void OnOvalHandToggleToggled(bool buttonPressed)
    {
        cfc.SetSetting("hand_use_oval_shape", ovalHandToggle.Pressed);
        foreach (var c in cfc.NMAP.hand.GetAllCards())  // Ensure GetAllCards method exists and returns an enumerable
        {
            c.ReorganizeSelf();  // Ensure ReorganizeSelf method exists
        }
    }
    public void OnReshuffleAllDeckPressed()
    {
        ReshuffleAllInPile(cfc.NMAP.deck);
    }

    public void OnReshuffleAllDiscardPressed()
    {
        ReshuffleAllInPile(cfc.NMAP.discard);
    }
    public void OnReshuffleAllDeckPressed()
    {
        ReshuffleAllInPile(cfc.NMAP.deck);
    }

    public void OnReshuffleAllDiscardPressed()
    {
        ReshuffleAllInPile(cfc.NMAP.discard);
    }

    public void ReshuffleAllInPile(Node pile = null)
    {
        if (pile == null)
        {
            pile = cfc.NMAP.deck;
        }

        foreach (var c in GetTree().GetNodesInGroup("cards"))
        {
            if (c.GetParent() != pile && c.state != Card.CardState.DECKBUILDER_GRID)  // Ensure Card and CardState enums are defined
            {
                c.MoveTo(pile);  // Ensure MoveTo method exists
                GD.Yield(GetTree().CreateTimer(0.1f), "timeout");
            }
        }

        var lastCard = pile.GetTopCard();  // Ensure GetTopCard method exists
        if (lastCard._tween.IsActive())  // Ensure _tween property and IsActive method exist
        {
            GD.Yield(lastCard._tween, "tween_all_completed");
        }

        GD.Yield(GetTree().CreateTimer(0.2f), "timeout");
        pile.ShuffleCards();  // Ensure ShuffleCards method exists
    }

    public void OnScalingFocusOptionsItemSelected(int index)
    {
        cfc.SetSetting("focus_style", index);
    }

    // ... (more translated methods will be added here)

    // At the end of _Ready method, add signal connections for the above methods
    public override void _Ready()
    {
        // ... (previous _Ready code)

        GetNode<Button>("ReshuffleAllDeck").Connect("pressed", this, nameof(OnReshuffleAllDeckPressed));
        GetNode<Button>("ReshuffleAllDiscard").Connect("pressed", this, nameof(OnReshuffleAllDiscardPressed));
        scalingFocusOptions.Connect("item_selected", this, nameof(OnScalingFocusOptionsItemSelected));

        // ... (more signal connections for other controls will be added here)
    }

}