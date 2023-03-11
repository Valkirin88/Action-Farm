using System;
using TMPro;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{

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

    private void Start()
    {
        _playerView.OnWheatCountChanged += AddWheat;
    }

    void Update()
    {
        ShowResources();
    }

    private void ShowResources()
    {
        _coinsCounterText.text = "X" + _coins;
        _wheatCounterText.text = "X" + _wheat;
    }

    private void AddWheat(int wheat)
    {
        _wheat = wheat;
    }
}
