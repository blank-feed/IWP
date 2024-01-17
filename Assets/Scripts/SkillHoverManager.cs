using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillHoverManager : MonoBehaviour
{
    public GameObject SkillDescriptionUI;

    public Image Skill1Img;
    public Image Skill2Img;
    public Image Skill3Img;

    public TextMeshProUGUI Skill1Name;
    public TextMeshProUGUI Skill2Name;
    public TextMeshProUGUI Skill3Name;

    public TextMeshProUGUI Skill1Desc;
    public TextMeshProUGUI Skill2Desc;
    public TextMeshProUGUI Skill3Desc;

    public Image BoonImg;
    public TextMeshProUGUI BoonName;
    public TextMeshProUGUI BoonDesc;


    void Start()
    {
        SkillDescriptionUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            OnF1KeyPress();
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {
            OnF1KeyReleased();
        }
    }

    public void OnF1KeyPress()
    {
        SkillDescriptionUI.SetActive(true);

        Skill1Img.sprite = PlayerSkills.instance.S1_Img;
        Skill1Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S1);
        Skill1Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S1);

        Skill2Img.sprite = PlayerSkills.instance.S2_Img;
        Skill2Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S2);
        Skill2Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S2);

        Skill3Img.sprite = PlayerSkills.instance.S3_Img;
        Skill3Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S3);
        Skill3Desc.text = PlayerSkills.instance.GetSkillDescription(PlayerSkills.instance.S3);

        BoonImg.sprite = BattleManager.instance.BoonImg.sprite;
        string boonnamestr = "";
        switch (PlayerManager.instance.PlayerClass)
        {
            case PlayerManager.Class.Paladin:
                boonnamestr = "Deficiency";
                BoonDesc.text = "The more health lost, the more damage you deal";
                break;
            case PlayerManager.Class.Rogue:
                boonnamestr = "Confiscation";
                BoonDesc.text = "The more damage stolen, the more damage you deal";
                break;
            case PlayerManager.Class.Sorcerer:
                boonnamestr = "DKDC";
                BoonDesc.text = "DKDC";
                break;
            case PlayerManager.Class.Ranger:
                boonnamestr = "Crippled";
                BoonDesc.text = "The further away you are from the enemy, the more damage you deal";
                break;
            case PlayerManager.Class.Fighter:
                boonnamestr = "Momentum";
                BoonDesc.text = "The more skills you use in a row, the more damage you deal";
                break;
            case PlayerManager.Class.Druid:
                boonnamestr = "Dragon Pals";
                BoonDesc.text = "The more Dragon Pals, the more damage you deal";
                break;
            default:
                break;
        }
        BoonName.text = "Boon :\n" + boonnamestr;
    }

    public void OnF1KeyReleased()
    {
        SkillDescriptionUI.SetActive(false);
    }
}
