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

    public void AddInBag()
    {
        _wheatCubes[_wheatCount].gameObject.SetActive(true);
        _wheatCount++;
    }

    public void UnloadWheat()
    {
        
        _unloadedCube = new GameObject[_wheatCount];
       
        for (int i = _wheatCount; i > 0; i--)
        {
            _unloadedCube[i] = Instantiate(_loadedCube[i], _loadedCube[i].transform.position, Quaternion.identity);
            _loadedCube[i].SetActive(false);
          //  ShowUnloadAnimation(_unloadedCube[i]);
        }
    }

    private void ShowUnloadAnimation(GameObject cube)
    {
        cube.transform.DOMove(_barnHouse.transform.position, 0.2f);
    }
}
   