using System;
using UnityEngine;
using DG.Tweening;

public class WheatCubeView : MonoBehaviour
{
    public Action OnAnimationDone;
    public Action OnCubeCollect;

    private Sequence sequence;
    private Transform _playerTransform;
    private int _cubeCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerView>(out PlayerView playerView))
        {
            _playerTransform = playerView.transform;
            if (playerView.WheatCount < 40)
            {

                _cubeCount++;
                OnCubeCollect?.Invoke();
                ShowCollectAnimation();
            }
        }
    }

    public void ShowCollectAnimation()
    {
        
        gameObject.GetComponent<Collider>().enabled = false;
        sequence = DOTween.Sequence();
        sequence.Append(gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z), 0.2f)).onComplete = CubeCollect;
        //sequence.Append(gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z), 0.2f)).onComplete = CubeCollect;
        
    }

    private void CubeCollect()
    {
        sequence.Kill();
        OnAnimationDone?.Invoke();
        Destroy(gameObject,0.1f);
    }
}
