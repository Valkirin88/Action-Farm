using UnityEngine;

public class PlayerController 
{
    [SerializeField]
    private PlayerView _view;
    
    private InputManager _inputManager;

    public PlayerController(InputManager inputManager, PlayerView view)
    {
        _view = view;
        _inputManager = inputManager;
        _inputManager.OnSlash += ShowSlash;
    }

    private void ShowSlash()
    {
        _view.ShowSlash();
    }

    public void Dispose()
    {
        _inputManager.OnSlash -= ShowSlash;
    }

}
