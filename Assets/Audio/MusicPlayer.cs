using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    public AudioClip standard_bgm;
    public AudioClip battle_bgm;
    public AudioClip lowhp_bgm;
    public AudioClip win_bgm;
    public AudioClip lose_bgm;
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
        musicSource = GetComponent<AudioSource>();
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
            case 3:
                musicClip = win_bgm;
                musicSource.loop = false;
                break;
            case 4:
                musicClip = lose_bgm;
                musicSource.loop = false;
                break;
            default:
                musicClip = standard_bgm;
                break;
        }
        musicSource.clip = musicClip;
        musicSource.Play();
    }
}
