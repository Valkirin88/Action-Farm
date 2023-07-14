using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;

using UnityEngine;

public static class DataKeeper 
{
    public static int Coins;

    public static void SaveCoins(int coins)
    {
        Debug.Log(Coins.ToString());
        Coins = Coins + coins;
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
