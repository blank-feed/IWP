using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform

    private CinemachineVirtualCamera virtualCamera;

    private bool toggleFollowPlayer = true;

    public float moveSpeed = 5f;
    public float minY = 0f;
    public float maxY = 10f;
    public float edgeThreshold = 50f;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        playerTransform = PlayerManager.instance.transform;

        if (playerTransform != null)
        {
            // Follow the player
            virtualCamera.Follow = playerTransform;
            virtualCamera.LookAt = playerTransform;
        }
        else
        {
            Debug.LogError("Player transform not assigned. Camera will not follow.");
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    toggleFollowPlayer = !toggleFollowPlayer;
        //    FollowPlayer(toggleFollowPlayer);
        //}

        //free cam!
        if (!toggleFollowPlayer)
        {
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Get the screen dimensions
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // Check if the mouse is near the edges of the screen
            bool nearLeftEdge = mousePosition.x < edgeThreshold;
            bool nearRightEdge = mousePosition.x > screenWidth - edgeThreshold;
            bool nearTopEdge = mousePosition.y > screenHeight - edgeThreshold;
            bool nearBottomEdge = mousePosition.y < edgeThreshold;

            // Calculate the new camera position
            Vector3 newPosition = virtualCamera.transform.position;

            if (nearLeftEdge)
            {
                newPosition.x -= moveSpeed * Time.deltaTime;
            }
            if (nearRightEdge)
            {
                newPosition.x += moveSpeed * Time.deltaTime;
            }
            if (nearTopEdge)
            {
                float clampedY = Mathf.Clamp(newPosition.y + moveSpeed * Time.deltaTime, minY, maxY);
                if (clampedY <= maxY)
                {
                    newPosition.y = clampedY;
                }
            }
            if (nearBottomEdge)
            {
                float clampedY = Mathf.Clamp(newPosition.y - moveSpeed * Time.deltaTime, minY, maxY);
                if (clampedY >= minY)
                {
                    newPosition.y = clampedY;
                }
            }

            // Update the camera's position
            virtualCamera.transform.position = newPosition;
        }

    }

    void FollowPlayer(bool ToF)
    {
        switch (ToF)
        {
            case true:
                virtualCamera.Follow = playerTransform;
                virtualCamera.LookAt = playerTransform;
                break;
            default:
                virtualCamera.Follow = null;
                virtualCamera.LookAt = null;
                break;
        }
    }
}
