﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VideoPlayerController))]
public class VideoPlayerControllerEditor : Editor
{
    private VideoPlayerController videoPlayerController;

    private void OnEnable()
    {
        videoPlayerController = (VideoPlayerController) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorUtility.SetDirty(videoPlayerController);

        GUILayout.BeginVertical("box");
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);

        if(videoPlayerController.VideoPlayer != null)
        {
            if (!videoPlayerController.VideoPlayer.isPlaying && GUILayout.Button("Play"))
            {
                videoPlayerController.Play();
            }

            if (videoPlayerController.VideoPlayer.isPlaying && GUILayout.Button("Replay"))
            {
                videoPlayerController.Play();
            }
        }

        GUILayout.Space(10);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.EndVertical();
    }
}