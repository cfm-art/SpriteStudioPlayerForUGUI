using System;
using UnityEditor;
using UnityEngine;

namespace a.spritestudio.editor.inspector
{
    public class InspectorUtility
    {
    }

    public class Vertical
        : IDisposable
    {
        public Vertical()
        {
            EditorGUILayout.BeginVertical();
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }

    public class Horizontal
        : IDisposable
    {
        public Horizontal()
        {
            EditorGUILayout.BeginHorizontal();
        }

        public void Dispose()
        {
            EditorGUILayout.EndHorizontal();
        }
    }
}
