using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnjoyLearning.VR.SDK
{
    public class OrientationManager : MonoBehaviour
    {
        public ScreenOrientation screenOrientation;

        void Awake()
        {
            Screen.orientation = screenOrientation;
        }
    }
}

