using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

public class LobbyManagerActivator : MonoBehaviour
{
    public bool active;

    void OnEnable()
    { 
		bool virtualClassroom = PlayerPrefs.GetInt(PlayerPrefsKeys.VirtualClassroom, 0) == 1 ? true : false;

        if(LobbyManager.s_Singleton != null)
        {
			//Debug.LogFormat("LobbyManagerActivator -> {0}", active && virtualClassroom);
			LobbyManager.s_Singleton._GUI.SetActive(active && virtualClassroom);
        }
            
    }
}
