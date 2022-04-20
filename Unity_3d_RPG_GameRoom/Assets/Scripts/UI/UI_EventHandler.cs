using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, 
                                IPointerUpHandler, IBeginDragHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnClickDownHandler = null;
    public Action<PointerEventData> OnClickUpHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragHandler != null)
            OnBeginDragHandler.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnClickDownHandler != null)
            OnClickDownHandler.Invoke(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnClickUpHandler != null)
            OnClickUpHandler.Invoke(eventData);
    }


}
