using UnityEngine;

public class CircleTrigger : MonoBehaviour
{
    public GameObject Hp_UI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hp_UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(625, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Hp_UI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
        }
    }
}
