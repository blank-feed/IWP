using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public enum Race
    {
        Human,
        HighElf,
        Dragonborn
    }

    public enum Class
    {
        Paladin,
        Rogue,
        Sorcerer,
        Ranger,
        Fighter,
        Druid
    }

    public Race PlayerRace;
    public Class PlayerClass;

    private bool runOnce = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            transform.position = new Vector3 (999, 999, 0);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            if (!runOnce)
            {
                transform.position = new Vector3(0, 0, 0);
                runOnce = true;
            }
        }
    }
}
