using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumberIndicator : MonoBehaviour
{
    public TextMeshProUGUI parentText;
    public TextMeshProUGUI baseText;
    public TextMeshProUGUI bgText;

    Vector3 initialPos;
    Vector3 targetPosition;
    float lerpingValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        bgText.text = baseText.text;
        initialPos = transform.position;
        targetPosition = new Vector3(initialPos.x - 250.0f, initialPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        baseText.alpha = parentText.alpha;
        bgText.alpha = parentText.alpha;

        transform.position = Vector3.Lerp(initialPos, targetPosition, lerpingValue);
        parentText.alpha -= Time.deltaTime / 2;
        lerpingValue += Time.deltaTime / 2;

        if (parentText.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
