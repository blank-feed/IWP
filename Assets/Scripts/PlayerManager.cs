using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject InteractIcon_prefab;
    GameObject newPrefabInstance;
    GameObject ConversableGOInrage;
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
    public bool inRange = false;
    public bool dialogueStarted = false;
    public bool bootsclicked = false;

    //sprites
    public Sprite playerSprite;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteChosen;

    //player stats

    //speed
    public float moveSpeed = 5f; // Adjust the speed to your liking

    //health
    public int health = 100;
    public int maxHealth = 100;

    //mana
    public int mana = 100;
    public int maxMana = 100;

    //level / exp
    public int level = 1;
    public int exp = 0;
    public int maxExp = 100;
    public int SkillPoints = 1;

    //sliders
    private Slider HealthBar;
    private Slider ExpBar;
    //private Slider ManaBar;

    //TMPros
    private TextMeshProUGUI HealthText;
    private TextMeshProUGUI LevelText;
    private TextMeshProUGUI ManaText;

    //Die
    public int MaxDieNum = 20;

    //animation shits
    public Animator playerAnimator;

    GameObject talkablePerson;

    public GameObject prefab_Inventory;

    public bool fightable;

    public Vector3 lastPos = new Vector3(54, 31, 0);

    public Vector3 lor = new Vector3(5, 5, 1);

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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
            transform.position = new Vector3 (0, 0, 0);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            
            if (!runOnce)
            {
                SceneStart();
            }
            else
            {
                lastPos = transform.position;
            }

            if (Input.GetKeyDown(KeyCode.F) && inRange && !DialogueManager.isActive)
            {
                if (ConversableGOInrage != null)
                {
                    ConversableGOInrage.GetComponent<DialogueTrigger>().StartDialogue();
                    inRange = false;
                }
                dialogueStarted = true;
            }

            if (inRange)
            {
                newPrefabInstance.transform.position = Camera.main.WorldToScreenPoint(talkablePerson.transform.position);
            }

            LevelCheck();

            //Update UI values
            UIUpdate();
        }
        else
        {
            runOnce = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("conversable"))
        {
            ConversableGOInrage = other.gameObject;
            if (newPrefabInstance == null)
            {
                newPrefabInstance = Instantiate(InteractIcon_prefab, Vector3.zero, Quaternion.identity);
                newPrefabInstance.name = "Interact_Icon";
                Canvas canvas = FindObjectOfType<Canvas>();
                if (canvas != null)
                {
                    newPrefabInstance.transform.SetParent(canvas.transform, false);
                }
            }
            inRange = true;
            talkablePerson = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("conversable"))
        {
            ConversableGOInrage = null;
            GameObject destroying = GameObject.Find("Interact_Icon");
            newPrefabInstance = null;
            Destroy(destroying);
            inRange = false;
            talkablePerson = null;
        }
    }

    void UIUpdate()
    {
        //transform.localScale = new Vector3(5f, 5f, 1f);
        transform.localScale = lor;
        playerSprite = spriteChosen;
        ExpBar.value = exp;
        HealthBar.value = health;
        //ManaBar.value = mana;
        HealthText.text = health.ToString();
        LevelText.text = level.ToString();
        //ManaText.text = mana.ToString();
    }

    void SceneStart()
    {
        transform.position = lastPos;
        spriteRenderer.sprite = playerSprite;
        ExpBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        HealthBar = GameObject.Find("Healthbar").GetComponent<Slider>();
        //ManaBar = GameObject.Find("ManaBar").GetComponent<Slider>();
        HealthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        LevelText = GameObject.Find("LevelNum").GetComponent<TextMeshProUGUI>();
        //ManaText = GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>();
        ExpBar.maxValue = maxExp;
        HealthBar.maxValue = maxHealth;
        //ManaBar.maxValue = maxMana;
        Sprite playerpfp = GameObject.Find("PlayerPFP").GetComponent<Image>().sprite = spriteChosen;
        GameObject.Find("PlayerPFP").transform.localScale = new Vector3(.3f, .3f, 1);
        runOnce = true;
    }

    void LevelCheck()
    {
        if (exp >= maxExp)
        {
            //level up
            level++;
            //set exp back to 0 and increases max exp
            exp = exp - maxExp;
            maxExp += 10;
            ExpBar.maxValue = maxExp;
            //increase max health
            maxHealth += 15;
            health += 15;
            HealthBar.maxValue = maxHealth;
        }
    }

    public void FlipSprite(bool ToF)
    {
        if (ToF)
        {
            if (transform.localScale.x > 0)
            {
                //// Get the current local scale
                //Vector3 scale = transform.localScale;

                //// Invert the X scale to flip horizontally
                //scale.x *= -1;

                //// Update the local scale
                //transform.localScale = scale;
                lor.x = -5;
            }
        }
        else
        {
            if (transform.localScale.x < 0)
            {
                //// Get the current local scale
                //Vector3 scale = transform.localScale;

                //// Invert the X scale to flip horizontally
                //scale.x *= -1;

                //// Update the local scale
                //transform.localScale = scale;
                lor.x = 5;
            }
        }
    }

    IEnumerator GradualExpIncrease(int expAdded)
    {
        float decreaseSpeed = .01f; // Adjust the speed as needed.
        int targetExp = exp + expAdded;

        while (exp < targetExp)
        {
            exp += Mathf.CeilToInt(decreaseSpeed * Time.deltaTime);
            yield return null;
        }

        if (exp == maxExp)
        {
            LevelCheck();
        }
    }
}
