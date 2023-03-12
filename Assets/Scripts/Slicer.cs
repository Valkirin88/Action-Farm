using EzySlice;
using UnityEngine;

public class Slicer : MonoBehaviour
{
    [SerializeField]
    private GameObject _readyWheat;
    [SerializeField]
    private Material _crossSectionMaterial;

    private GameObject[] _slicedObjects;


    private void Start()
    {
        _slicedObjects = Slice(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 0.5f), new Vector3(0, 1, 0), new TextureRegion());
        SlicedKeeper.HighPart = _slicedObjects[0];
        SlicedKeeper.LowPart = _slicedObjects[1];        
    }

    public GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion region)
    {
        return _readyWheat.SliceInstantiate(planeWorldPosition, planeWorldDirection, region, _crossSectionMaterial);
    }
}
