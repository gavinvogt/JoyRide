using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BoostIndicator : MonoBehaviour
{
    bool isActive = false;
    Vector2 carPosition = Vector2.zero;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] RectTransform canvasRectTransform;

    void FixedUpdate()
    {
        if (isActive)
        {
            //Rotate and grow
            //transform.Rotate(0, 0, 5);
            transform.localScale *= 1.02f;
        }
    }

    public void Activate(Vector2 carPos)
    {
        carPosition = Camera.main.WorldToViewportPoint(carPos);
        rectTransform.anchoredPosition = new Vector2(
            ((carPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
            ((carPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));
        isActive = true;
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        isActive = false;

        //Reset rotation and size
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector2.one;

        this.gameObject.SetActive(false);
    }
}
