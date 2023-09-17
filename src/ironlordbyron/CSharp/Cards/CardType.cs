public class CardType
{
    string Name { get; set; }

    public CardType(string name)
    {
        this.Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    public static CardType AttackCard = new CardType("Attack");
    public static CardType SkillCard = new CardType("Skill");
    public static CardType PowerCard = new CardType("Power");
    public static CardType ConditionCard = new CardType("Condition");
    public static CardType ErosionCard = new CardType("Erosion");

}
