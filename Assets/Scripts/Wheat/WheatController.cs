
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
    
        _playerView.OnNearBarn += UnloadWheat;
        _patchView.OnAllCut += CreateCube;
    }

    private void CubeCollect()
    {
        _cubeView.OnCubeCollect -= CubeCollect;
        _patchView.StartGrowing();
        ShowCollectAnimation();
        _playerView.AddWheat();
    }

    private void CreateCube()
    {
       _patchView.CreateCube();
       _cubeView = _patchView.CubeOnScene.GetComponent<WheatCubeView>();
        _cubeView.OnCubeCollect += CubeCollect;    
          
    }

    private void ShowCollectAnimation()
    {
        _cubeView.ShowCollectAnimation();
        _cubeView.OnAnimationDone += AddInBag;
    }

    private void AddInBag()
    {
        _cubeView.OnAnimationDone -= AddInBag;
        _wheatBag.AddInBag();
    }

    private void UnloadWheat()
    {
        _wheatBag.UnloadWheat();
    }
}
