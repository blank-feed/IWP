using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public GameObject[] GoneGameObjs;

    // Start is called before the first frame update
    void Start()
    {
        GoneGameObjs = GameObject.FindGameObjectsWithTag("gone");

        RemoveAll();
    }

    void RemoveAll()
    {
        foreach (GameObject obj in GoneGameObjs)
        {
            Destroy(obj);
        }
    }
}