namespace GodotStsXcomalike.src.ironlordbyron.CSharp.Cards.ArchonCards.Effects
{
    public static class ArchonBattleRules
    {

        public static void Manuever(AbstractBattleUnit target)
        {
            if (target.HasStatusEffect<AdvancedStatusEffect>())
            {
                ActionManager.Instance.RemoveStatusEffect<AdvancedStatusEffect>(target);
            }
            else
            {
                ActionManager.Instance.ApplyStatusEffect(target, new AdvancedStatusEffect(), 1);
            }
        }
    }
}