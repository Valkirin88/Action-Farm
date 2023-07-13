using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public class PlayerFabManager : MonoBehaviour
{
    private void Start()
    {
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSucces, OnError);
    }

    private void OnError(PlayFabError obj)
    {
        
    }

    private void OnSucces(LoginResult obj)
    {
        Debug.Log("playfab succes connection");
    }
}
