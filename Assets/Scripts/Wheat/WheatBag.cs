using System;
using UnityEngine;
using DG.Tweening;

public class WheatBag : MonoBehaviour
{
    [SerializeField]
    private GameObject _barnHouse;

    private WheatCubeInBag[] _wheatCubes;
    private int _wheatCount = 0;
    private GameObject[] _loadedCube;
    private GameObject[] _unloadedCube;
    private float _cubeMoveTime = 0.2f;
    private float _timerForNextCubeUplad;
    
    

    private void Start()
    {
        _wheatCubes = GetComponentsInChildren<WheatCubeInBag>();
        
        for (int i = 0; i < _wheatCubes.Length; i++)
        {
            _loadedCube = new GameObject[_wheatCubes.Length];
            _loadedCube[i] = _wheatCubes[i].gameObject;
            _wheatCubes[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        _timerForNextCubeUplad += Time.deltaTime;
    }

    public void AddInBag()
    {
        _wheatCubes[_wheatCount].gameObject.SetActive(true);
        _wheatCount++;
    }

    public void UnloadWheat()
    {
        
        _unloadedCube = new GameObject[_wheatCount];
        ShowUnloadAnimation();
    }

    private void ShowUnloadAnimation()
    {
        
        var index = _wheatCount-1;
        
        if (index >= 0 && _timerForNextCubeUplad >_cubeMoveTime)
        {
            _timerForNextCubeUplad = 0;
            _unloadedCube[index] = Instantiate(_wheatCubes[index].gameObject, _wheatCubes[index].gameObject.transform.position, Quaternion.identity);
            _wheatCubes[index].gameObject.SetActive(false);
            _wheatCount--;
            _unloadedCube[index].transform.DOMove(_barnHouse.transform.position, _cubeMoveTime).onComplete = ShowUnloadAnimation;
            Destroy(_unloadedCube[index], _cubeMoveTime);
        }
    }
}
   