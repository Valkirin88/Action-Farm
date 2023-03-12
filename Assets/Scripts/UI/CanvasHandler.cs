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
    private float _countdownTime = 0.1f;

    private int _coinsOnCounter;

    private void Start()
    {
        _playerView.OnWheatCountChanged += AddWheat;
        _playerView.OnNearBarn += WheatCountdown;
        _coinsHandler.OnCoinGet += CoinsCountStart;
        ShowResources(0,0);
    }

    private void ShowResources(int coins, int wheat)
    {
        _coinsCounterText.text =  $"{coins}";
        _wheatCounterText.text =  wheat + "/40";
    }

    private void AddWheat(int wheat)
    {
        _wheat = wheat;
        ShowResources(_coins, _wheat);
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
        ShowResources(_coins, _wheat);
        WheatCountdown();
    }

    private void CoinsCountStart(int coinsCount)
    {
        StopAllCoroutines();
        _coins = _coins + coinsCount;
        CoinsCountUp();
        Debug.Log(_coins);
    }

    private void CoinsCountUp()
    {
        if (_coinsOnCounter < _coins)
        {
            _coinsWindow.transform.DOShakePosition(0.02f, 10);
            StartCoroutine(WaitForNextCoinCount());
        }
    }

    private IEnumerator WaitForNextCoinCount()
    {
        yield return new WaitForSeconds(0.02f);
        _coinsOnCounter++;
        ShowResources(_coinsOnCounter, 0);
        CoinsCountUp();
    }
}
