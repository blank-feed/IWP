using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public GameObject SkillTree;
    public bool openSkillTreeUI = false;

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

    // Start is called before the first frame update
    void Start()
    {
        SkillTree.SetActive(openSkillTreeUI);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            openSkillTreeUI = !openSkillTreeUI;
            SkillTree.SetActive(openSkillTreeUI);
            if (InventoryManager.instance.openInventoryUI)
            {
                InventoryManager.instance.openInventoryUI = false;
            }
        }
    }

    public void Pressed()
    {
        Debug.Log("ef");
    }
}
