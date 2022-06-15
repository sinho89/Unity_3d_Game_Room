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
        // 이벤트 바인딩 (드래그와 드래그시작 이벤트)
        BindEvent(gameObject, OnDrag, Defines.UIEvents.Drag);
        BindEvent(gameObject, OnBeginDrag, Defines.UIEvents.BeginDrag);
        
        if (_camPivot != null)
        {
            Managers.Input._originAngleX = _camPivot.rotation.eulerAngles.x;
            Managers.Input._originAngleY = _camPivot.rotation.eulerAngles.y;
        }

        // Angle & Speed Setting

        _camRotateSpeed = 5.0f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중 스크린좌표 저장 (_dragX, _dragY)
        Managers.Input._dragX = eventData.position.x;
        Managers.Input._dragY = eventData.position.y;

        // 드래그 시작 좌표에서부터 드래그로 움직인 크기 ((_dragX -_beginX),(_dragY - _beginY)) * (디바이스 크기 변화량/스크린 가로,세로 * 회전속도)
        // 위에 결과 값에 회전축 별로 연산 
        // (Y축 => 오른쪽 드래깅(x) => yAngle값 증가 => +연산)
        // (X축 => 아래쪽 드래깅(y) => xAngle값 감소 => -연산)
        float yAngle = Managers.Input._tempAngleY + (Managers.Input.CamRotateInput.y - Managers.Input.CamRotateInput.x) * 180 / Screen.width * _camRotateSpeed;
        float xAngle = Managers.Input._tempAngleX - (Managers.Input.CamRotateInput.w - Managers.Input.CamRotateInput.z) * 90 / Screen.height * _camRotateSpeed;

        // X축으로 회전시킬수 있는 범위 설정
        if (xAngle > 40) xAngle = 40;
        if (xAngle < -5) xAngle = -5;

        // _camPivot Rotation 최종 설정

        if (_camPivot != null)
        {
            _camPivot.rotation = Quaternion.Euler(xAngle, yAngle, 0.0f);
        }

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시 스크린좌표 저장 (_beginX, _beginY)
        Managers.Input._beginX = eventData.position.x;
        Managers.Input._beginY = eventData.position.y;

        // 드래그 시작 시 _camPivot의 rotation값 저장 (_tempAngleX, _tempAngleY)
        Managers.Input._tempAngleX = Managers.Input._originAngleX;
        Managers.Input._tempAngleY = Managers.Input._originAngleY;
    }
}
