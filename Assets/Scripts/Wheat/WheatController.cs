
public class WheatController 
{
    private WheatCubeView _cubeView;
    private WheatPatchView _patchView;
    private PlayerView _playerView;
    
    public WheatController(WheatPatchView patchView, PlayerView playerView)
    {
        _playerView = playerView;
        _patchView = patchView;

        _playerView.OnWheatCollected += CubeCollect;
        _playerView.OnWheatCollected += ShowCollectAnimation;
        _patchView.OnAllCut = CreateCube;
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
    }
}
