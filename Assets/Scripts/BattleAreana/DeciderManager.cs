using UnityEngine;

public class DeciderManager : MonoBehaviour
{
    public static DeciderManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void DecideCard(CardInstance playerCard, CardInstance enemyCard, float baseDamagePlayer, float baseDamageEnemy)
    {
        /*RULE WHAT CARD DO TO ENEMY OR PLAYER
          Both run at same times
          RULES:
            CardType.Movement
              1) Both can deal baseDamage each other
              2) Went 1 of them is a MovementCardType.Defensive 
                 1. Can neglected all baseDamage
                 2. Can reduce 50% of the baseDamage (roundUP/roundDown the maxHealth)
            
            CardType.Item
              1) Both can deal baseDamage each other
              2) Went using the healing potion same as dealing baseDamage
                 Example : Player daealingDamage, enemy Healing -> Enemy Take Damage then Heal reduce 50%
         */

        if (enemyCard.cardData.cardType == CardType.Special)
        {
            //Change Scene
            return;
        }

       if (playerCard != null)
        {
            if (playerCard.cardData.cardType == CardType.Item)
            {
                InventoryManager.instance.RemoveCard(playerCard.cardData);
            }
        }

        int playerDamage = 0;
        bool playerDefending = false;
        bool playerHealing =  false;

        int enemyDamage = 0;
        bool enemyDefending = false;
        bool enemyHealing = false;

        #region Calculate
        // Player
        if (playerCard != null && playerCard.cardData != null)
        {
            if (playerCard.cardData.cardType == CardType.Movement)
            {
                playerDamage = playerCard.cardData.damageMovement;

                if (playerCard.cardData.movementCardType == MovementCardType.Defensive)
                    playerDefending = true;
            }
            else if (playerCard.cardData.cardType == CardType.Item)
            {
                if (playerCard.cardData.itemCardType == ItemCardType.HealthPotion)
                {
                    playerHealing = true;
                }
                else
                {
                    playerDamage = playerCard.cardData.itemDamage;
                }
            }
        }

        // Enemy
        if (enemyCard != null && enemyCard.cardData != null)
        {
            if (enemyCard.cardData.cardType == CardType.Movement)
            {
                enemyDamage = enemyCard.cardData.damageMovement;

                if (enemyCard.cardData.movementCardType == MovementCardType.Defensive)
                    enemyDefending = true;
            }
            else if (enemyCard.cardData.cardType == CardType.Item)
            {
                if (enemyCard.cardData.itemCardType == ItemCardType.HealthPotion)
                {
                    enemyHealing = true;
                }
                else
                {
                    enemyDamage = enemyCard.cardData.itemDamage;
                }
            }
        }
        #endregion

        #region Defending Section

        if (playerDefending && playerCard?.cardData != null && enemyCard?.cardData != null)
        {
            if (enemyCard.cardData.offensiveCardType == OffensiveCardType.LightAttack &&
                playerCard.cardData.defensiveCardType == DefensiveCardType.Parry)
            {
                Debug.Log($"Player parried Light Attack!");
                enemyDamage = 0;
            }
            else if (enemyCard.cardData.offensiveCardType == OffensiveCardType.HeavyAttack &&
                     playerCard.cardData.defensiveCardType == DefensiveCardType.Dodge)
            {
                Debug.Log($"Player dodged Heavy Attack!");
                enemyDamage = 0;
            }
            else if (enemyDamage > 0)
            {
                enemyDamage = Mathf.CeilToInt(enemyDamage * 0.5f);
            }
        }

        if (enemyDefending && enemyCard?.cardData != null && playerCard?.cardData != null)
        {
            if (playerCard.cardData.offensiveCardType == OffensiveCardType.LightAttack &&
                enemyCard.cardData.defensiveCardType == DefensiveCardType.Parry)
            {
                Debug.Log($"Enemy parried Light Attack!");
                playerDamage = 0;
            }
            else if (playerCard.cardData.offensiveCardType == OffensiveCardType.HeavyAttack &&
                     enemyCard.cardData.defensiveCardType == DefensiveCardType.Dodge)
            {
                Debug.Log($"Enemy dodged Heavy Attack!");
                playerDamage = 0;
            }
            else if (playerDamage > 0)
            {
                playerDamage = Mathf.CeilToInt(playerDamage * 0.5f);
            }
        }


        #endregion

        #region Healing Section
        int playerHeal = 0;
        int enemyHeal = 0;

        if (playerHealing && playerCard != null)
            playerHeal = playerCard.cardData.itemHealAmount;

        if (enemyHealing && enemyCard != null)
            enemyHeal = enemyCard.cardData.itemHealAmount;

        // Healing reduced if also taking baseDamage
        if (playerHeal > 0 && enemyDamage > 0)
            playerHeal = Mathf.FloorToInt(playerHeal * 0.5f);

        if (enemyHeal > 0 && playerDamage > 0)
            enemyHeal = Mathf.FloorToInt(enemyHeal * 0.5f);
        #endregion

        if (playerDamage > 0)
            DamageManager.instance.DealDamageToTarget(TargetType.Enemy, playerDamage + (int)baseDamagePlayer);

        if (enemyDamage > 0)
            DamageManager.instance.DealDamageToTarget(TargetType.Player, enemyDamage + +(int)baseDamageEnemy);

        if (playerHeal > 0)
            DamageManager.instance.HealToTarget(TargetType.Player, playerHeal);

        if (enemyHeal > 0)
            DamageManager.instance.HealToTarget(TargetType.Enemy, enemyHeal);
    }
}
