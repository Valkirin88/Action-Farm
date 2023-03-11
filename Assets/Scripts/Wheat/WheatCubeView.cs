using System;
using UnityEngine;
using DG.Tweening;

public class WheatCubeView : MonoBehaviour
{
    public Action _OnAnimationDone;
       
    public void ShowCollectAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z), 0.5f));
        sequence.Append(gameObject.transform.DOMove(gameObject.transform.position, 0.5f)).onComplete = CubeCollect;
    }

    private void CubeCollect()
    {
        _OnAnimationDone?.Invoke();
        Destroy(gameObject);
    }

    public void Destroy()
    {
        Debug.Log("Destroy");
        
    }
}
