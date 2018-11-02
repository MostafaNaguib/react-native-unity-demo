using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ArabicSupportUI;
using System.IO;
using UnityEngine.SceneManagement;
using EnjoyLearning.VR.SDK;
public class SimulationController : MonoBehaviour {

  

    void Awake() {
		Resources.UnloadUnusedAssets ();
		System.GC.Collect ();

		AssetBundle arc = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, PlayerPrefs.GetString("ELVREgyptBundleName")));
		if (PlayerPrefs.GetString("ELVREgyptMode") == "normal")
            {
			StartCoroutine(SceneLoader.LoadSceneWithDevice(PlayerPrefs.GetString("ELVREgyptSceneName"), false));
            }
            else
            {
			StartCoroutine(SceneLoader.LoadSceneWithDevice(PlayerPrefs.GetString("ELVREgyptSceneName"), true));

            }
        }
    



}

