
using System;

public class WheatController 
{
    private WheatCubeView _cubeView;
    private WheatPatchView _patchView;
    private PlayerView _playerView;
    private WheatBag _wheatBag;
    
    public WheatController(WheatPatchView patchView, WheatBag wheatBag ,PlayerView playerView)
    {
        _playerView = playerView;
        _patchView = patchView;
        _wheatBag = wheatBag;
        _playerView.OnWheatCollected += CubeCollect;
        _playerView.OnWheatCollected += ShowCollectAnimation;
        _playerView.OnWheatUnloaded += UnloadWheat;
        _patchView.OnAllCut += CreateCube;
    }

    private void CubeCollect()
    {
        _patchView.StartGrowing();
    }

    private void CreateCube()
    {
       _patchView.CreateCube();
       _cubeView = _patchView.CubeOnScene.GetComponent<WheatCubeView>();
       
    }

    private void ShowCollectAnimation()
    {
        _cubeView.ShowCollectAnimation();
        //_cubeView._OnAnimationDone += AddInBag;
        _wheatBag.AddInBag();

    }

    private void AddInBag()
    {
        _wheatBag.AddInBag();

    }

    private void UnloadWheat()
    {
        _wheatBag.UnloadWheat();
    }

    

}
