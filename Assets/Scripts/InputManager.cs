using System;
using UnityEngine;

public class InputManager 
{
    public Action OnSlash;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnSlash?.Invoke();
        }

    }
}
