using UnityEngine;

public class BootsManager : MonoBehaviour
{
    public Texture2D customCursor;

    void Start()
    {
        // Hide the default cursor
        Cursor.visible = false;

        // Check if another CursorManager already exists
        BootsManager[] bootsManagers = FindObjectsOfType<BootsManager>();
        if (bootsManagers.Length > 1 || PlayerManager.instance.bootsclicked)
        {
            // Destroy this instance to keep only one
            Destroy(gameObject);
        }

        // Make this object persistent across scenes
        DontDestroyOnLoad(gameObject);
    }

    void OnGUI()
    {
        // Draw the custom cursor at the mouse position
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y - 75, customCursor.width, customCursor.height), customCursor);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerManager.instance.bootsclicked = true;
            Destroy(gameObject);
        }
    }

}
