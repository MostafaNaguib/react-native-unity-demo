using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Prototype.NetworkLobby;
using EnjoyLearning.VR.SDK;

public class ClassroomMemberManager : NetworkBehaviour
{
    public float returnTolobbyWait = 2.5f;
    public bool startPaused = true;
    public bool startMuted = false;
    
    [SerializeField]
    private PlayPauseManager playPauseManager;

    [SerializeField]
    private MuteUnmuteManager muteUnmuteManager;

    private Coroutine coroutineInitializingPlayPauseManager;
    private Coroutine coroutineInitializingMuteUnmuteManager;
    
    void Update()
    {
        if (isServer && Input.GetKeyUp(KeyCode.Escape))
        {
            //Debug.LogFormat("ServerReturnToLobby by ClassroomMemberManager Escape");
            ReturnToLoby();
        }

        if (playPauseManager == null && coroutineInitializingPlayPauseManager == null)
        {
            coroutineInitializingPlayPauseManager = StartCoroutine(InitializingPlayPauseManager());
        }

        if (muteUnmuteManager == null && coroutineInitializingMuteUnmuteManager == null)
        {
            coroutineInitializingMuteUnmuteManager = StartCoroutine(InitializingMuteUnmuteManager());
        }

        //Debug.LogFormat("IP: {0} | Local Player: {1} | PlayPauseManager: {2} | MuteUnmuteManager: {3}", Network.player.ipAddress, isLocalPlayer, playPauseManager != null, muteUnmuteManager != null);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    [ClientRpc]
    void RpcSwitchPlayPause()
    {
        SwitchPlayPause();
    }

    [ClientRpc]
    void RpcSwitchMuteUnmute()
    {
        SwitchMuteUnmute();
    }

    void PlayPauseButtonClicked()
    {
        RpcSwitchPlayPause();
    }

    void MuteUnmuteButtonClicked()
    {
        RpcSwitchMuteUnmute();
    }

    void SwitchPlayPause()
    {
        //Debug.LogFormat("SwitchPlayPause call by RPC: {0}", this.GetInstanceID());
        if(playPauseManager != null)
            playPauseManager.Switch();
    }

    void SwitchMuteUnmute()
    {
        //Debug.LogFormat("SwitchMuteUnmute call by RPC: {0}", this.GetInstanceID());
        if (muteUnmuteManager != null)
            muteUnmuteManager.Switch();
    }

    IEnumerator InitializingPlayPauseManager()
    {
        GameObject playPauseGO = GameObject.FindGameObjectWithTag("Play Pause Manager");
        while (playPauseGO == null)
        {
            //Debug.LogFormat("Trying to Find playPauseGO: {0}", Network.player.ipAddress);
            yield return new WaitForEndOfFrame();
            playPauseGO = GameObject.FindGameObjectWithTag("Play Pause Manager");
        }

        playPauseManager = playPauseGO.GetComponent<PlayPauseManager>();
        while (playPauseManager == null)
        {
            //Debug.LogFormat("Trying to Find playPauseManager: {0}", Network.player.ipAddress);
            yield return new WaitForEndOfFrame();
            playPauseManager = playPauseGO.GetComponent<PlayPauseManager>();
        }

        playPauseManager.ShowPlayPauseButton(isServer);
        if (isServer && isLocalPlayer)
            playPauseManager.playPauseButton.onClick.AddListener(PlayPauseButtonClicked);
        playPauseManager.UpdateGUI(startPaused);

        coroutineInitializingPlayPauseManager = null;
    }

    IEnumerator InitializingMuteUnmuteManager()
    {
        GameObject muteUnmuteGO = GameObject.FindGameObjectWithTag("Mute Unmute Manager");
        while (muteUnmuteGO == null)
        {
            //Debug.LogFormat("Trying to Find muteUnmuteGO: {0}", Network.player.ipAddress);
            yield return new WaitForEndOfFrame();
            muteUnmuteGO = GameObject.FindGameObjectWithTag("Mute Unmute Manager");
        }

        muteUnmuteManager = muteUnmuteGO.GetComponent<MuteUnmuteManager>();
        while (muteUnmuteManager == null)
        {
            //Debug.LogFormat("Trying to Find muteUnmuteManager: {0}", Network.player.ipAddress);
            yield return new WaitForEndOfFrame();
            muteUnmuteManager = muteUnmuteGO.GetComponent<MuteUnmuteManager>();
        }

        muteUnmuteManager.ShowMuteUnmuteButton(isServer);
        if (isServer && isLocalPlayer)
            muteUnmuteManager.muteUnmuteButton.onClick.AddListener(MuteUnmuteButtonClicked);
        muteUnmuteManager.UpdateGUI(startMuted);

        coroutineInitializingMuteUnmuteManager = null;
    }

    void ReturnToLoby()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        LobbyManager.s_Singleton.ServerReturnToLobby();
    }
}
