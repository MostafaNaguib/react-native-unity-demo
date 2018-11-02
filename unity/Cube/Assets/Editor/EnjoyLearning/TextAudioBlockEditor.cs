using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextAudioBlock))]
public class TextAudioBlockEditor : Editor
{
	private TextAudioBlock textAudioBlock;

	private void OnEnable()
	{
		textAudioBlock = (TextAudioBlock) target;
	}

	public override void OnInspectorGUI()
	{
		EditorUtility.SetDirty(textAudioBlock);

		// begin box
		EditorGUILayout.BeginVertical("box");
		GUILayout.Space(5);
        
        GUILayout.Label("Text", GUILayout.MaxWidth(32));
        textAudioBlock.text = EditorGUILayout.TextArea(textAudioBlock.text, GUILayout.MinHeight(50), GUILayout.MinWidth(200));

        GUILayout.Space(5);

        GUILayout.Label("Audio", GUILayout.MaxWidth(32));
        textAudioBlock.audio = (AudioClip)EditorGUILayout.ObjectField(textAudioBlock.audio, typeof(AudioClip), false);

		GUILayout.Space(5);
		EditorGUILayout.EndVertical();
	}
}
