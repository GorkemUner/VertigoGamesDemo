#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(RandomStrategySelectorAttribute))]
public class RandomStrategySelectorDrawer : PropertyDrawer
{
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
