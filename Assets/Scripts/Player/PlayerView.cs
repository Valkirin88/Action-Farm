using UnityEngine;
using System;

public class PlayerView : MonoBehaviour
{
    private const string Slash = "Slash";

    public Action OnWheatCollected;
    
    [SerializeField]
    private float _acceleration  = 2;

    [SerializeField]
    private GameObject _sickle;


    private Animator _animator;
    private Vector2 _direction;
   
    private bool _isIdle = true; 

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Idle", _isIdle);
    }

    private void Update()
    {
        if (!_isIdle)
        transform.position = transform.position + new Vector3(_direction.x, 0, _direction.y) * Time.deltaTime * _acceleration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<WheatCube>(out WheatCube cube))
        {
            OnWheatCollected?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<WheatZone>() != null)
        {
            ShowSlash();
        }
    }

    public void Move(Vector2 direction)
    {
        _direction = direction;
        _direction.Normalize();
        _animator.SetBool("Idle", false);
        _animator.SetFloat("Velocity X", _direction.x);
        _animator.SetFloat("Velocity Z", _direction.y);
        HideSickle();
    }

    public void Idle(bool isIdle)
    {
        _isIdle = isIdle;
        _animator.SetBool("Idle", _isIdle);
        HideSickle();
    }

    private void ShowSlash()
    {
        _animator.SetTrigger(Slash);
    }

    private void ShowSickle()
    {
        _sickle.SetActive(true);
    }

    private void HideSickle()
    {
        _sickle.SetActive(false);
    }
}
