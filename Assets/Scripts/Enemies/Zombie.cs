using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;
using System.Collections;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidBody;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private Transform[] _patrolPoints;

    private GameObject _player;
    private float _runSpeed =1.5f;
    private float _walkSpeed = 1f;
    private int _quantityPoints;
    private int _nextPoint;

    private void Start()
    {
        _quantityPoints = _patrolPoints.Length;
        Patrol();
    }

    private void OnTriggerStay(Collider other)
        {
        if (other.GetComponent<PlayerView>())
        {
            _player = other.gameObject;
            FollowPlayer();
        }
    }

    private void Update()
    {
        if (_player != null)
        {
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance < 4)
                FollowPlayer();
            else
            {
                Patrol();
                _player = null;
            }
        }
        if (_agent.velocity.magnitude < 0.01f)
            Patrol();
    }
     
    private void FollowPlayer()
    {
        _animator.SetBool("Run", true);
        _agent.speed = _runSpeed;
        _agent.SetDestination(_player.transform.position);
    }

    private void Patrol()
    {
        StartCoroutine(ShowIdle());
        _nextPoint = UnityEngine.Random.Range(0, _quantityPoints);
    }

    private void WalkToPoint(int nextPoint)
    {
        _animator.SetBool("Walk", true);
        _agent.speed = _walkSpeed;
        _agent.SetDestination(_patrolPoints[nextPoint].transform.position);
    }

    private IEnumerator ShowIdle()
    {
        _agent.speed = 0;
        _animator.SetBool("Run", false);
        _animator.SetBool("Walk", false);
        yield return new WaitForSeconds(2);
        WalkToPoint(_nextPoint);
    }
}
