using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _wheatWindow;
    [SerializeField]
    private GameObject _coinsWindow;
    [SerializeField]
    private GameObject _joystick;

    [SerializeField]
    private TextMeshProUGUI _coinsCounterText;
    [SerializeField]
    private TextMeshProUGUI _wheatCounterText;

    [SerializeField]
    private int _coins;
    [SerializeField]
    private int _wheat;

    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private CoinsHandler _coinsHandler;
    [SerializeField]
    private float _countdownTime = 0.01f;
    
    [SerializeField]
    private GameObject _winScreen;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _mainMenuButton;

    private int _coinsOnCounter;
    private bool _isInitiated;
    private bool _isWin;

    private void Initiate()
    {
        _playerView.OnWheatCountChanged += AddWheat;
        _playerView.OnNearBarn += WheatCountdown;
        _coinsHandler.OnCoinGet += CoinsCountStart;
        ShowCoins(_coins);
        ShowWheat(_wheat);
        _isInitiated = true;
    }
    private void Update()
    {
        if (_playerView == null)
            _playerView = FindObjectOfType<PlayerView>();
        else if (_playerView != null && !_isInitiated)
            Initiate();
        if (_coinsOnCounter > 60 && !_isWin)
            ShowWin();
       
    }

    private void ShowWin()
    {
        _isWin = true;
        _winScreen.SetActive(true);
        _joystick.SetActive(false);
        _restartButton.onClick.AddListener(Restart);
        _mainMenuButton.onClick.AddListener(ShowMainMenu);

        
    }

    private void ShowMainMenu()
    {
        DataKeeper.SaveCoins(_coinsOnCounter);
        Debug.Log("Save");
        SceneManager.LoadScene(0);
    }

    private void Restart()
    {

        SceneManager.LoadScene(1);
    }

    private void ShowCoins(int coins)
    {
        _coinsCounterText.text =  $"{coins}";
    }

    private void ShowWheat(int wheat)
    {
        _wheatCounterText.text = wheat + "/40";
    }


    private void AddWheat(int wheat)
    {
        _wheat = wheat;
        ShowWheat(_wheat);
    }

    private void WheatCountdown()
    {
        if (_wheat > 0)
        {
            _wheatWindow.transform.DOShakePosition(0.1f, 10);
            StartCoroutine(WaitForNextWheatCount());
        }
    }

    private IEnumerator WaitForNextWheatCount()
    {
        yield return new WaitForSeconds(_countdownTime);
        _wheat--;
        ShowWheat(_wheat);
        WheatCountdown();
    }

    private void CoinsCountStart(int coinsCount)
    {

        _coins = _coins + coinsCount;
        CoinsCountUp();
    }

    private void CoinsCountUp()
    {
        if (_coinsOnCounter < _coins)
        {
            _coinsWindow.transform.DOShakePosition(0.1f, 10);
            StartCoroutine(WaitForNextCoinCount());
        }
    }

    private IEnumerator WaitForNextCoinCount()
    {
        yield return new WaitForSeconds(0.1f);
        _coinsOnCounter += 15;
        if (_coinsOnCounter > _coins)
            _coinsOnCounter = _coins;
        ShowCoins(_coinsOnCounter);
        CoinsCountUp();
    }

    private void OnDestroy()
    {
        _playerView.OnWheatCountChanged -= AddWheat;
        _playerView.OnNearBarn -= WheatCountdown;
        _coinsHandler.OnCoinGet -= CoinsCountStart;
        _restartButton.onClick.RemoveListener(Restart);
        _mainMenuButton.onClick.RemoveListener(ShowMainMenu);
    }
}
