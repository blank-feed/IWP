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
    private bool inRange = false;
    public bool dialogueStarted = false;

    //sprites
    public Sprite playerSprite;
    public SpriteRenderer spriteRenderer;

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
    private Slider ManaBar;

    //TMPros
    private TextMeshProUGUI HealthText;
    private TextMeshProUGUI LevelText;
    private TextMeshProUGUI ManaText;

    //Die
    public int MaxDieNum = 20;

    GameObject enemy;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //playerSkills = new PlayerSkills();

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
                SceneStart();
            }

            //if (Input.GetKeyDown(KeyCode.X))
            //{
            //    InventoryManager.instance.RemoveItem("Egg", 1);
            //}

            if (Input.GetKeyDown(KeyCode.F) && inRange && !DialogueManager.isActive)
            {
                if (ConversableGOInrage != null)
                {
                    ConversableGOInrage.GetComponent<DialogueTrigger>().StartDialogue();
                }
                //FindAnyObjectByType<DialogueTrigger>().StartDialogue();
                dialogueStarted = true;
            }

            if (inRange)
            {
                newPrefabInstance.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position);
            }

            //LevelCheck();

            //Update UI values
            UIUpdate();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("conversable"))
        {
            ConversableGOInrage = other.gameObject;
            newPrefabInstance = Instantiate(InteractIcon_prefab, Vector3.zero, Quaternion.identity);
            newPrefabInstance.name = "Interact_Icon";
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                newPrefabInstance.transform.SetParent(canvas.transform, false);
            }
            inRange = true;
            enemy = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("conversable"))
        {
            ConversableGOInrage = null;
            GameObject destroying = GameObject.Find("Interact_Icon");
            Destroy(destroying);
            inRange = false;
            enemy = null;
        }
    }

    void UIUpdate()
    {
        ExpBar.value = exp;
        HealthBar.value = health;
        ManaBar.value = mana;
        HealthText.text = health.ToString();
        LevelText.text = level.ToString();
        ManaText.text = mana.ToString();
    }

    void SceneStart()
    {
        transform.position = new Vector3(0, 0, 0);
        spriteRenderer.sprite = playerSprite;
        ExpBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        HealthBar = GameObject.Find("Healthbar").GetComponent<Slider>();
        ManaBar = GameObject.Find("ManaBar").GetComponent<Slider>();
        HealthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        LevelText = GameObject.Find("LevelNum").GetComponent<TextMeshProUGUI>();
        ManaText = GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>();
        ExpBar.maxValue = maxExp;
        HealthBar.maxValue = maxHealth;
        ManaBar.maxValue = maxMana;
        runOnce = true;
    }

    void LevelCheck()
    {
        if (exp == maxExp)
        {
            level++;
            SkillPoints++;
            exp = 0;
            maxExp += 10;
            ExpBar.maxValue = maxExp;
        }
    }

    //public PlayerSkills GetPlayerSkills()
    //{
    //    //return playerSkills;
    //}

    public void FlipSprite(bool ToF)
    {
        if (ToF)
        {
            if (transform.localScale.x > 0)
            {
                // Get the current local scale
                Vector3 scale = transform.localScale;

                // Invert the X scale to flip horizontally
                scale.x *= -1;

                // Update the local scale
                transform.localScale = scale;
            }
        }
        else
        {
            if (transform.localScale.x < 0)
            {
                // Get the current local scale
                Vector3 scale = transform.localScale;

                // Invert the X scale to flip horizontally
                scale.x *= -1;

                // Update the local scale
                transform.localScale = scale;
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
