using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class TextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    bool isHovering;
    bool isPressed;
    bool isReleased;

    TextMeshProUGUI tmp;

    [ColorUsageAttribute(true, true)]
    public Color color;

    [ColorUsageAttribute(true, true)]
    public Color hoverColor;

    [ColorUsageAttribute(true, true)]
    public Color pressedColor;

    public float colorFadeSpeed;

    public Vector2 size;
    public Vector2 hoverSize;
    public Vector2 pressedSize;

    public float fontSize;
    public float hoverFontSize;
    public float pressedFontSize;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();

        tmp.faceColor = color;
        size = tmp.rectTransform.sizeDelta;
        fontSize = tmp.fontSize;
    }

    private void OnEnable()
    {
        isHovering = false;
        isPressed = false;
        tmp.faceColor = color;
    }

    private void OnDisable()
    {
        isHovering = false;
        isPressed = false;
        tmp.faceColor = color;
    }

    void Update()
    {
        if (isHovering) { OnHover(); } else if(!isHovering) { OnExit(); }
        if (isPressed) { OnClick(); } else if (isReleased) { OnRelease(); }
    }
    #region Custom Functions
    void FadeToColor(Color targetColor) 
    {
        tmp.faceColor = (Color)Vector4.MoveTowards((Color)tmp.faceColor, targetColor, colorFadeSpeed * Time.deltaTime);
    }
    #endregion

    #region PointerFunctions
    void OnHover() 
    {
        if (isPressed) return;
        FadeToColor(hoverColor);
    }

    void OnExit() 
    {
        if (isPressed) return;
        FadeToColor(color);
    }

    void OnClick() 
    {
        FadeToColor(pressedColor);
    }

    void OnRelease() 
    {
        if (isHovering)
        {
            FadeToColor(hoverColor);
        }
        else 
        {
            FadeToColor(color);
        }

        isReleased = false;
    }
    #endregion

    #region Pointer Events
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isReleased = isHovering;
        isPressed = false;
    }
    #endregion
}
