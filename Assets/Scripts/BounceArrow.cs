using UnityEngine;

public class BounceArrow : MonoBehaviour
{
    public float bounceHeight = .5f;
    public float bounceDuration = 1f;
    Vector3 ori_pos;

    void Start()
    {
        // Initial setup
        ori_pos = gameObject.transform.position;
        Bounce();
    }

    void Bounce()
    {
        // Bounce up and then drop down
        LeanTween.moveY(gameObject, transform.position.y + bounceHeight, bounceDuration / 2f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() => BounceDown());
    }

    void BounceDown()
    {
        // Drop down to the original position
        LeanTween.moveY(gameObject, ori_pos.y, bounceDuration / 2f)
            .setEase(LeanTweenType.easeInQuad)
            .setOnComplete(() => Bounce());
    }
}
