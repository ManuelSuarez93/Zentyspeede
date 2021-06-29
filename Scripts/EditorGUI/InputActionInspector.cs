using UnityEditor;
using UnityEngine;
using ZentySpeede.Player;


namespace ZentySpeede.GUI
{
#if (UNITY_EDITOR)
    [CustomEditor(typeof(InputAction)),CanEditMultipleObjects]
    public class InputActionInspector: Editor
    {
        private SerializedObject inspectorObject;
        private SerializedProperty guiSuccessEvent;
        private SerializedProperty guiGreatSuccessEvent;
        private SerializedProperty guiFailEvent;
        private SerializedProperty guiOnChangeEvent;
        private SerializedProperty guiObstacleDetector;
        private SerializedProperty guiMesh;
        private SerializedProperty guiAnimController;


        protected static bool showEvents = false;
        protected static bool showComponents = false;
        private void OnEnable()
        {
            inspectorObject = new SerializedObject(target);
            guiSuccessEvent = inspectorObject.FindProperty("successEvent");
            guiGreatSuccessEvent = inspectorObject.FindProperty("greatSuccessEvent");
            guiFailEvent = inspectorObject.FindProperty("failEvent");
            guiOnChangeEvent = inspectorObject.FindProperty("onChangeEvent");
            guiObstacleDetector = inspectorObject.FindProperty("obstacleDetector");
            guiMesh = inspectorObject.FindProperty("mesh");
            guiAnimController = inspectorObject.FindProperty("animController");
        }

        public override void OnInspectorGUI()
        {
            inspectorObject.Update();

            showComponents = EditorGUILayout.Foldout(showComponents, "Components");
            if(showComponents)
            {
                EditorGUILayout.PropertyField(guiObstacleDetector);
                EditorGUILayout.PropertyField(guiMesh);
                EditorGUILayout.PropertyField(guiAnimController);
            }

            showEvents = EditorGUILayout.Foldout(showEvents, "Events");
            if (showEvents)
            {
                EditorGUILayout.PropertyField(guiSuccessEvent);
                EditorGUILayout.PropertyField(guiGreatSuccessEvent);
                EditorGUILayout.PropertyField(guiFailEvent);
                EditorGUILayout.PropertyField(guiOnChangeEvent);
            }

            inspectorObject.ApplyModifiedProperties();
        }
    }
#endif
}