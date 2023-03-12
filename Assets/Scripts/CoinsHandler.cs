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
 
    private float _movingTime = 0.02f;

    private Vector3 _coinWindows;
    private int _wheatCount;
    private GameObject[] _coin;
    private bool _isSelling;
    private int _index;
    

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = _playerView.transform;
        _coinWindows = new Vector3(_playerTransform.position.x-3, _playerTransform.position.y+3, _playerTransform.position.z +3);
        
        GetWheatCount(_playerView.WheatCount);
        _playerView.OnNearBarn += SellWheat;
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
        OnCoinGet?.Invoke(_index);
        MoveCoins();
    }

    private void MoveCoins()
    {
        if (_index >= 0 && _isSelling)
        {
            _coin[_index] = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            _coin[_index].transform.DOMove(_coinWindows, _movingTime).onComplete = DestroyCoin;
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
