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
        if (!_isIdle)
        transform.position = transform.position + new Vector3(_direction.x, 0, _direction.y) * Time.deltaTime * _acceleration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<WheatCube>(out WheatCube cube))
        {
            OnWheatCollected?.Invoke();
        }

        if (other.GetComponent<WheatZone>() != null)
        {
            Debug.Log("Slash");
            ShowSlash();
        }
        else
            Idle(_isIdle);
    }

    public void Move(Vector2 direction)
    {
        _direction = direction;
        _direction.Normalize();
        _isIdle = false;
        _animator.SetBool("Idle", _isIdle);
        _animator.SetFloat("Velocity X", _direction.x);
        _animator.SetFloat("Velocity Z", _direction.y);
        HideSickle();
    }

    public void Idle(bool isIdle)
    {
        _isIdle = isIdle;
        _animator.SetBool("Idle", _isIdle);
        //_animator.SetFloat("Velocity X", _direction.x);
        //_animator.SetFloat("Velocity Z", _direction.y);
        HideSickle();
    }

    private void ShowSlash()
    {
        _animator.SetTrigger(Slash);
     
       // StartCoroutine(WaitNextSlash());
    }

    private IEnumerator WaitNextSlash()
    {
        yield return new WaitForSeconds(2);
        ShowSlash();
    }

    private void ShowSickle()
    {
        _sickle.SetActive(true);
    }

    private void HideSickle()
    {
        Debug.Log("Hide");
        _sickle.SetActive(false);
    }

    //private IEnumerator HideSickle()
    //{
    //    yield return new WaitForSeconds(1f);
    //    _sickle.SetActive(false);
    //}
}
