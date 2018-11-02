using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ServerClientActions : NetworkBehaviour
{
	public UnityEvent serverEvents;
	public UnityEvent clientEvents;

	void Start ()
	{
		//Debug.LogFormat ("ServerClientActions -> isLocalPlayer: {0} | isServer: {1}", isLocalPlayer, isServer);
		if (isServer)
		{
			if(serverEvents != null)
			{
				serverEvents.Invoke();
			}
		}
		else
		{
			if (clientEvents != null)
			{
				clientEvents.Invoke();
			}
		}
	}
}
