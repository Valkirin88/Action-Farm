using System;
using UnityEngine;
using UnityEngine.Animations;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody;

    [SerializeField]
    private Animator _animator;

    private GameObject _player;
    private float _speed =1f;

    private void OnTriggerStay(Collider other)
        {
        if (other.GetComponent<PlayerView>())
        {
            _player = other.gameObject;
            CatchPlayer();
        }
    }

    private void CatchPlayer()
    {
        Debug.Log("Look");
        transform.LookAt(_player.transform.position);
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed *Time.deltaTime);
        _animator.SetBool("Follow", true);
    }

    private void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        //if(distance > 2)

    }

}
