using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;


public class WheatPatchView : MonoBehaviour
{
    private const string Sickle = "Sickle";

    public Action OnAllCut;

    [SerializeField]
    private GameObject[] _growingState;
    [SerializeField]
    private Material _crossSectionMaterial;
    [SerializeField]
    private GameObject _readyWheat;
    [SerializeField]
    private GameObject _cubePrefab;

    private int _stateTimer = 2;  //seconds
    private int _growingStep = 1;
    private int _cuttingStep = 1;
    private bool _isReadyToBeCut = false;

    private GameObject _highPart;
    private GameObject _lowPart;
    private GameObject _cubeOnScene;

    public GameObject CubeOnScene  => _cubeOnScene; 
    public bool IsReadyToBeCut => _isReadyToBeCut;

    private void Start()
    {
        StartGrowing();
    }

    public void StartGrowing()
    {
        StopAllCoroutines();
        _growingStep = 1;
        ShowWheatState(_growingStep);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sickle" && _growingStep == _growingState.Length && _isReadyToBeCut)
        {
           _isReadyToBeCut = false;
           StopAllCoroutines();
           StartCoroutine(WaitReadyToCut());
           Cut(_cuttingStep);
        }
    }

    private IEnumerator WaitReadyToCut()
    {
        yield return new WaitForSeconds(1);  //works as filter for multipe cut
        _isReadyToBeCut = true;
    }

    private IEnumerator NextGrowingState()
    {
        yield return new WaitForSeconds(_stateTimer);
        if (_growingStep < _growingState.Length)
        {
            _growingStep++;
        }
        ShowWheatState(_growingStep);
    }

    private void ShowWheatState(int _step)
    {
        switch (_step)
        {
            case 1:
                _growingState[0].SetActive(true);
                _growingState[1].SetActive(false);
                _growingState[2].SetActive(false);
                _growingState[3].SetActive(false);
                _growingState[4].SetActive(false);
                break;
            case 2:
                _growingState[0].SetActive(false);
                _growingState[1].SetActive(true);
                _growingState[2].SetActive(false);
                _growingState[3].SetActive(false);
                _growingState[4].SetActive(false);
                break;
            case 3:
                _growingState[0].SetActive(false);
                _growingState[1].SetActive(false);
                _growingState[2].SetActive(true);
                _growingState[3].SetActive(false);
                _growingState[4].SetActive(false);
                break;
            case 4:
                _growingState[0].SetActive(false);
                _growingState[1].SetActive(false);
                _growingState[2].SetActive(false);
                _growingState[3].SetActive(true);
                _growingState[4].SetActive(false);
                break;
            case 5:
                _growingState[0].SetActive(false);
                _growingState[1].SetActive(false);
                _growingState[2].SetActive(false);
                _growingState[3].SetActive(false);
                _growingState[4].SetActive(true);
                _isReadyToBeCut = true;
                break;
        }
        StartCoroutine(NextGrowingState());
    }

    private void Cut(int step)
    {
        switch(step)
        {
            case 0:
                Destroy(_highPart);
                Destroy(_lowPart);
                _cuttingStep++;
                _isReadyToBeCut=false;
                break;
            case 1:
                _growingState[4].SetActive(false);
                _highPart = Instantiate(SlicedKeeper.HighPart);
                _lowPart = Instantiate(SlicedKeeper.LowPart);
                _highPart.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                _lowPart.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                ShowCutAnim(_highPart);
                _cuttingStep++;
                break;
            case 2:
                ShowCutAnim(_lowPart);
                StartCoroutine(WaitForCube());
                break;
        }
    }

    private void ShowCutAnim(GameObject part)
    {
        part.transform.DOLocalRotate(new Vector3(0,0,90), 1, RotateMode.WorldAxisAdd);
    }

    private IEnumerator WaitForCube()
    {
        yield return new WaitForSeconds(1);
        Cut(_cuttingStep = 0);
        OnAllCut?.Invoke();
    }

    public void CreateCube()
    {
        _cubeOnScene = Instantiate(_cubePrefab, new Vector3(transform.position.x, transform.position.y + 0.18f, transform.position.z), Quaternion.identity);
    }
}

