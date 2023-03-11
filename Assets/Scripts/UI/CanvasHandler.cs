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
    private float _countdownTime = 0.2f;

    private void Start()
    {
        _playerView.OnWheatCountChanged += AddWheat;
        _playerView.OnNearBarn += WheatCountdown;
        ShowResources(0,0);
    }

       private void ShowResources(int coins, int wheat)
    {
        _coinsCounterText.text = "x" + coins;
        _wheatCounterText.text = "x" + wheat;
    }

    private void AddWheat(int wheat)
    {
        _wheat = wheat;
        _wheatCounterText.text = "x" + _wheat;
    }

    private void WheatCountdown()
    {
        if (_wheat > 0)
        {
            _wheatWindow.transform.DOShakePosition(0.2f, 10);
            StartCoroutine(WaitForNextCount());
        }
    }

    private IEnumerator WaitForNextCount()
    {
        yield return new WaitForSeconds(_countdownTime);
        _wheat--;
        ShowResources(_coins, _wheat);
        WheatCountdown();
    }
}
