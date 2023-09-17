
using Assets.CodeAssets.Cards;
using Assets.CodeAssets.GameLogic;
using Assets.CodeAssets.ParticleSystemEffects;
using Assets.CodeAssets.UI.CardParts;
using HyperCard;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractCard
{

	public AbstractCard ReferencesAnotherCard { get; set; }

	public int MagicNumber { get; set; }
	public int RestOfTurnCostMod { get; set; } = 0;

	public List<AbstractCostModifier> PersistentCostModifiers = new List<AbstractCostModifier>();


	protected Color GetDefaultColoration(string cardName)
	{
		var hashcodeOfName = cardName.GetHashCode();
		var g = hashcodeOfName % 255;
		hashcodeOfName = hashcodeOfName / 255;
		var r = hashcodeOfName % 255;
		hashcodeOfName = hashcodeOfName / 255;
		var b = hashcodeOfName % 255;
		return new Color(r, g, b);
	}

	public bool WasCreated { get; set; } = false;

	/// <summary>
	/// if this is empty, any class can use it.
	/// </summary>
	public List<Type> SoldierClassCardPools { get; } = new List<Type>();

	public Guid OwnerGuid;
	// TODO: Move to a more explicit magic-words system
	public List<MagicWord> MagicWordsReferencedOnThisCard { get; set; } = new List<MagicWord>();

	public bool HasDamageModifier<T>() where T:DamageModifier
	{
		return DamageModifiers.Any(item => item is T);
	}

	/// <summary>
	///  This is just a thing tracking the countdown on a card.
	/// </summary>
	public int Countdown { get; set; } = -1;

	/// <summary>
	/// I just added this for debugging purposes
	/// </summary>
	public GameState GameState => GameState.Instance;

	public ProtoGameSprite ProtoSprite = ProtoGameSprite.FromGameIcon();
	public int FatigueCost => 1;
	public AbstractBattleUnit Owner { get; set; }

	public string Name { get; set; } = "Name";

	public Rarity Rarity { get; set; } = Rarity.COMMON;

	public int UpgradeQuantity { get; set; } = 0;

	public TargetType TargetType { get; set; } = TargetType.NO_TARGET_OR_SELF;

	public CardType CardType { get; set; }

	public string Id { get; set; } = Guid.NewGuid().ToString();

	public int BaseDamage { get; set; } = 0;

	public int BaseDefenseValue { get; set; } = 0;

	public List<string> CardTags { get; set; } = new List<string>();
	public List<CardVisualTag> CardVisualTags { get; set; } = new List<CardVisualTag>();
	/// <summary>
	/// Similar to focus; non-general-purpose and not applicable to all cards
	/// </summary>
	public int BaseTechValue { get; set; } = 0;

	public int TechValue => BaseTechValue;

	public int CurrentToughness { get; set; } = 0;

	public List<AbstractCardSticker> Stickers = new List<AbstractCardSticker>();

	#region convenience functions

	public T GetStickerOfType<T>() where T : AbstractCardSticker
	{
		return Stickers.FirstOrDefault(item => item.GetType() == typeof(T)) as T;
	}
	public List<T> GetStickersOfType<T>() where T : AbstractCardSticker
	{
		return Stickers.Where(item => item.GetType() == typeof(T)).Select(item => item as T).ToList();
	}

	public T GetDamageModifierOfType<T>() where T: DamageModifier
	{
		return DamageModifiers.FirstOrDefault(item => item is T) as T;
	}

	public int displayedDamage()
	{
		return BattleRules.GetDisplayedDamageOnCard(this);
	}

	public string ownerDisplayString()
	{
		if (Owner == null)
		{
			return "Owner";
		}
		else
		{
			return Owner.GetDisplayName(DisplayNameType.SHORT_NAME);
		}
	}

	public ActionManager action()
	{
		return ServiceLocator.GetActionManager();
	}

	public List<AbstractBattleUnit> enemies()
	{
		return ServiceLocator.GameState().EnemyUnitsInBattle;
	}
	public List<AbstractBattleUnit> allies()
	{
		return ServiceLocator.GameState().AllyUnitsInBattle;
	}

	public GameState state()
	{
		return ServiceLocator.GameState();
	}

	public List<AbstractCard> CardsInHand => state().Deck.Hand;
	#endregion


	public List<DamageModifier> DamageModifiers = new List<DamageModifier>();


	public AbstractCard(CardType cardType = null)
	{
		this.CardType = cardType ?? CardType.SkillCard;
		this.Name = this.GetType().Name;
	}
	private int? _staticBaseEnergyCost = null;

	/// <summary>
	/// This represents the energy cost PHYSICALLY ON THE CARD
	/// that means things like stickers can modify this.
	/// </summary>
	/// <returns></returns>
	public virtual int BaseEnergyCost()
	{
		return (StaticBaseEnergyCost ?? 1);
	}

	/// <summary>
	/// This IS allowed to take into account the current UI state (e.g. cursor position)
	/// </summary>
	/// <returns></returns>
	public int GetDisplayedEnergyCost()
	{
		var targetedUnitIfAny = GameState.Instance.CharacterSelected;

		var costMod = 0;
		if (targetedUnitIfAny != null)
		{
			costMod = targetedUnitIfAny.StatusEffects.Sum(item => item.GetTargetedCostModifier(this));
		}

		return BaseEnergyCost()
			+ RestOfTurnCostMod
			+ costMod
			+ PersistentCostModifiers
				.Select(item => item.GetCostModifier())
				.Sum();
	}

	/// <summary>
	/// This represents the energy cost ACTUALLY PAID (e.g. via bloodprice)
	/// </summary>
	/// <returns></returns>
	public virtual EnergyPaidInformation GetNetEnergyCost()
	{
		var cost = GetDisplayedEnergyCost();

		return new EnergyPaidInformation
		{
			EnergyCost = cost
		};
	}


	public abstract string DescriptionInner();

	public string Description()
	{
		var baseDescription = DescriptionInner();
		foreach (var sticker in Stickers)
		{
			baseDescription += "\n" + sticker.CardDescriptionAddendum();
		}

		if (!DamageModifiers.IsEmpty())
		{
			baseDescription += "\n<color=green>";
			baseDescription += string.Join(",", DamageModifiers.Select(item => item.CardDescriptionAddendum));
			baseDescription += "</color>";
		}

		return baseDescription;
	}

	public virtual bool CanAfford()
	{
		return ServiceLocator.GameState().energy >= GetNetEnergyCost().EnergyCost;
	}

	public virtual CanPlayCardQueryResult CanPlayInner(AbstractBattleUnit target)
	{
		return CanPlayCardQueryResult.CanPlay();
	}

	public CanPlayCardQueryResult CanPlay(AbstractBattleUnit target)
	{
		if (Unplayable)
		{
			return CanPlayCardQueryResult.CannotPlay("This card is unplayable.");
		}

		if (!CanPlayInner(target).Playable)
		{
			return CanPlayInner(target);
		}

		if (CanAfford())
		{
			return CanPlayCardQueryResult.CanPlay();
		}
		else
		{
			return CanPlayCardQueryResult.CannotPlay("I don't have the energy for this.");
		}
	}

	public virtual void OnDrawInner()
	{

	}

	public void OnDraw()
	{
		OnDrawInner();
		foreach (var sticker in Stickers)
		{
			sticker.OnCardDrawn(this);
		}
	}

	/// <summary>
	///  returns -1 if the card's not in hand.
	/// </summary>
	/// <returns></returns>
	public int GetCardPosition()
	{
		var cardsInHand = state().Deck.Hand;
		var index = cardsInHand.IndexOf(this);
		return index;
	}

	public virtual bool ShouldRetainCardInHandAtEndOfTurn()
	{
		return false;
	}

	public abstract void OnPlay(AbstractBattleUnit target, EnergyPaidInformation energyPaid);

	public virtual void InHandAtEndOfTurnAction()
	{

	}

	public virtual void OnStartup()
	{

	}

	public int DisplayedDefense()
	{
		return BattleRules.GetDisplayedDefenseOnCard(this);
	}
	public int DisplayedDamage(int? baseDamageOverride =null)
	{
		return BattleRules.GetDisplayedDamageOnCard(this, baseDamageOverride);
	}

	public void PlayCardFromHandIfAble_Action(AbstractBattleUnit target)
	{
		if (!CanPlay(target).Playable)
		{
			return;
		}
		var costPaid = BattleRules.ProcessPlayingCardCost(this);
		EvokeCardEffect(target, costPaid);
		BattleRules.RunOnPlayCardEffects(this, target, costPaid);
		if (state().Deck.Hand.Contains(this))
		{
			state().Deck.MoveCardToPile(this, CardPosition.DISCARD);
			ServiceLocator.GetCardAnimationManager().MoveCardToDiscardPile(this, assumedToExistInHand: true);
		}
		state().cardsPlayedThisTurn += 1;
	}

	public virtual void OnManualDiscard()
	{

	}

	public void EvokeCardEffect(AbstractBattleUnit target, EnergyPaidInformation costPaid = null)
	{
		if (costPaid == null)
		{
			costPaid = new EnergyPaidInformation
			{
				EnergyCost = 0
			};
		}
		OnPlay(target, costPaid);
		foreach (var sticker in Stickers)
		{
			sticker.OnThisCardPlayed(this, target);
		}

	}

	public Card CreateHyperCard()
	{
		var cardInstantiator = ServiceLocator.GetCardInstantiator();
		var card = cardInstantiator.CreateCard();
		card.SetToAbstractCardAttributes(this);
		card.LogicalCard = this;
		card.GetComponent<PlayerCard>().LogicalCard = this;

		return card;
	}

	private string ConvertTypeToString(CardType type)
	{
		return type.ToString();
	}

	public virtual void CopyCardInner(AbstractCard card)
	{

	}

	public void Upgrade()
	{
		this.UpgradeQuantity += 1;
	}

	/// <summary>
	/// logicallyIdenticalToExistingCard just means that the card is keeping its ID, since it's logically the same card as in the character's persistent decks.
	/// This distinction here matters because things that happen to cards in combat don't in general affect the character's persistent deck.
	/// It's pretty much ONLY true when we're first initializing the battle deck in a combat.
	/// </summary>
	public AbstractCard CopyCard(bool logicallyIdenticalToExistingCard = false)
	{
		var copy = (AbstractCard)this.MemberwiseClone();
		copy.Stickers = new List<AbstractCardSticker>();
		foreach (var sticker in copy.Stickers)
		{
			var newSticker = sticker.CopySticker();
			copy.Stickers.Add(newSticker);
		}

		CopyCardInner(copy);
		if (!logicallyIdenticalToExistingCard)
		{
			copy.Id = Guid.NewGuid().ToString();
		}
		copy._Initialize();
		return copy;
	}


	public Node2D FindCorrespondingHypercard()
	{
		throw new Exception("not implemented");
		//return ServiceLocator.GetCardAnimationManager().GetGraphicalCard(this);
	}

	public void _Initialize()
	{
		CardVisualTags.Clear();
		// does postprocessing work
		var cardType = CardType;
		if (cardType == CardType.AttackCard)
		{
			//CardVisualTags.Add(CardVisualTag.AttackIcon);
		}
		else if (cardType == CardType.SkillCard)
		{
			//CardVisualTags.Add(CardVisualTag.SkillIcon);
		}
		else if (cardType == CardType.PowerCard)
		{
			//CardVisualTags.Add(CardVisualTag.PowerIcon);
		}
	}

	public void AddSticker(AbstractCardSticker sticker)
	{
		sticker.CardAttachedTo = this;
		Stickers.Add(sticker);
	}

	public bool HasSticker<T>() where T: AbstractCardSticker
	{
		return Stickers.Any(item => item is T);
	}

	public void RemoveSticker<T>() where T : AbstractCardSticker
	{
		var sticker = Stickers.FirstOrDefault(item => item.GetType() == typeof(T));
		if (sticker == null)
		{
			return;
		}
		Stickers.Remove(sticker);
	}

	public bool Unplayable { get; set; }
	public int? StaticBaseEnergyCost { get => _staticBaseEnergyCost; set
		{
			if (value < 0)
			{
				value = 0;
			}
			_staticBaseEnergyCost = value;
		}
	}
	public void SetCommonCardAttributes(string name,
		Rarity rarity,
		TargetType targetType,
		CardType cardType,
		int baseEnergyCost,
		Type soldierClassType = null,
		ProtoGameSprite protoGameSprite = null)
	{
		this.Name = name;
		this.Rarity = rarity;
		this.TargetType = targetType;
		this.CardType = cardType;
		this.StaticBaseEnergyCost = baseEnergyCost;
		this.SoldierClassCardPools.Add(soldierClassType);
		if (protoGameSprite != null)
		{
			this.ProtoSprite = protoGameSprite;
		}
		else if (ProtoSprite == null)
		{
			var color = GetDefaultColoration(Name);
			ProtoSprite = ProtoGameSprite.FromGameIcon(color: color);
		}
	}


	public bool IsValidForClass(AbstractBattleUnit unit)
	{
		if (unit == null)
		{
			return false;
		}

		return SoldierClassCardPools.IsEmpty()
			|| SoldierClassCardPools.Contains(unit.SoldierClass.GetType());
	}

	public bool NameContains(string s)
	{
		return this.Name.ToLower().Contains(s.ToLower());
	}

	/// <summary>
	/// 
	/// </summary>
	public AbstractCard CorrespondingPermanentCard()
	{
		var persistentDeck = this.Owner.CardsInPersistentDeck;
		var permanentCard = persistentDeck.First(item => item.Id == this.Id);
		return permanentCard;
	}

	public virtual void IsNotExhaustedInDeckAtEndOfBattle()
	{

	}


	public virtual void OnProcWhileThisIsInDeck(AbstractProc proc)
	{

	}

	protected int GetStacksOf<T>() where T: AbstractStatusEffect
	{
		return Owner.GetStatusEffect<T>()?.Stacks ?? 0;
	}

	protected void Action_ApplyStatusEffectToOwner(AbstractStatusEffect effect, int stacks)
	{
		action().ApplyStatusEffect(this.Owner, effect, stacks);
	}

	protected void Action_ApplyStatusEffectToTarget(AbstractStatusEffect effect, int stacks, AbstractBattleUnit target)
	{
		action().ApplyStatusEffect(target, effect, stacks);
	}
	protected void Action_ApplyDefenseToTarget(AbstractBattleUnit target, int? block = null)
	{
		action().ApplyDefense(target, this.Owner, block ?? BaseDefenseValue);
	}
	protected void Action_AttackTarget(AbstractBattleUnit target)
	{
		action().AttackWithCard(this, target);
	}
	protected void Action_AttackAllEnemies()
	{
		foreach (var enemy in GameState.Instance.EnemyUnitsInBattle)
		{
			action().AttackWithCard(this, enemy);
		}
	}
	protected void Action_DefendAllAllies()
	{
		foreach(var ally in GameState.Instance.AllyUnitsInBattle)
		{
			action().ApplyDefenseFromCard(this, ally);
		}
	}
	protected void Action_Exhaust()
	{
		action().ExhaustCard(this);
	}

	public SpecialEffect GetSpecialEffect_Nullable(AbstractBattleUnit target)
	{
		if (this.CardType == CardType.AttackCard && target != null && Owner != null)
		{
			return CompositeSpecialEffect.DefaultAttackEffect_WithMuzzleFlash(target, this.Owner);
		}

		return null;
	}
}

