using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnjoyLearning.VR.SDK;

[RequireComponent(typeof(CardboardButtonTarget))]
public class CardboardButtonTargetListener : MonoBehaviour
{
    public bool activeOnUnPause;

    private CardboardButtonTarget cardboardButtonTarget;
    private bool paused;

    void Awake()
    {
        cardboardButtonTarget = GetComponent<CardboardButtonTarget>();
    }

    private void OnEnable()
    {
        PlayPauseManager.OnPause += OnPause;
    }

    private void OnDisable()
    {
        PlayPauseManager.OnPause -= OnPause;
    }

    private void OnPause(bool pause)
    {
        if (pause && cardboardButtonTarget.enabled)
        {
            cardboardButtonTarget.enabled = false;
            paused = true;
        }

        if (!pause && (paused || activeOnUnPause))
        {
            cardboardButtonTarget.enabled = true;
            paused = false;
            activeOnUnPause = false;
        }
    }
}
