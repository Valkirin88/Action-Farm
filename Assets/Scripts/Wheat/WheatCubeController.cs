
public class WheatCubeController 
{
    private WheatCubeView _cubeView;
    private WheatPatchView _patchView;

    public WheatCubeController(WheatCubeView cubeView, WheatPatchView patchView)
    {
        _cubeView = cubeView;
        _patchView = patchView;
        _patchView.OnAllCut = CreateCube;
        _cubeView.OnCubeCollect = CubeCollect;
    }

    private void CubeCollect()
    {
        _patchView.StartGrowing();
    }

    private void CreateCube()
    {
        _cubeView.CreateCube();
    }
}
