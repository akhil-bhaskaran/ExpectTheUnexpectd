using UnityEngine;

public class UIIntroAnimation : MonoBehaviour
{
    public RectTransform whiteCloud;  // Assign White Cloud in Inspector
    public RectTransform greyCloud;   // Assign Grey Cloud in Inspector
    public RectTransform playerLayer; // Assign Player Layer in Inspector
    public RectTransform text;

    public float animationDuration = 2.0f; // Duration for each animation

    void Start()
    {
        // Animate the White Cloud from Left to Right
        LeanTween.moveX(whiteCloud, -248.98f, animationDuration).setEase(LeanTweenType.easeInOutQuad);

        // Animate the Grey Cloud from Bottom to Top
        LeanTween.moveY(greyCloud, -10.898f, animationDuration).setEase(LeanTweenType.easeInOutQuad);

        // Animate the Player Layer from Right to Left
        LeanTween.moveX(playerLayer, 161f, animationDuration).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.moveX(text, -545f, animationDuration).setEase(LeanTweenType.easeInOutQuad);
    }
    public void LoginBtn()
    {
        Debug.Log("Pressed");
    }
}
