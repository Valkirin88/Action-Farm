
using UnityEngine;

public class WheatCubeView : MonoBehaviour
{
   [SerializeField]
   private GameObject _cube;

   
    public void CreateCube()
    {
        Instantiate(_cube, new Vector3(transform.position.x, transform.position.y + 0.18f, transform.position.z), Quaternion.identity);
    }
}
