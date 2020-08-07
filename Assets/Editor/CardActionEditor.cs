using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// CardAction编辑器类
/// </summary>
[CustomEditor(typeof(CardAction))]
public class CardActionEditor : Editor
{
    SerializedProperty actionType, actionClass,filterClass, param;

    private void OnEnable()
    {
        actionType = serializedObject.FindProperty("actionType");
        actionClass = serializedObject.FindProperty("actionClass");
        filterClass = serializedObject.FindProperty("filterClass");
        param = serializedObject.FindProperty("param");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(actionType);
        switch (actionType.enumValueIndex)
        {
            //分组器
            case (int)CardAction.ActionType.GROUP:
                break;
            //过滤器（待编辑）
            case (int)CardAction.ActionType.FILTER:
                EditorGUILayout.PropertyField(filterClass);
                EditorGUILayout.PropertyField(param);
                break;
            //动作
            case (int)CardAction.ActionType.ACTION:
                EditorGUILayout.PropertyField(actionClass);
                EditorGUILayout.PropertyField(param);
                break;
        }
        if(actionType.enumValueIndex == (int)CardAction.ActionType.ACTION)
        {
            switch (actionClass.enumValueIndex)
            {
                case (int)CardAction.ActionClass.HARM_SINGLE:
                    EditorGUILayout.TextArea("P0:HP伤害值\nP1:SP伤害值\nP2:MP伤害值");
                    param.arraySize = 3;
                    break;
            }
        }
        else if(actionType.enumValueIndex == (int)CardAction.ActionType.FILTER)
            switch (filterClass.enumValueIndex)
            {
                case (int)CardAction.FilterClass.NONE:
                    param.arraySize = 0;
                    break;
            }
        serializedObject.ApplyModifiedProperties();
    }
}
