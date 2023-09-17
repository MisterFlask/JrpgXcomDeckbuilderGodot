namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.ChessCourt
{

    /// <summary>
    /// CHESSCOURT MECHANICS
    /// 
    /// 1)  Guarded:  Knights have the Pawn-Guarded buff, causing them to take -80% damage while Pawns exist.
    /// Similar exists for King and Queen and Bishop.
    /// 
    /// 2) Challenge: Card added to your hand at start of combat which summons a Knight or a Bishop in exchange for money on victory.
    /// Only playable on first turn of combat, and is destroyed after.
    /// 
    /// Pawns: Deal a small amount of damage, and shield every other turn.
    /// Bishops:  Deal stress damage exclusively.
    /// Rooks:  Deal lots of physical damage and guard each turn.  Gain strength each turn.
    /// Knight:  Increase strength and Armor of pawns each turn. (Armor reduces damage to a minimum of 1.)
    /// King:  Summons pawns and does single-target damage each turn.  Artifact.
    /// Queen:  Does AoE damage each turn she goes undamaged.  Increases damage necessary to prevent AoE attack by 5 each turn.
    /// </summary>
    public class ChessCourt
    {



        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}