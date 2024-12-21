using UnityEngine;
using System.Collections;

public class UIIntroAnimation : MonoBehaviour
{
    public RectTransform whiteCloud;  // Assign White Cloud in Inspector
    public RectTransform greyCloud;   // Assign Grey Cloud in Inspector
    public RectTransform playerLayer; // Assign Player Layer in Inspector
    public RectTransform text;
    public GameObject userNamePanel;
    public RectTransform registerPannel;
    public GameObject signInPannel;
    public GameObject statusPannel;
    
    public float animationDuration = 2.0f; // Duration for each animation

    void Start()
    { 
       Vector3 sign= signInPannel.GetComponent<RectTransform>().localScale ;
        // Animate the White Cloud from Left to Right
        LeanTween.moveX(whiteCloud, -248.98f, animationDuration).setEase(LeanTweenType.easeInOutQuad);

        // Animate the Grey Cloud from Bottom to Top
        LeanTween.moveY(greyCloud, -10.898f, animationDuration).setEase(LeanTweenType.easeInOutQuad);

        // Animate the Player Layer from Right to Left
        LeanTween.moveX(playerLayer, 161f, animationDuration).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.moveX(text, -545f, animationDuration).setEase(LeanTweenType.easeInOutQuad);
    }
    public void RegisterButtonPressed()
    {
        
        LeanTween.moveY(registerPannel,60f, animationDuration).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.moveY(signInPannel, -1522f, animationDuration).setEase(LeanTweenType.easeInOutQuad);
    }
   public void SignUpSubmit()
    {
        LeanTween.moveY(userNamePanel, 400, animationDuration).setEase(LeanTweenType.easeInOutQuad);
    }
    public void LoginButtonPressed() {
        LeanTween.moveY(signInPannel,550f, animationDuration).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.moveY(registerPannel, -1000f, animationDuration).setEase(LeanTweenType.easeInOutQuad);
    }
    public void LoginSubmit() {
        
    }
    public void UsernameCancel() {
        LeanTween.moveY(userNamePanel, -400, animationDuration).setEase(LeanTweenType.easeInOutQuad);
    }
    public  void ShowStatusPanel()
    {
        
        statusPannel.SetActive(true); // Show the panel
        StartCoroutine(HideStatusPanelAfterDelay(4f)); // Call the coroutine to hide it after 4 seconds
    }

    // Coroutine to hide the status panel after a delay
    private IEnumerator HideStatusPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay (4 seconds)
        statusPannel.SetActive(false);
    }
}
