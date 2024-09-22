using UnityEngine;

public static class HexCanvas
{
    public static Vector2 GetCanvasSize(this Canvas canvas)
    {
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();
        return rectTransform.sizeDelta;
    }
}