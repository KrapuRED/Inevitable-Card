using UnityEngine;

[System.Serializable]
public enum CardType
{
    Movement,
    item
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



[CreateAssetMenu(fileName = "CardSO", menuName = "Cards/CardDataSO")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public Sprite cardImage;
    public CardType cardType;

    public MovementCardType movementCardType;

    //Movement Card Offensive Properties
    public OffensiveCardType offensiveCardType;
    public float damage;

    //Movement Card Defensive Properties
    public DefensiveCardType defensiveCardType;

    public float StaminaCost;
}
