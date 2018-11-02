using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class LobbyMainMenu : MonoBehaviour 
    {
        public LobbyManager lobbyManager;

        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public InputField networkHostAddressInput;
        public InputField networkHostPortInput;

        public InputField networkGuestAddressInput;
        public InputField networkGuestPortInput;

        public LobbyInfoPanel infoPanel;

        private int portNumber;
        
        public void OnEnable()
        {
            //lobbyManager.topPanel.ToggleVisibility(true);
            lobbyManager.UpdateNetworkAddress();
            networkHostAddressInput.text = lobbyManager.networkAddress;
            networkHostPortInput.text = lobbyManager.networkPort.ToString();

            networkGuestAddressInput.text = PlayerPrefs.GetString(PlayerPrefsKeys.LastUsedHostAddress);
            networkGuestPortInput.text = PlayerPrefs.GetString(PlayerPrefsKeys.LastUsedHostPort);
        }

        public void OnClickHost()
        {
            if (!ValidPortNumber(networkHostPortInput.text, out portNumber))
            {
                infoPanel.Display("Invalid Port Number", "Close", null);
                return;
            }

            lobbyManager.UpdateNetworkAddress();
            
            lobbyManager.UpdateNetworkPort(portNumber);
            lobbyManager.playScene = PlayerPrefs.GetString(PlayerPrefsKeys.ClassRoomScene);
            lobbyManager.StartHost();
        }

        public void OnClickJoin()
        {
            if (!ValidIpAddress(networkGuestAddressInput.text))
            {
                infoPanel.Display("Invalid IP Address", "Close", null);
                return;
            }

            if (!ValidPortNumber(networkGuestPortInput.text, out portNumber))
            {
                infoPanel.Display("Invalid Port Number", "Close", null);
                return;
            }

            PlayerPrefs.SetString(PlayerPrefsKeys.LastUsedHostAddress, networkGuestAddressInput.text);
            PlayerPrefs.SetString(PlayerPrefsKeys.LastUsedHostPort, networkGuestPortInput.text);
            PlayerPrefs.Save();

            lobbyManager.networkAddress = networkGuestAddressInput.text;
            
            lobbyManager.UpdateNetworkPort(portNumber);

            lobbyManager.ChangeTo(lobbyPanel);
            lobbyManager.playScene = PlayerPrefs.GetString(PlayerPrefsKeys.ClassRoomScene);
            lobbyManager.StartClient();

            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress, lobbyManager.networkPort.ToString());
        }
        
        private bool ValidIpAddress(string ip)
        {
            return Regex.IsMatch(ip, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
        }

        private bool ValidPortNumber(string port, out int portNumber)
        {
            return int.TryParse(port, out portNumber);
        }
    }
}
