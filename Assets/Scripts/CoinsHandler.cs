using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public class CoinsHandler : MonoBehaviour
{
    public Action<int> OnCoinGet;

    [SerializeField]
    private GameObject _coinPrefab;
    [SerializeField]
    private PlayerView _playerView;
    [SerializeField]
    private Transform _coinWindow;

    private float _movingTime = 0.03f;

    
    private int _wheatCount;
    private GameObject[] _coin;
    private bool _isSelling;
    private int _index;

    private Vector3 _barnHousePosition;
    private Vector3 _coinWindowPosition;



    private void Start()
    {
        GetWheatCount(_playerView.WheatCount);
        _playerView.OnNearBarn += SellWheat;
    }

    private void Update()
    {
        _coinWindowPosition = Camera.main.ScreenToWorldPoint(_coinWindow.position + new Vector3(0,0,5));
    }

    private void GetWheatCount(int wheatCount)
    {
        _wheatCount = wheatCount;
    }

    private void SellWheat()
    {
        _isSelling = true;
        
        GetWheatCount(_playerView.WheatCount);
        _coin = new GameObject[_wheatCount*15];
        StartCoroutine(WaitSelling());
    }
    private IEnumerator WaitSelling()
    {
        yield return new WaitForSeconds(2);
        _index = _wheatCount * 15 - 1;
        OnCoinGet?.Invoke(_wheatCount * 15);
        MoveCoins();
    }

    private void MoveCoins()
    {
        if (_index >= 0 && _isSelling)
        {
            _coin[_index] = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            
            
            _coin[_index].transform.DOMove(_coinWindowPosition, _movingTime).onComplete = DestroyCoin;
        }
        if(_index <0)
            _isSelling = false;
    }

    private void DestroyCoin()
    {
        Destroy(_coin[_index]);
        _index--;
        MoveCoins();
    }
}
