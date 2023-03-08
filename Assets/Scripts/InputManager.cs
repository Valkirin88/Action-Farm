using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InputManager 
{
    private GameObject _touchController;
    private Vector3 _touchPosition;
    private Image[] _cotrollerImages;


    public InputManager(GameObject touchController)
    {
        _touchController = touchController;
        _cotrollerImages = _touchController.GetComponentsInChildren<Image>();

    }

    public void Update()
    {
        CheckTouchInput();
    }

    private void CheckTouchInput()
    {
        //if you see it, it's costili ans I'm ashamed for thos code(it works, though) 
        if (Input.touchCount > 0)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _touchPosition = Input.GetTouch(0).position;
                _touchController.transform.position = _touchPosition;
                _touchController.transform.localScale = new Vector3(1, 1, 1);
                for (int i = 0; i < _cotrollerImages.Length; i++)
                {
                    _cotrollerImages[i].color = Color.white;
                }
            }
            else
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                for (int i = 0; i < _cotrollerImages.Length; i++)
                {

                    _cotrollerImages[i].color = Color.clear;

                }
                _touchController.transform.localScale = new Vector3(10, 10, 10);
            }
    }
}
