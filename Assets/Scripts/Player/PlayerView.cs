using UnityEngine;
using System;
using System.Collections;

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
        Debug.Log(_isIdle);
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

    public void Move(Vector2 direction)
    {
        _direction = direction;
        _isIdle = false;
        _animator.SetBool("Idle", _isIdle);
        _animator.SetFloat("Velocity X", _direction.x);
        _animator.SetFloat("Velocity Z", _direction.y);
    }

    public void Idle(bool isIdle)
    {
        _isIdle = isIdle;
        _animator.SetBool("Idle", _isIdle);
        _animator.SetFloat("Velocity X", _direction.x);
        _animator.SetFloat("Velocity Z", _direction.y);
    }

    private void ShowSlash()
    {
        //_animator.SetTrigger(Slash);
        //ShowSickle();
        //StartCoroutine(WaitNextSlash());
        
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
