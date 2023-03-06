using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatCubeController 
{
    private WheatCubeView _cubeView;
    private WheatPatchView _patchView;

    public WheatCubeController(WheatCubeView cubeView, WheatPatchView patchView)
    {
        _cubeView = cubeView;
        _patchView = patchView;
        _patchView.OnAllCut = CreateCube;
    }

    private void CreateCube()
    {
        _cubeView.CreateCube();
    }
}
