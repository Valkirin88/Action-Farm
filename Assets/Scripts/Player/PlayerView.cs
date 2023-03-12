using UnityEngine;
using System;

public class PlayerView : MonoBehaviour
{
    private const string Slash = "Slash";

    public Action OnNearBarn;
    public Action<int> OnWheatCountChanged;

    [SerializeField]
    private float _acceleration  = 4f;

    [SerializeField]
    private GameObject _sickle;

    [SerializeField]
    private AudioClip _slashSound;
    
    private AudioSource _audioSource;
    
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector2 _direction;

    private int _wheatCount;
    private bool _isIdle = true;
    private bool _isSlash = false;

    public int WheatCount => _wheatCount;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Idle", _isIdle);
    }

    private void FixedUpdate()
    {
        if (!_isIdle)
        {
            HideSickle();
            _animator.ResetTrigger(Slash);
            _rigidbody.velocity = (new Vector3(_direction.x, 0, _direction.y) * _acceleration);
        }

   
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<SlashZone>() != null && !_isSlash)
        {
            _isSlash = true;
            ShowSlash();
        }
        if (other.GetComponent<CoinsHandler>() && WheatCount > 0)
        {
            OnNearBarn?.Invoke();
            _wheatCount = 0;
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
        _audioSource.clip = _slashSound;
        _audioSource.Play();
        _sickle.SetActive(true);
        
    }

    private void HideSickle()
    {
   
        _sickle.SetActive(false);
        _isSlash = false;
       
    }

    public void AddWheat()
    {
        _wheatCount++;
        OnWheatCountChanged?.Invoke(WheatCount);
    }

}
