using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private SimpleTouchController _touchController;

    [SerializeField]
    private PlayerView _playerView;

    //[SerializeField]
    //private GameObject _wheatZone;

    [SerializeField]
    private GameObject[] _patches;


    [SerializeField]
    private GameObject _touchControllerObject;
    
    private InputManager _inputManager;
    private PlayerController _playerController;
    private WheatPatchView[] _wheatPatchView;
    private WheatController[] _wheatController;


    void Awake()
    {
        _inputManager = new InputManager(_touchControllerObject);
        _playerController = new PlayerController(_inputManager, _playerView, _touchController);
       
        WheatControllerInitiate();
    }

    void Update()
    {
        _inputManager.Update();
    }

    private void OnDestroy()
    {
        _playerController.Dispose();
    }

    private void WheatControllerInitiate()
    {
        _wheatController = new WheatController[_patches.Length];
     
        _wheatPatchView = new WheatPatchView[_patches.Length];
        for(int i =0; i < _patches.Length; i++)
        {
            _wheatPatchView[i] = _patches[i].GetComponent<WheatPatchView>();
           
            _wheatController[i] = new WheatController(_wheatPatchView[i], _playerView);
        }
    }
}
