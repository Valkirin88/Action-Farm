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
       
    }

    public void Dispose()
    {
       
    }

}
