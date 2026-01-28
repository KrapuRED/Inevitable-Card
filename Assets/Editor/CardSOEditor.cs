using UnityEditor;
using UnityEngine;

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

    SerializedProperty staminaCost;

    private void OnEnable()
    {
        cardName            = serializedObject.FindProperty("cardName");
        cardImage           = serializedObject.FindProperty("cardImage");
        cardType            = serializedObject.FindProperty("cardType");
        movementCardType    = serializedObject.FindProperty("movementCardType");
        offensiveCardType   = serializedObject.FindProperty("offensiveCardType");
        damage              = serializedObject.FindProperty("damage");
        defensiveCardType   = serializedObject.FindProperty("defensiveCardType");
        staminaCost         = serializedObject.FindProperty("StaminaCost");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(cardName);
        EditorGUILayout.PropertyField(cardImage);
        EditorGUILayout.PropertyField(cardType);
        if (cardType.enumValueIndex == (int)CardType.Movement)
        {
            EditorGUILayout.PropertyField(movementCardType);
            if (movementCardType.enumValueIndex == (int)MovementCardType.Offensive)
            {
                EditorGUILayout.PropertyField(offensiveCardType);
                EditorGUILayout.PropertyField(damage);
            }
            else if (movementCardType.enumValueIndex == (int)MovementCardType.Defensive)
            {
                EditorGUILayout.PropertyField(defensiveCardType);
            }
        }
        EditorGUILayout.PropertyField(staminaCost);
        serializedObject.ApplyModifiedProperties();
    }
}
