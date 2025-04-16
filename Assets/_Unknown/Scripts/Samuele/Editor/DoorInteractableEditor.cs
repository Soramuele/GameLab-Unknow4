using UnityEditor;

namespace Unknown.Samuele
{
    [CustomEditor(typeof(DoorInteractable))]
    public class DoorInteractableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty needsKeyProp = serializedObject.FindProperty("needsKey");
            SerializedProperty keyProp = serializedObject.FindProperty("key");
            SerializedProperty alwaysNeedsKeyProp = serializedObject.FindProperty("alwaysNeedsKey");

            SerializedProperty iterator = serializedObject.GetIterator();
            bool enterChildren = true;

            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;

                if (iterator.name == "m_Script")
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(iterator, true);
                    EditorGUI.EndDisabledGroup();
                    continue;
                }

                // Skip these — we'll draw them manually after needsKey
                if (iterator.name == "key" || iterator.name == "alwaysNeedsKey")
                    continue;

                EditorGUILayout.PropertyField(iterator, true);

                // After drawing needsKey, conditionally draw key & alwaysNeedsKey
                if (iterator.name == "needsKey" && iterator.boolValue)
                {
                    EditorGUILayout.PropertyField(keyProp, true);
                    EditorGUILayout.PropertyField(alwaysNeedsKeyProp, true);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
