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
            Debug.Log("Special card is been used");
            return;
        }

        if (playerCard != null && playerCard.cardData != null)
        {
            if (playerCard.cardData.cardType == CardType.Item)
            {
                if (InventoryManager.instance != null)
                {
                    InventoryManager.instance.RemoveCard(playerCard.cardData);
                }
                else
                {
                    Debug.LogError("InventoryManager.instance is NULL!");
                }
            }
        }


        int playerDamage = 0;
        bool playerDefending = false;
        bool playerHealing =  false;
        bool playerCancelAttack = false;

        int enemyDamage = 0;
        bool enemyDefending = false;
        bool enemyCancelAttack = false;

        if (playerCard != null)
        {
            Debug.Log("Player card  : " + playerCard.cardData.name);
            Debug.Log("Enemy card   : " + enemyCard.cardData.name);
        }

        #region Calculate
        // Player
        if (playerCard != null && playerCard.cardData != null)
        {
            if (playerCard.cardData.cardType == CardType.Movement)
            {
                playerDamage = playerCard.cardData.damageMovement;

                if (playerCard.cardData.movementCardType == MovementCardType.Defensive)
                {
                    playerDefending = true;
                    //Debug.Log("Player is defendeing is " + playerDefending);
                }

            }
            else if (playerCard.cardData.cardType == CardType.Item && playerCard.cardData != null)
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
        }
        #endregion

        #region Defending Section

        if (playerDefending &&
            playerCard?.cardData != null &&
            enemyCard?.cardData != null &&
            playerCard.cardData.movementCardType == MovementCardType.Defensive)
        {
            Debug.Log("Player is defendeing is " + playerDefending);

            if (enemyCard.cardData.offensiveCardType == OffensiveCardType.LightAttack &&
                playerCard.cardData.defensiveCardType == DefensiveCardType.Parry)
            {
                Debug.Log($"Player parried Light Attack!");
                AnimationManager.instance.DoPlayerAnimation(AnimationEffectType.Parry);
                SoundEffectManager.instance.PlaySoundEffectOneClip("Parry");

                enemyDamage = 0;
                enemyCancelAttack= true;
            }
            else if (enemyCard.cardData.movementCardType == MovementCardType.Offensive &&
                enemyCard.cardData.offensiveCardType == OffensiveCardType.HeavyAttack &&
                playerCard.cardData.defensiveCardType == DefensiveCardType.Dodge)
            {
                Debug.Log($"Player dodged Heavy Attack!");
                AnimationManager.instance.DoPlayerAnimation(AnimationEffectType.Dodge);
                SoundEffectManager.instance.PlaySoundEffectOneClip("Dodge");

                enemyCancelAttack = true;
                enemyDamage = 0;
            }
            else if (playerCard.cardData.defensiveCardType == DefensiveCardType.Guard && enemyDamage > 0)
            {
                AnimationManager.instance.DoPlayerAnimation(AnimationEffectType.Guard);
                SoundEffectManager.instance.PlaySoundEffectOneClip("Guard");

                enemyDamage = Mathf.CeilToInt(enemyDamage * 0.5f);
                enemyCancelAttack = true;
            }
            else
            {
                Debug.LogWarning("Player failed to def");
            }
        }

        if (enemyDefending &&
            enemyCard?.cardData != null &&
            playerCard?.cardData != null &&
            enemyCard.cardData.movementCardType == MovementCardType.Defensive)
        {
            Debug.Log("Enemy is defendeing is " + enemyDefending);

            if (playerCard.cardData.offensiveCardType == OffensiveCardType.LightAttack &&
                enemyCard.cardData.defensiveCardType == DefensiveCardType.Parry)
            {
                Debug.Log("Enemy parried Light Attack!");
                AnimationManager.instance.DoEnemyAnimation(AnimationEffectType.Parry);
                SoundEffectManager.instance.PlaySoundEffectOneClip("Parry");
                playerCancelAttack = true;
                playerDamage = 0;
            }
            if (playerCard.cardData.movementCardType == MovementCardType.Offensive &&
                playerCard.cardData.offensiveCardType == OffensiveCardType.HeavyAttack &&
                enemyCard.cardData.defensiveCardType == DefensiveCardType.Dodge)
            {
                Debug.Log("Enemy dodged Heavy Attack!");
                AnimationManager.instance.DoEnemyAnimation(AnimationEffectType.Dodge);
                SoundEffectManager.instance.PlaySoundEffectOneClip("Dodge");

                playerCancelAttack = true;
                playerDamage = 0;
            }
            else if (enemyCard.cardData.defensiveCardType == DefensiveCardType.Guard && playerDamage > 0)
            {
                AnimationManager.instance.DoEnemyAnimation(AnimationEffectType.Guard);
                SoundEffectManager.instance.PlaySoundEffectOneClip("Guard");

                playerCancelAttack = true;
                playerDamage = Mathf.CeilToInt(playerDamage * 0.5f);
            }
        }

        #endregion

        #region Healing Section
        int playerHeal = 0;

        if (playerHealing && playerCard != null)
            playerHeal = playerCard.cardData.itemHealAmount;

        // Healing reduced if also taking baseDamage
        if (playerHeal > 0 && enemyDamage > 0)
            playerHeal = Mathf.FloorToInt(playerHeal * 0.5f);

        #endregion

        Debug.Log("enemy is cancel attack is " + enemyCancelAttack);

        Debug.Log("player is cancel attack is " + playerCancelAttack);

        if (playerDamage > 0 && playerCard != null && !enemyCancelAttack)
        {
            Debug.Log("Enemy get damage : " + playerDamage);
            if (playerCard.cardData.offensiveCardType == OffensiveCardType.LightAttack)
            {
                AnimationManager.instance.DoEnemyAnimation(AnimationEffectType.LightBlast);
                SoundEffectManager.instance.PlaySoundEffectOneClip("LightAttack");
            }
            else if (playerCard.cardData.offensiveCardType == OffensiveCardType.HeavyAttack)
            {
                AnimationManager.instance.DoEnemyAnimation(AnimationEffectType.HeavyBlast);
                SoundEffectManager.instance.PlaySoundEffectOneClip("HeavyAttack");
            }
            else if (playerCard.cardData.cardType == CardType.Item && playerCard.cardData.itemCardType == ItemCardType.Offensive)
            {
                AnimationManager.instance.DoEnemyAnimation((AnimationEffectType.Explosion));
                SoundEffectManager.instance.PlaySoundEffectOneClip("Explosion");

            }

            DamageManager.instance.DealDamageToTarget(TargetType.Enemy, playerDamage + (int)baseDamagePlayer);
        }

        if (enemyDamage > 0 && !playerCancelAttack)
        {
            Debug.Log("Player get damage : " + enemyCard);

            if (enemyCard.cardData.offensiveCardType == OffensiveCardType.LightAttack)
            {
                AnimationManager.instance.DoPlayerAnimation(AnimationEffectType.LightBlast);
                SoundEffectManager.instance.PlaySoundEffectOneClip("LightAttack");
            }
            else if (enemyCard.cardData.offensiveCardType == OffensiveCardType.HeavyAttack)
            {
                AnimationManager.instance.DoPlayerAnimation(AnimationEffectType.HeavyBlast);
                SoundEffectManager.instance.PlaySoundEffectOneClip("HeavyAttack");

            }
            DamageManager.instance.DealDamageToTarget(TargetType.Player, enemyDamage + +(int)baseDamageEnemy);
        }

        if (playerHeal > 0 && playerCard != null)
        {
            AnimationManager.instance.DoPlayerAnimation(AnimationEffectType.Heal);
            DamageManager.instance.HealToTarget(TargetType.Player, playerHeal);
        }
    }
}
