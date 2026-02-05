using UnityEditor;

[CustomEditor(typeof(CardSO))]
public class CardSOEditor : Editor
{
    SerializedProperty cardName;
    SerializedProperty cardImage;
    SerializedProperty cardType;
    SerializedProperty movementCardType;

    SerializedProperty offensiveCardType;
    SerializedProperty damage;

    SerializedProperty defensiveCardType;

    SerializedProperty itemCardType;
    SerializedProperty useItem;
    SerializedProperty itemHealAmount;
    SerializedProperty itemDamage;

    SerializedProperty staminaCost;

    private void OnEnable()
    {
        cardName            = serializedObject.FindProperty("cardName");
        cardImage           = serializedObject.FindProperty("cardImage");
        cardType            = serializedObject.FindProperty("cardType");
        movementCardType    = serializedObject.FindProperty("movementCardType");
        offensiveCardType   = serializedObject.FindProperty("offensiveCardType");
        damage              = serializedObject.FindProperty("damageMovement");
        defensiveCardType   = serializedObject.FindProperty("defensiveCardType");
        itemCardType        = serializedObject.FindProperty("itemCardType");
        useItem             = serializedObject.FindProperty("useItem");
        itemHealAmount      = serializedObject.FindProperty("itemHealAmount");
        itemDamage          = serializedObject.FindProperty("itemDamage");
        staminaCost         = serializedObject.FindProperty("staminaCost");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(cardName);
        EditorGUILayout.PropertyField(cardImage);
        EditorGUILayout.PropertyField(cardType);

        if ((CardType)cardType.enumValueIndex == CardType.Movement)
        {
            EditorGUILayout.PropertyField(movementCardType);

            if ((MovementCardType)movementCardType.enumValueIndex == MovementCardType.Offensive)
            {
                EditorGUILayout.PropertyField(offensiveCardType);
                EditorGUILayout.PropertyField(damage);
            }
            else if ((MovementCardType)movementCardType.enumValueIndex == MovementCardType.Defensive)
            {
                EditorGUILayout.PropertyField(defensiveCardType);
            }
        }

        if ((CardType)cardType.enumValueIndex == CardType.Item)
        {
            EditorGUILayout.PropertyField(itemCardType);
            EditorGUILayout.PropertyField(useItem);

            if ((ItemCardType)itemCardType.enumValueIndex == ItemCardType.HealthPotion)
            {
                EditorGUILayout.PropertyField(itemHealAmount);
            }
            else if ((ItemCardType)itemCardType.enumValueIndex == ItemCardType.Offensive)
            {
                EditorGUILayout.PropertyField(itemDamage);
            }
        }

        EditorGUILayout.PropertyField(staminaCost);

        serializedObject.ApplyModifiedProperties();
    }
}
