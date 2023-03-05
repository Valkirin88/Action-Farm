using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    private PlayerView _playerView;
    
    [SerializeField]
    private WheatPatchView[] _wheatPatchView;

    private InputManager _inputManager;
    private PlayerController _playerController;
    private WheatPatchController[] _wheatPatchController;

    void Awake()
    {
        _inputManager = new InputManager();
        _playerController = new PlayerController(_inputManager, _playerView);

        _wheatPatchController = new WheatPatchController[_wheatPatchView.Length];
        for (int i = 0; i < _wheatPatchView.Length; i++)
        {
            _wheatPatchController[i] = new WheatPatchController(_wheatPatchView[i]);
        }
    }

    void Update()
    {
        _inputManager.Update(); 
    }

    private void OnDestroy()
    {
        _playerController.Dispose();
    }
}
