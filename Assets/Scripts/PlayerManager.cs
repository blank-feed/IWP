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

    public Sprite playerSprite;
    private SpriteRenderer spriteRenderer;

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
            transform.position = new Vector3 (999, 999, 0);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            if (!runOnce)
            {
                transform.position = new Vector3(0, 0, 0);
                spriteRenderer.sprite = playerSprite;
                runOnce = true;
            }
        }
    }

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
}
