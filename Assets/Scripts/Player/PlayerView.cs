using UnityEngine;
using System;
using System.Collections;

public class PlayerView : MonoBehaviour
{
    private const string Slash = "Slash";

    public Action OnWheatCollected;

    [SerializeField]
    private GameObject _sickle;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        ShowSlash();
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<WheatCube>(out WheatCube cube))
        {
            OnWheatCollected?.Invoke();
        }
    }

    private void ShowSlash()
    {
        _animator.SetTrigger(Slash);
        ShowSickle();
        StartCoroutine(WaitNextSlash());
        
    }

    private IEnumerator WaitNextSlash()
    {
        yield return new WaitForSeconds(2);
        ShowSlash();
    }

    private void ShowSickle()
    {
        _sickle.SetActive(true);
        StartCoroutine(HideSickle());
    }

    private IEnumerator HideSickle()
    {
        yield return new WaitForSeconds(0.5f);
        _sickle.SetActive(false);
    }
}
