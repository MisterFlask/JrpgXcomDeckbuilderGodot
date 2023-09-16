using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeAssets.BattleEntities.Augmentations
{
    public static class BasicAugmentations
    {
        public static AbstractSoldierPerk GrantsPowerAugmentation = new GrantsStatusEffectPerk("Power Fist", "Increases this soldier's Strength by 1.", new StrengthStatusEffect(), 1);
        public static AbstractSoldierPerk GrantsAblativeArmorAugmentation = new GrantsStatusEffectPerk("Ablative Armor", "Decreases ALL damage taken by 1", new ArmoredStatusEffect(), 1);
        public static AbstractSoldierPerk IncreaseHpOnGettingNewCardsPerk = new HpGainOnCardAddedPerk();
        public static AbstractSoldierPerk GrantsDexterityAugmentation = new GrantsStatusEffectPerk("Power Legs", "Increases this soldier's Dexterity by 1.", new DexterityStatusEffect(), 1);
        public static AbstractSoldierPerk GrantPowerToSpecificCardPerk = new GrantsStatusEffectPerk("Buzzsaw", "Increases the power of a random card in your deck by 4.", new DexterityStatusEffect(), 1);

        public static List<AbstractSoldierPerk> BasicAugmentationsList = new List<AbstractSoldierPerk>
        {
            GrantsPowerAugmentation,
            GrantsAblativeArmorAugmentation,
            IncreaseHpOnGettingNewCardsPerk,
            GrantsDexterityAugmentation
        }; 
    }

    public class DealGreaterDamageToEnemiesWithStatusEffectPerk : AbstractSoldierPerk
    {
        int DamageChange { get; set; }
        AbstractStatusEffect TargetEffect { get; set; }
        string MyName { get; set; }
        public DealGreaterDamageToEnemiesWithStatusEffectPerk(
            AbstractStatusEffect abstractStatusEffectType,
            int damageChange,
            string name)
        {
            DamageChange = damageChange;
            TargetEffect = abstractStatusEffectType;
            this.MyName = name;
        }

        public override string Description()
        {
            return $"Deals {Stacks} greater damage to enemies afflicted with " + TargetEffect.Name;
        }

        public override string Name()
        {
            return MyName;
        }

        public override void PerformAtBeginningOfCombat(AbstractBattleUnit soldierAffected)
        {
            soldierAffected.ApplyStatusEffect(new DealsExtraDamageToBurningStatusEffect(), Stacks);
        }
    }

    public class DealsExtraDamageToBurningStatusEffect: AbstractDamageModifierToEnemiesWithStatusEffect
    {
        public DealsExtraDamageToBurningStatusEffect() : base(new BurningStatusEffect())
        {
        }
    }

    /// <summary>
    /// Note: made this an abstract class because I'm just having it be an invariant that if two status effects have the same class,
    /// they ARE the same status effect.
    /// </summary>
    public abstract class AbstractDamageModifierToEnemiesWithStatusEffect : AbstractStatusEffect
    {
        public AbstractStatusEffect TargetStatusEffect { get; private set; }
        public AbstractDamageModifierToEnemiesWithStatusEffect(AbstractStatusEffect targetStatusEffect)
        {
            Name = "Damage Mod: " + targetStatusEffect.Name;
            TargetStatusEffect = targetStatusEffect;
        }

        public override string Description =>  $"Deals {Stacks} greater damage to enemies afflicted with " + TargetStatusEffect?.Name ?? "UNKNOWN";

    }

    public class DuplicateNextCardTwicePerk : AbstractSoldierPerk
    {
        public DuplicateNextCardTwicePerk()
        {

        }

        public override void ModifyCardsUponAcquisition(AbstractCard card, AbstractBattleUnit soldierAffected)
        {
            if (Stacks <= 0)
            {
                return;
            }
            Stacks--;
            soldierAffected.AddCardToPersistentDeck(card.CopyCard());
            soldierAffected.AddCardToPersistentDeck(card.CopyCard());
        }

        public override string Name()
        {
            return "Unsupervised Learning";
        }

        public override string Description()
        {
            return "Whenever you add a card to your deck, add a duplicate of that card TWICE.";
        }
    }



    public class HpGainOnCardAddedPerk : AbstractSoldierPerk
    {
        public HpGainOnCardAddedPerk()
        {

        }

        public override void ModifyCardsUponAcquisition(AbstractCard card, AbstractBattleUnit soldierAffected)
        {
            soldierAffected.MaxHp += Stacks;
        }

        public override string Name()
        {
            return "Supervised Learner Module";
        }

        public override string Description()
        {
            return $"Whenever you add a card to your deck, increase your HP by {Stacks}.";
        }
    }

    /// <summary>
    /// Applies a card sticker to a random card in your deck.
    /// </summary>
    public abstract class ApplyStickerToRandomCardPerk : AbstractSoldierPerk
    {
        public AbstractCardSticker StickerToApply;

        public virtual bool IsApplicableToCard(AbstractCard card)
        {
            return StickerToApply.IsCardTagApplicable(card);
        }

        public string GivenName { get; set; }
        public string GivenDescription { get; set; }
        public ApplyStickerToRandomCardPerk()
        {

        }

        public override bool CanAssignToSoldier(AbstractBattleUnit soldier)
        {
            var applicableCards = soldier.CardsInPersistentDeck
                            .Where(item => IsApplicableToCard(item));
            return applicableCards.Any();
        }

        public override void OnAssignment(AbstractBattleUnit abstractBattleUnit)
        {
            //todo
            var applicableCard = abstractBattleUnit.CardsInPersistentDeck
                .Where(item => IsApplicableToCard(item))
                .PickRandom();

            applicableCard.AddSticker(StickerToApply);
        }

        public override string Name()
        {
            return GivenName;
        }

        public override string Description()
        {
            return GivenDescription;
        }
    }

}