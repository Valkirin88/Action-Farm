using EzySlice;
using System;
using System.Collections;
using UnityEngine;

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

    private int _stateTimer = 2;  //seconds
    private int _growingStep = 1;

    private void Start()
    {
        ShowWheatState(_growingStep);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sickle" && _growingStep == _growingState.Length)
        {
            StopAllCoroutines();

            Cut();
           // OnAllCut?.Invoke();
        }
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
                break;

        }
        StartCoroutine(NextGrowingState());
    }

    private void Cut()
    {
        Slice(new Vector3(transform.position.x, transform.position.y +0.5f, transform.position.z - 0.5f), new Vector3(0, 1, 0), new TextureRegion());
       // _growingState[4].SetActive(false);
    }

    public GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion region)
    {
        return _readyWheat.SliceInstantiate(planeWorldPosition, planeWorldDirection, region, _crossSectionMaterial);
    }
}

