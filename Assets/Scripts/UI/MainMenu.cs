using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public int Coins;

    [SerializeField]
    private Button _startGameButton;
    [SerializeField]
    private Button _openLoginScreenButton;
    [SerializeField]
    private Button _loginButton;
    [SerializeField]
    private Button _registerButton;
    [SerializeField]
    private Button _backButton;

    [SerializeField]
    private TMP_Text _messageText;
    [SerializeField]
    private TMP_Text _coinsText;

    [SerializeField]
    private TMP_InputField _emailInput;
    [SerializeField]
    private TMP_InputField _passwordInput;

    [SerializeField]
    private GameObject _mainScreen;
    [SerializeField]
    private GameObject _loginScreen;

    private void Start()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _loginButton.onClick.AddListener(Login);
        _registerButton.onClick.AddListener(Register);
        _openLoginScreenButton.onClick.AddListener(OpenLoginScreen);
        _backButton.onClick.AddListener(OpenMainScreen);
    }

    private void OpenLoginScreen()
    {
        _loginScreen.SetActive(true);
        _mainScreen.SetActive(false);
    }

    private void OpenMainScreen()
    {
        _loginScreen.SetActive(false);
        _mainScreen.SetActive(true);
        UpdateCoins();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Register()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = _emailInput.text,
            Password = _passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    private void OnError(PlayFabError error)
    {
        _messageText.text = error.ErrorMessage;
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        _messageText.text = "Registered and logged in";
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = _emailInput.text,
            Password = _passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult message)
    {
        _messageText.text = "Login success";
        UpdateCoins();
    }

    private void UpdateCoins()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    private void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Coins"))
        {
            _coinsText.text = $"Coins:{result.Data["Coins"].Value}";
        }
        else
            _coinsText.text = "Coins: 0";
    }

    private void SaveCoins()
    {
        
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
            { "Coins", Coins.ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    private void OnDataSend(UpdateUserDataResult result)
    {
        
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveListener(StartGame);
        _loginButton.onClick.RemoveListener(Login);
        _registerButton.onClick.RemoveListener(Register);
        _openLoginScreenButton.onClick.RemoveListener(OpenLoginScreen);
        _backButton.onClick.RemoveListener(OpenMainScreen);
    }
}
