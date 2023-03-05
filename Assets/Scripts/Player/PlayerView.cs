using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private const string Slash = "Slash";

    [SerializeField]
    private GameObject _sickle;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ShowOrHideSickle();
    }

    public void ShowSlash()
    {
        _animator.SetTrigger(Slash);
    }

    private void ShowOrHideSickle()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(Slash))
        {
            _sickle.SetActive(true);
        }
        else
            _sickle.SetActive(false);
    }
}
