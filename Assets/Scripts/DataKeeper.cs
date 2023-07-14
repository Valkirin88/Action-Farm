
using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using System;

public static class DataKeeper 
{
    public static int Coins;

    private static void SaveCoins(int coins)
    {
        Coins = coins;
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
            { "Coins", Coins.ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private static void OnError(PlayFabError error)
    {
        
    }

    private static void OnDataSend(UpdateUserDataResult result)
    {
        
    }
}
