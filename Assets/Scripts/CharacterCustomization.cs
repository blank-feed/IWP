using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCustomization : MonoBehaviour
{
    public GameObject RaceSelectionUI;
    public GameObject ClassSelectionUI;

    public Button Race0btn;
    public Button Race1btn;
    public Button Race2btn;
    public Button Backbtn;

    public TextMeshProUGUI Class0Text;
    public TextMeshProUGUI Class1Text;

    // Start is called before the first frame update
    void Start()
    {
        Button Race0Btn = Race0btn.GetComponent<Button>();
        Button Race1Btn = Race1btn.GetComponent<Button>();
        Button Race2Btn = Race2btn.GetComponent<Button>();
        Button BackBtn = Backbtn.GetComponent<Button>();

        Race0Btn.onClick.AddListener(() => ClassType(0));
        Race1Btn.onClick.AddListener(() => ClassType(1));
        Race2Btn.onClick.AddListener(() => ClassType(2));
        BackBtn.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClassType(int x)
    {
        RaceSelectionUI.SetActive(!RaceSelectionUI.activeSelf);
        ClassSelectionUI.SetActive(!ClassSelectionUI.activeSelf);

        switch (x)
        {
            case 0:
                Class0Text.text = "Paladin";
                Class1Text.text = "Rogue";
                break;
            case 1:
                Class0Text.text = "Sorcerer";
                Class1Text.text = "Ranger";
                break;
            case 2:
                Class0Text.text = "Fighter";
                Class1Text.text = "Druid";
                break;
            default:
                break;
        }
    }

    void Back()
    {
        RaceSelectionUI.SetActive(!RaceSelectionUI.activeSelf);
        ClassSelectionUI.SetActive(!ClassSelectionUI.activeSelf);
    }
}
