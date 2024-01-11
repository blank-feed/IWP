using UnityEngine;
using UnityEngine.UI;

public class ButtonAlpha : MonoBehaviour
{
    private Button button;
    private Image buttonImage;

    void Start()
    {
        // Get the button and its Image component
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
    }

    // Change the alpha when the mouse pointer enters the button
    public void OnPointerEnter()
    {
        ChangeAlpha(0.5f); // Set the desired alpha value
    }

    // Reset the alpha when the mouse pointer exits the button
    public void OnPointerExit()
    {
        ChangeAlpha(1f); // Set the desired alpha value
    }

    // Change the alpha of the button's sprite
    void ChangeAlpha(float alphaValue)
    {
        if (buttonImage != null)
        {
            Color currentColor = buttonImage.color;
            currentColor.a = alphaValue;
            buttonImage.color = currentColor;
        }
    }
}
