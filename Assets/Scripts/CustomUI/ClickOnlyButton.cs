using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System;

public class ClickOnlyButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler  {

    public UnityEvent onClick;
    [SerializeField]
    private bool highlight = true;

    public void OnPointerClick(PointerEventData eventData) {
        pointerEventDebug(eventData);
        if (isClickBlocked(eventData)) { return; }
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (highlight) {
            Image im = GetComponent<Image>();
            im.color = Color.gray;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (highlight) {
            Image im = GetComponent<Image>();
            im.color = Color.white;
        }
    }

    private void pointerEventDebug(PointerEventData pd) {
        print("pointer press: " +pd.pointerPress);
        for(int i=0; i < pd.hovered.Count; ++i) {
            print("hovered " + pd.hovered[i].name + " : " + i);
        }
        print("raw press: " + pd.rawPointerPress);
    }

    private bool isClickBlocked(PointerEventData eventData) {
        return false; 
// &&&*********************************
        if (eventData.hovered.Count < 1) { return true; }
        if(eventData.hovered[0] == gameObject) { return false; }
        for(int i=0; i < eventData.hovered.Count; ++i) {
            print(eventData.hovered[i].name + " : " + i);
        }
        foreach(Transform ch in transform) {
            if(eventData.hovered[0] == ch.gameObject) { return false; }
        }
        print("blocked by: " + eventData.hovered[0].name);
        return true;
    }

    
}
