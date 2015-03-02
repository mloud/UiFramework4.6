using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowBackground : Ui.Component
{
    [SerializeField]
    private Image image;

    public void SetAlpha(float alpha)
    {
        var color = image.color;
        color.a = alpha;
        image.color = color;

        image.enabled = alpha > 0;
    }


    public override void SetParent(RectTransform parent)
    {
        transform.SetParent(parent);
        transform.SetAsFirstSibling(); // = move to background
        transform.localPosition = Vector3.zero;
        (transform as RectTransform).offsetMin = Vector2.zero;
        (transform as RectTransform).offsetMax = Vector2.one;
    }
}
