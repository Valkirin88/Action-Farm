using UnityEngine;

public class AudioView : MonoBehaviour
{
    [SerializeField]
    private AudioClip _slash;

    public AudioClip Slash  => _slash;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
