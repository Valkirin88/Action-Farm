using System;
using UnityEngine;

public class PlayerController 
{
    [SerializeField]
    private PlayerView _view;
    
    private InputManager _inputManager;
    private SimpleTouchController _touchController;

    public PlayerController(InputManager inputManager, PlayerView view, SimpleTouchController touchController)
    {
        _view = view;
        _inputManager = inputManager;
        _touchController = touchController;
        _touchController.TouchEvent += PlayerMove;
        _touchController.TouchStateEvent += StopMoving;
    }

    private void StopMoving(bool touchPresent)
    {
        _view.Idle(!touchPresent);
    }

    private void PlayerMove(Vector2 vector2)
    {
        _view.Move(vector2);
    }


    public void Dispose()
    {
        _touchController.TouchEvent -= PlayerMove;
        _touchController.TouchStateEvent -= StopMoving;
    }

}
