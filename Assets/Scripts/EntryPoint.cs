using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private PlayerView _playerView;

    [SerializeField]
    private GameObject[] _patches;
    
    private InputManager _inputManager;
    private PlayerController _playerController;
    private WheatPatchController[] _wheatPatchController;
    private WheatPatchView[] _wheatPatchView;
    private WheatCubeController[] _wheatCubeController;
    private WheatCubeView[] _wheatCubeView;

    void Awake()
    {
        _inputManager = new InputManager();
        _playerController = new PlayerController(_inputManager, _playerView);
        WheatPatchControllerInitiate();
        WheatCubeControllerInitiate();

    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        _playerController.Dispose();
    }

    private void WheatPatchControllerInitiate()
    {
        _wheatPatchController = new WheatPatchController[_patches.Length];
        _wheatPatchView = new WheatPatchView[_patches.Length];
        for (int i = 0; i < _patches.Length; i++)
        {
            _wheatPatchView[i] = _patches[i].GetComponent<WheatPatchView>();
            _wheatPatchController[i] = new WheatPatchController(_wheatPatchView[i]);
        }
    }

    private void WheatCubeControllerInitiate()
    {
        _wheatCubeController = new WheatCubeController[_patches.Length];
        _wheatCubeView = new WheatCubeView[_patches.Length];
        for(int i =0; i < _patches.Length; i++)
        {
            _wheatCubeView[i] = _patches[i].GetComponent<WheatCubeView>();
            _wheatCubeController[i] = new WheatCubeController(_wheatCubeView[i], _wheatPatchView[i]);
        }
    }
}
