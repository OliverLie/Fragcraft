using UnityEngine;
using UnityEngine.UI;

public class UiHealthbar : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Image ForegroundImage;
    public Image BackgroundImage;
    public Health health;


    void LateUpdate()
    {

        Vector3 direction = (target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;
        ForegroundImage.enabled = !isBehind;
        BackgroundImage.enabled = !isBehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        if (health.currentHealth == 100f)
        {
            ForegroundImage.enabled = false;
            BackgroundImage.enabled = false;
        }
        else
        {
            ForegroundImage.enabled = true;
            BackgroundImage.enabled = true;
        }
    }

    public void SetHealthBarPercentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        ForegroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        
    }
}
