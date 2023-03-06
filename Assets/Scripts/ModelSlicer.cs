using EzySlice;
using UnityEngine;

public class ModelSlicer : MonoBehaviour
{
    [SerializeField]
    private GameObject _wheat;
    public Material crossSectionMaterial;

    private void Start()
    {
        var slice = Slice(transform.position, new Vector3(0,1,0), new TextureRegion());
    }


    public GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion region)
    {
        return _wheat.SliceInstantiate(planeWorldPosition, planeWorldDirection, region, crossSectionMaterial);
    }

}
