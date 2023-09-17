namespace GodotStsXcomalike.src.ironlordbyron.CSharp.GameLogic
{
    /// <summary>
    /// Rookies can be bought for 10 money
    /// </summary>
    public class RookiePurchaseAction
    {


        public static void PurchaseRookie()
        {
            GameState.Instance.Credits -= 10;
            GameState.Instance.PersistentCharacterRoster.Add(Soldier.GenerateFreshRookie());
        }

        public static bool CanPurchaseRookie()
        {
            return GameState.Instance.Credits >= 10;
        }

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