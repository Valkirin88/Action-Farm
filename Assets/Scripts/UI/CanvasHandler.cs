using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _wheatWindow;
    [SerializeField]
    private GameObject _coinsWindow;

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

    private int _coinsOnCounter;

    private void Start()
    {
        _playerView.OnWheatCountChanged += AddWheat;
        _playerView.OnNearBarn += WheatCountdown;
        _coinsHandler.OnCoinGet += CoinsCountStart;
        ShowCoins(_coins);
        ShowWheat(_wheat);
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
}
