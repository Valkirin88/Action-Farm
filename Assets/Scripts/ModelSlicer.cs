using EzySlice;
using UnityEngine;

public class ModelSlicer : MonoBehaviour
{
    [SerializeField]
    private GameObject _wheat;


    private void Start()
    {
        var slice = Slice(transform.position, new Vector3(1,1,1));
    }


    public GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection)
    {
        
        return _wheat.SliceInstantiate(planeWorldPosition, planeWorldDirection);
    }

}
