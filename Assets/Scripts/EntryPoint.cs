using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private SimpleTouchController _touchController;

    [SerializeField]
    private PlayerView _playerView;

    [SerializeField]
    private GameObject[] _patches;
    
    private InputManager _inputManager;
    private PlayerController _playerController;
    private WheatPatchView[] _wheatPatchView;
    private WheatController[] _wheatController;
    private WheatCubeView[] _wheatCubeView;

    void Awake()
    {
        _inputManager = new InputManager();
        _playerController = new PlayerController(_inputManager, _playerView, _touchController);
        WheatControllerInitiate();
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        _playerController.Dispose();
    }

    private void WheatControllerInitiate()
    {
        _wheatController = new WheatController[_patches.Length];
        _wheatCubeView = new WheatCubeView[_patches.Length];
        _wheatPatchView = new WheatPatchView[_patches.Length];
        for(int i =0; i < _patches.Length; i++)
        {
            _wheatPatchView[i] = _patches[i].GetComponent<WheatPatchView>();
            _wheatCubeView[i] = _patches[i].GetComponent<WheatCubeView>();
            _wheatController[i] = new WheatController(_wheatCubeView[i], _wheatPatchView[i]);
        }
    }
}
