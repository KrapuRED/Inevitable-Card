using UnityEditor;

[CustomEditor(typeof(EnemySO))]
public class EnemySOEditor : Editor
{
    SerializedProperty enemyName;
    SerializedProperty enemyType;
    SerializedProperty enemyStatus;
    SerializedProperty baseDamage;
    SerializedProperty maxHealth;
    SerializedProperty hiddenCard;
    SerializedProperty isElimination;

    private void OnEnable()
    {
        enemyName = serializedObject.FindProperty("enemyName");
        enemyType = serializedObject.FindProperty("enemyType");
        enemyStatus = serializedObject.FindProperty("enemyStatus");
        baseDamage = serializedObject.FindProperty("baseDamage");
        maxHealth = serializedObject.FindProperty("maxHealth");
        hiddenCard = serializedObject.FindProperty("hiddenCard");
        isElimination = serializedObject.FindProperty("isElimination");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(enemyName);
        EditorGUILayout.PropertyField(enemyType);

        EnemyType type = (EnemyType)enemyType.enumValueIndex;
        if (type == EnemyType.Goon)
        {
            EditorGUILayout.PropertyField(enemyStatus);
        }

        EditorGUILayout.PropertyField(baseDamage);
        EditorGUILayout.PropertyField(maxHealth);
        EditorGUILayout.PropertyField(hiddenCard);
        EditorGUILayout.PropertyField(isElimination);

        serializedObject.ApplyModifiedProperties();
    }
}
