using System;
using UnityEngine;

public class WheatCubeView : MonoBehaviour
{
    public Action OnCubeCollect;

    [SerializeField]
    private GameObject _cubePrefab;

    private GameObject _cubeOnScene;

    private void OnTriggerEnter(Collider other)
    {
        if (_cubeOnScene != null && other.GetComponent<PlayerView>())
        {
            OnCubeCollect?.Invoke();
            Destroy(_cubeOnScene);
        }
    }

    public void CreateCube()
    {
       _cubeOnScene = Instantiate(_cubePrefab, new Vector3(transform.position.x, transform.position.y + 0.18f, transform.position.z), Quaternion.identity);
    }
}
