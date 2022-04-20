using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CamRotator : UI_Scene
{
    Transform _camPivot;

    private float _camRotateSpeed = 0.0f;
    public void SetCamPivot(Transform camPivot) { _camPivot = camPivot; }

    public override void Init()
    {
        BindEvent(gameObject, OnDrag, Defines.UIEvents.Drag);
        BindEvent(gameObject, OnBeginDrag, Defines.UIEvents.BeginDrag);

        Managers.Input._originAngleX = _camPivot.rotation.eulerAngles.x;
        Managers.Input._originAngleY = _camPivot.rotation.eulerAngles.y;

        _camRotateSpeed = 5.0f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Managers.Input._dragX = eventData.position.x;
        Managers.Input._dragY = eventData.position.y;

        float yAngle = Managers.Input._tempAngleY + (Managers.Input.CamRotateInput.y - Managers.Input.CamRotateInput.x) * 180 / Screen.width * _camRotateSpeed;
        float xAngle = Managers.Input._tempAngleX - (Managers.Input.CamRotateInput.w - Managers.Input.CamRotateInput.z) * 90 / Screen.height * _camRotateSpeed;

        if (xAngle > 40) xAngle = 40;
        if (xAngle < -5) xAngle = -5;

        _camPivot.rotation = Quaternion.Euler(xAngle, yAngle, 0.0f);

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Managers.Input._beginX = eventData.position.x;
        Managers.Input._beginY = eventData.position.y;

        Managers.Input._tempAngleX = Managers.Input._originAngleX;
        Managers.Input._tempAngleY = Managers.Input._originAngleY;
    }
}
