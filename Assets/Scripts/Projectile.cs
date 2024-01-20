using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int atk;
    public GameObject DmgIndicatorPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the projectile collides with the player
        if (other.CompareTag("Player"))
        {
            // Destroy the projectile
            PlayerManager.instance.health -= atk;
            GameObject go = Instantiate(DmgIndicatorPrefab);
            go.transform.SetParent(GameObject.Find("Canvas").transform, false);
            go.transform.position = new Vector3(Camera.main.WorldToScreenPoint(PlayerManager.instance.transform.position).x, Camera.main.WorldToScreenPoint(PlayerManager.instance.transform.position).y + 75);
            go.GetComponent<DamageNumberIndicator>().baseText.text = "-" + atk.ToString();
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Check if a target is assigned
        if (PlayerManager.instance != null)
        {
            // Calculate the direction to the target
            Vector3 directionToTarget = (PlayerManager.instance.transform.position - transform.position).normalized;

            // Calculate the rotation angle to face the target
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // Set the initial rotation of the projectile
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.right * 5f * Time.deltaTime);
    }
}
