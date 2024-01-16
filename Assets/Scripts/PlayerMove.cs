using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 targetPosition;

    public GameObject indicatorPrefab;
    private GameObject indicatorInstance;

    private void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isMoving = false;
            HideIndicator();
        }

        MovePlayer();
    }

    private void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z; // Ensure the z-coordinate remains the same
        ShowIndicator();
        isMoving = true;
    }

    private void MovePlayer()
    {
        if (isMoving)
        {
            float step = PlayerManager.instance.moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                HideIndicator();
            }
        }
    }

    private void ShowIndicator()
    {
        if (indicatorInstance != null)
        {
            Destroy(indicatorInstance);
        }

        if (indicatorPrefab != null)
        {
            indicatorInstance = Instantiate(indicatorPrefab, targetPosition, Quaternion.identity);
        }
    }

    private void HideIndicator()
    {
        if (indicatorInstance != null)
        {
            Destroy(indicatorInstance);
        }
    }
}
