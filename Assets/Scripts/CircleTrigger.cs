using UnityEngine;

public class CircleTrigger : MonoBehaviour
{
    public GameObject[] Target_UI;
    public Vector3[] NewPos;
    public Vector3[] OldPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < Target_UI.Length; i++)
            {
                Target_UI[i].GetComponent<RectTransform>().anchoredPosition = NewPos[i];
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < Target_UI.Length; i++)
            {
                Target_UI[i].GetComponent<RectTransform>().anchoredPosition = OldPos[i];
            }
        }
    }
}
