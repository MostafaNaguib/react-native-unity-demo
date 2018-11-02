using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextSpriteAudioBlock))]
public class TextSpriteAudioBlockEditor : Editor
{
    private TextSpriteAudioBlock textSpriteAudioBlock;

    private void OnEnable()
    {
        textSpriteAudioBlock = (TextSpriteAudioBlock) target;
    }

    public override void OnInspectorGUI()
    {
        EditorUtility.SetDirty(textSpriteAudioBlock);

        // begin box
        EditorGUILayout.BeginVertical("box");
        GUILayout.Space(5);

        textSpriteAudioBlock.text = EditorGUILayout.TextField(textSpriteAudioBlock.text);

        GUILayout.Space(5);

        textSpriteAudioBlock.sprite = (Sprite)EditorGUILayout.ObjectField(textSpriteAudioBlock.sprite, typeof(Sprite), false);

        GUILayout.Space(5);

        textSpriteAudioBlock.audio = (AudioClip)EditorGUILayout.ObjectField(textSpriteAudioBlock.audio, typeof(AudioClip), false);

        GUILayout.Space(5);
        EditorGUILayout.EndVertical();
    }
}
