using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    public List<GameObject> UncollectedGameObjs;
    public List<GameObject> CollectedGameObjs;
    public bool RunOnce = false;

    // Start is called before the first frame update
    void Awake()
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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            if (!RunOnce)
            {
                RemoveAll();
            }
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
        {
            RunOnce = false;
        }
    }

    void RemoveAll()
    {
        if (CollectedGameObjs == null)
        {
            RunOnce = true;
            return;
        }
        foreach (GameObject obj in CollectedGameObjs)
        {
            Destroy(obj);
        }
        RunOnce = true;
    }
}