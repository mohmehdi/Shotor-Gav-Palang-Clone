using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance;
    AudioSource _audioSource;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        //DontDestroyOnLoad(this.gameObject);
    }
    public void PLay(AudioClip clip){
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
