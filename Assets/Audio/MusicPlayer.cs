using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    public AudioClip standard_bgm;
    public AudioClip battle_bgm;
    public AudioClip lowhp_bgm;
    private AudioSource musicSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        PlayBGM(0);
    }

    public void PlayBGM(int music_Num)
    {
        AudioClip musicClip;
        switch (music_Num)
        {
            case 0:
                musicClip = standard_bgm;
                break;
            case 1:
                musicClip = battle_bgm;
                break;
            case 2:
                musicClip = lowhp_bgm;
                break;
            default:
                musicClip = standard_bgm;
                break;
        }
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.Play();
    }
}
