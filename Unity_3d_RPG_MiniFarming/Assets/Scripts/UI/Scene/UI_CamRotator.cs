using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CamRotator : UI_Scene
{

    public override void Init()
    {
        BindEvent(gameObject, OnDrag, Defines.UIEvents.Drag);
        BindEvent(gameObject, OnBeginDrag, Defines.UIEvents.BeginDrag);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Managers.Input._dragX = eventData.position.x;
        Managers.Input._dragY = eventData.position.y;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Managers.Input._beginX = eventData.position.x;
        Managers.Input._beginY = eventData.position.y;

        Managers.Input._tempAngleX = Managers.Input._originAngleX;
        Managers.Input._tempAngleY = Managers.Input._originAngleY;
    }
}
