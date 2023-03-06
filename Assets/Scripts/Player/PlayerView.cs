using UnityEngine;
using System;

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
    }

    private void Update()
    {
        ShowOrHideSickle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<WheatCube>(out WheatCube cube))
        {
            OnWheatCollected?.Invoke();
        }
    }

    public void ShowSlash()
    {
        _animator.SetTrigger(Slash);
    }

    private void ShowOrHideSickle()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(Slash))
        {
            _sickle.SetActive(true);
        }
        else
            _sickle.SetActive(false);
    }
}
