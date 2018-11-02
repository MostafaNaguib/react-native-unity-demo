using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnjoyLearning.VR.SDK;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CardboardButtonTarget))]
public class CardboardButtonTargetActivator : MonoBehaviour
{
    private CardboardButtonTarget mCardboardButtonTarget;
    private Collider mCollider;

    private void Awake()
    {
        mCollider = GetComponent<Collider>();
        mCardboardButtonTarget = GetComponent<CardboardButtonTarget>();
    }

    private void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        mCollider.enabled = true;
        mCardboardButtonTarget.enabled = true;
    }

    public void Deactivate()
    {
        mCollider.enabled = false;
        mCardboardButtonTarget.enabled = false;
    }
}
