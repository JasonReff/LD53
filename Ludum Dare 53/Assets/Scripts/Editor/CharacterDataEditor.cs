using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(CharacterData))]
public class CharacterDataEditor : Editor
{
    private CharacterData _script;
    private void OnEnable()
    {
        _script = (CharacterData)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Get Voice Lines"))
        {
            _script.GetVoiceLines();
        }
    }
}