public class DamageBlob
{
	public AbstractCard CardIfAny { get; set; }

	public int Damage { get; set; }
	public bool IsAttackDamage { get; set; }
}


public enum Rarity
{
	COMMON,UNCOMMON,RARE,BASIC,NOT_IN_POOL,ANY
}

public class TargetType
{
	public string Name { get; set; }
	public static TargetType NO_TARGET_OR_SELF = new TargetType { Name = "NO_TARGET_OR_SELF" };
	public static TargetType ENEMY = new TargetType { Name = "ENEMY" };
	public static TargetType ALLY = new TargetType { Name = "ALLY" };

	public string ToString()
	{
		return Name;
	}
}

public class BattleCardTags
{
	public static string SWARM = "swarm";
	public static string VIGIL = "vigil";
}

public class CanPlayCardQueryResult
{
	public static CanPlayCardQueryResult CannotPlay(string reason)
	{
		return new CanPlayCardQueryResult
		{
			Playable = false,
			ReasonUnplayable = reason
		};
	}

	public static CanPlayCardQueryResult CanPlay()
	{
		return new CanPlayCardQueryResult
		{
			Playable = true
		};
	}

	public string ReasonUnplayable { get; set; }
	public bool Playable { get; set; }
}

public abstract class AbstractCostModifier
{
	public abstract int GetCostModifier();
}

public class RestOfCombatCostModifier : AbstractCostModifier
{
	int modifier = 0;
	public RestOfCombatCostModifier(int mod)
	{
		this.modifier = mod;
	}
	public override int GetCostModifier()
	{
		return modifier;
	}
}
