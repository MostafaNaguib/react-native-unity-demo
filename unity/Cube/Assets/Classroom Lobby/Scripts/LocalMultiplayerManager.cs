using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMultiplayerManager : MonoBehaviour
{
    public bool canHost;
    public GameObject HostUI;
    public GameObject GuestUI;
    
	void OnEnable ()
    {
        canHost = PlayerPrefs.GetInt(PlayerPrefsKeys.UserCanHost, 0) == 1 ? true : false;
        UpdateUI();
	}

    public void UpdateUI()
    {
        HostUI.SetActive(canHost);
        GuestUI.SetActive(!canHost);
    }
}
