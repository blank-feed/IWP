using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public GameObject SkillTree;
    public bool openSkillTreeUI = false;
    public int skillClicked = 0;

    public GameObject skillImg;
    public GameObject skillName;
    public GameObject skillDesc;
    public GameObject skillBTN;
    public GameObject skillBTNtext;

    public Sprite defaultSprite;

    //bool s1 = false;
    //bool s2 = false;
    //bool s3 = false;
    //bool s4 = false;
    //bool s5 = false;

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
        skillImg.GetComponent<Image>().sprite = defaultSprite;
        skillName.GetComponent<TextMeshProUGUI>().text = "";
        skillDesc.GetComponent<TextMeshProUGUI>().text = "Select A Skill";
        skillBTN.SetActive(false);
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

    public void OnClickSkill_1()
    {
        skillClicked = 1;
        skillName.GetComponent<TextMeshProUGUI>().text = "Basic Ball";
        skillDesc.GetComponent<TextMeshProUGUI>().text = "Throws a basic ball";
        
        skillBTN.SetActive(true);
    }

    public void OnClickSkill_2()
    {
        skillClicked = 2;
        skillName.GetComponent<TextMeshProUGUI>().text = "Spiked Ball";
        skillDesc.GetComponent<TextMeshProUGUI>().text = "Throws a spiked ball";
        
        skillBTN.SetActive(true);
    }

    public void OnClickSkill_3()
    {
        skillClicked = 3;
        skillName.GetComponent<TextMeshProUGUI>().text = "Fire Ball";
        skillDesc.GetComponent<TextMeshProUGUI>().text = "Throws a fire ball";
        
        skillBTN.SetActive(true);
    }

    public void OnClickSkill_4()
    {
        skillClicked = 4;
        skillName.GetComponent<TextMeshProUGUI>().text = "Big Spiked Ball";
        skillDesc.GetComponent<TextMeshProUGUI>().text = "Throws a big spiked ball";
        
        skillBTN.SetActive(true);
    }

    public void OnClickSkill_5()
    {
        skillClicked = 5;
        skillName.GetComponent<TextMeshProUGUI>().text = "Big Fire Ball";
        skillDesc.GetComponent<TextMeshProUGUI>().text = "Throws a big fire ball";
        
        skillBTN.SetActive(true);
    }

    public void btn()
    {
        switch (skillClicked)
        {
            case 1:
                
                break;
            case 2:
                
                break;
            case 3:
                
                break;
            case 4:
                
                break;
            case 5:
                
                break;
            default:
                break;
        }
    }
}
