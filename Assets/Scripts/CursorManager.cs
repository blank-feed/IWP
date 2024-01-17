using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class CursorManager : MonoBehaviour
{
    public Texture2D customCursor;
    public Texture2D customHoverCursor;
    private Texture2D cursor;
    bool FoundButton = false;

    void Start()
    {
        // Hide the default cursor
        Cursor.visible = false;

        cursor = customCursor;

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
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, customCursor.width, customCursor.height), cursor);

        // Check if the mouse is hovering over a button
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Get the current pointer event data
            PointerEventData eventData = new PointerEventData(EventSystem.current);

            // Set the position of the pointer event data to the current mouse position
            eventData.position = Input.mousePosition;

            // Create a list to store the raycast results
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            // Perform a raycast to check for UI elements and store the results in the list
            EventSystem.current.RaycastAll(eventData, raycastResults);

            // Check if any of the raycast results are buttons
            FoundButton = false;
            foreach (RaycastResult result in raycastResults)
            {
                Button hoveredButton = result.gameObject.GetComponent<Button>();
                if (hoveredButton != null)
                {
                    FoundButton = true;
                    LoadCursor(true);
                    break; // Exit the loop after the first button is found
                }
            }

            if (!FoundButton)
            {
                LoadCursor(false);
            }
        }
        else
        {
            LoadCursor(false);
        }
    }

    void LoadCursor(bool HoverButton)
    {
        if (HoverButton)
        {
            if (cursor != customHoverCursor)
            {
                cursor = customHoverCursor;
            }
        }
        else
        {
            if (cursor != customCursor)
            {
                cursor = customCursor;
            }
        }
    }
}
