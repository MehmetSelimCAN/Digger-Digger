using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonShrink : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private bool shrinking;
    private float shrinkSpeed = 1.5f;
    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();    
    }

    private void Update() {
        if (shrinking && rectTransform.sizeDelta.x < 60) {
            rectTransform.sizeDelta += new Vector2(shrinkSpeed, shrinkSpeed);
        }

        else if (!shrinking && rectTransform.sizeDelta.x > 50) {
            rectTransform.sizeDelta -= new Vector2(shrinkSpeed, shrinkSpeed);
        }
    }


    public void OnPointerEnter(PointerEventData eventData) {
        shrinking = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        shrinking = false;
    }
}
