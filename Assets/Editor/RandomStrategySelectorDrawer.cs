#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[CustomPropertyDrawer(typeof(RandomStrategySelectorAttribute))]
public class RandomStrategySelectorDrawer : PropertyDrawer
{
    //private Type[] _strategyTypes;
    //private string[] _strategyNames;

    //private void LoadStrategies()
    //{
    //    if (_strategyTypes != null) return;

    //    var baseType = typeof(BaseRandomStrategy);

    //    _strategyTypes = AppDomain.CurrentDomain.GetAssemblies()
    //        .SelectMany(a =>
    //        {
    //            try { return a.GetTypes(); }
    //            catch { return Array.Empty<Type>(); }
    //        })
    //        .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t))
    //        .ToArray();

    //    _strategyNames = _strategyTypes.Select(t => t.Name).ToArray();
    //}

    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    LoadStrategies();

    //    EditorGUI.BeginProperty(position, label, property);

    //    int currentIndex = 0;

    //    if (!string.IsNullOrEmpty(property.stringValue))
    //    {
    //        var foundIndex = Array.FindIndex(_strategyTypes,
    //            t => t.AssemblyQualifiedName == property.stringValue);

    //        if (foundIndex >= 0) currentIndex = foundIndex;
    //    }

    //    int newIndex = EditorGUI.Popup(position, label.text, currentIndex, _strategyNames);

    //    if (newIndex != currentIndex)
    //    {
    //        property.stringValue = _strategyTypes[newIndex].AssemblyQualifiedName;
    //    }

    //    EditorGUI.EndProperty();
    //}

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var baseType = typeof(BaseRandomStrategy);

        var allTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t))
            .ToList();

        var names = allTypes.Select(t => t.FullName).ToList();

        int index = Mathf.Max(0, names.IndexOf(property.stringValue));
        int newIndex = EditorGUI.Popup(position, label.text, index, names.ToArray());

        if (newIndex != index)
        {
            property.stringValue = names[newIndex];
        }
    }

}
#endif
