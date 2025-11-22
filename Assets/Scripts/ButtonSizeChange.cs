using UnityEngine;

public class ButtonSizeChange : MonoBehaviour
{
    [Range(1, 2)]
    public float sizeDifference = 1.1f;

    private Vector2 originalSize;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.localScale;
    }
    public void MakeBigger()
    {
        Debug.Log("Mouse Enter");
        rectTransform.localScale = originalSize * sizeDifference;
    }
    public void MakeSmaller()
    {
        rectTransform.localScale = originalSize;
    }
}

