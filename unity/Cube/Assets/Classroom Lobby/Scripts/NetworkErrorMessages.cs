using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public static class NetworkErrorMessages
{
    public static string MessageForError(int errorCode)
    {
        switch(errorCode)
        {
            case 0:
                return "0";
            case 1:
                return "1";
            case 2:
                return "2";
            case 3:
                return "3";
            case 4:
                return "4";
            case 5:
                return "5";
            case 6:
                return "Connection Timeout";
            case 7:
                return "7";
            case 8:
                return "8";
            case 9:
                return "9";
            case 10:
                return "10";
            case 11:
                return "11";
            default:
                return "12";
        }
    }
}
