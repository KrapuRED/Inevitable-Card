using UnityEngine;

[System.Serializable]
public enum CardType
{
    Movement,
    Item,
    Special
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
    None,
    Parry,
    Dodge,
    Guard   //reduce damageMovement taken %50 dmg
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
    public int damageMovement;

    //Movement Card Defensive Properties
    public DefensiveCardType defensiveCardType;

    #endregion

    #region ItemCardType
    public ItemCardType itemCardType;
    public bool useItem;

    //Item Card Healing Properties
    public int itemHealAmount;

    //item Card Offensive Properties
    public int itemDamage;
    #endregion

    public int staminaCost;
}
