using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private bool isMoving = false;

    private Vector3 targetPosition;

    private void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isMoving = false;
        }

        MovePlayer();
    }

    private void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (targetPosition.x < transform.position.x)
        {
            PlayerManager.instance.FlipSprite(true);
        }
        else
        {
            PlayerManager.instance.FlipSprite(false);
        }
        targetPosition.z = transform.position.z; // Ensure the z-coordinate remains the same
        isMoving = true;
        return;
    }

    private void MovePlayer()
    {
        if (isMoving)
        {
            float step =  PlayerManager.instance.moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
}
