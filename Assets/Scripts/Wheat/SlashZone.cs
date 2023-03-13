using UnityEngine;

public class SlashZone : MonoBehaviour
{
    public bool _isReadyToBeCut;

   
    private WheatPatchView _wheatPatchView;

    private void Start()
    {
        _wheatPatchView = GetComponentInParent<WheatPatchView>();
    }

    private void Update()
    {
        _isReadyToBeCut = _wheatPatchView.IsReadyToBeCut;
    }
}
