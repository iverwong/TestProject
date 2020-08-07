using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// CardTargetTrigger编辑器类
/// </summary>
[CustomEditor(typeof(CardTargetTrigger))]
public class CardTargetTriggerEditor : Editor
{
    SerializedProperty distance, angle,triggerType;
    private void OnEnable()
    {
        triggerType = serializedObject.FindProperty("triggerType");
        distance = serializedObject.FindProperty("distance");
        angle = serializedObject.FindProperty("angle");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(triggerType);
        switch (triggerType.enumValueIndex)
        {
            //无触发
            case (int)CardTargetTrigger.TriggerType.NONE:
                break;
            //圆形范围内的点目标
            case (int)CardTargetTrigger.TriggerType.ROUND_POINT:
                EditorGUILayout.PropertyField(distance);
                break;

        }
        serializedObject.ApplyModifiedProperties();
    }
}
