using System;
using UnityEngine;
using DG.Tweening;

public class WheatCubeView : MonoBehaviour
{
    public Action OnCubeCollect;

    [SerializeField]
    private GameObject _cubePrefab;

    private GameObject _cubeOnScene;
    private Transform _playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (_cubeOnScene != null && other.GetComponent<PlayerView>())
        {
            _playerTransform= other.GetComponentInChildren<Spine>().transform;
            ShowCollectAnimation();
        }
    }

    public void CreateCube()
    {
        _cubeOnScene = Instantiate(_cubePrefab, new Vector3(transform.position.x, transform.position.y + 0.18f, transform.position.z), Quaternion.identity);
    }

    private void ShowCollectAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_cubeOnScene.transform.DOMove(new Vector3(_cubeOnScene.transform.position.x, _cubeOnScene.transform.position.y + 0.5f, _cubeOnScene.transform.position.z), 0.5f));
        sequence.Append(_cubeOnScene.transform.DOMove(_playerTransform.position, 0.5f)).onComplete = CubeCollect;
    }

    private void CubeCollect()
    {
        OnCubeCollect?.Invoke();
        Destroy(_cubeOnScene);
    }
}
