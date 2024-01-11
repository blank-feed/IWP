using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D customCursor;

    void Start()
    {
        // Hide the default cursor
        Cursor.visible = false;

        // Check if another CursorManager already exists
        CursorManager[] cursorManagers = FindObjectsOfType<CursorManager>();
        if (cursorManagers.Length > 1)
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
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, customCursor.width, customCursor.height), customCursor);
    }
}
