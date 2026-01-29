using UnityEngine;

[System.Serializable]
public enum CardType
{
    Movement,
    Item
}

#region Movement Card Types
public enum MovementCardType
{
    Offensive,
    Defensive
}

public enum OffensiveCardType
{
    LightAttack,
    HeavyAttack
}

public enum DefensiveCardType
{
    Parry,
    Dodge,
    Guard   //reduce damage taken %50 dmg
}
#endregion

#region ITEM CARD TYPES
public enum ItemCardType
{
    HealthPotion,
    Offensive
}

#endregion

[CreateAssetMenu(fileName = "CardSO", menuName = "Cards/CardDataSO")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public CardType cardType;

    #region MovementCardType
    public MovementCardType movementCardType;

    //Movement Card Offensive Properties
    public OffensiveCardType offensiveCardType;
    public float damage;

    //Movement Card Defensive Properties
    public DefensiveCardType defensiveCardType;

    #endregion

    #region ItemCardType
    public ItemCardType itemCardType;
    public bool useItem;

    //Item Card Healing Properties
    public float itemHealAmount;

    //item Card Offensive Properties
    public float itemDamage;
    #endregion

    public float StaminaCost;
}
