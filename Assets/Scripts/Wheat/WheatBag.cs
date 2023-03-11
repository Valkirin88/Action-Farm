using UnityEngine;
using DG.Tweening;

public class WheatBag : MonoBehaviour
{
    [SerializeField]
    private GameObject _barnHouse;

    [SerializeField]
    private PlayerView _playerView;

    private WheatCubeInBag[] _wheatCubes;
    private int _wheatCount = 0;
    private GameObject[] _loadedCube;
    private GameObject[] _unloadedCube;
    private float _cubeMoveTime = 0.1f;
    private float _timerForNextCubeUplad;
    private int _index;
    private bool _isUnload;
    
    

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
        _timerForNextCubeUplad +=  Time.deltaTime;
        if(_isUnload && _timerForNextCubeUplad > 0.05)
        {
            _timerForNextCubeUplad = 0;
            ShowUnloadAnimation();
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
        _isUnload = true;
    }

    private void ShowUnloadAnimation()
    {
        _index = _wheatCount - 1;


        if (_index >= 0)
        {
            Debug.Log(_wheatCount);

            _unloadedCube[_index] = Instantiate(_wheatCubes[_index].gameObject, _wheatCubes[_index].gameObject.transform.position, Quaternion.identity);
            _wheatCubes[_index].gameObject.SetActive(false);
            _wheatCount--;
            _unloadedCube[_index].transform.DOMove(_barnHouse.transform.position, _cubeMoveTime);

            Destroy(_unloadedCube[_index], _cubeMoveTime);
        }
        if(_index == 0)
            _isUnload = false;

    }

    private void DestroyCube(GameObject cube, int wheatCount)
    {
        Destroy(cube, _cubeMoveTime);
    }
}
   