using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public GameObject ballPrefab;
    public float shootForce = 10f;
    public float ballLifetime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (PlayerManager.instance.mana >= 10)
            {
                //PlayerManager.instance.mana -= 10;
                StartCoroutine(GradualManaDecrease(10));
                ShootBall();
            }
        }
    }

    void ShootBall()
    {
        // Create a new instance of the ball prefab
        GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        // Calculate the direction towards the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - newBall.transform.position).normalized;

        // Get the Rigidbody2D component of the ball
        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();

        // Apply force to the ball to shoot it in the calculated direction
        rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);

        Destroy(newBall, ballLifetime);
    }

    IEnumerator GradualManaDecrease(int manaCost)
    {
        float decreaseSpeed = .01f; // Adjust the speed as needed.
        int targetMana = PlayerManager.instance.mana - manaCost;

        while (PlayerManager.instance.mana > targetMana)
        {
            PlayerManager.instance.mana -= Mathf.CeilToInt(decreaseSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
