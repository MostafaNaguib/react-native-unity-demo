using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClassroomNormalSceneActions : MonoBehaviour
{
    public bool callEventOnEnable;
    public UnityEvent normalSceneEvents;
    public UnityEvent classroomSceneEvents;

    
    void OnEnable ()
    {
        bool virtualClassroom = PlayerPrefs.GetInt(PlayerPrefsKeys.VirtualClassroom, 0) == 1 ? true : false;

        if (virtualClassroom)
        {
            if(classroomSceneEvents != null)
            {
                classroomSceneEvents.Invoke();
            }
        }
        else
        {
            if (normalSceneEvents != null)
            {
                normalSceneEvents.Invoke();
            }
        }
	}
}
