//#define PC_BASE_CONTROLLER
#define MOBILE_JOYSTICK_BASE_CONTROLLER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform _camPivot;
    private Transform _followTarget;

    private float _camRotateSpeed;
    private float _wheelSpeed;

    private float _rotAxisX;
    private float _rotAxisY;

    private float _followDistX;
    private float _followDistY;

    public void SetCentralAxis(Transform centralaxis) 
    {
        _camPivot = centralaxis;
        transform.SetParent(centralaxis);
    }
    public void SetFollowTarget(Transform followtarget) { _followTarget = followtarget; }


    private void Start()
    {
#if PC_BASE_CONTROLLER
        PcBaseCamInit();
#else
        MobileBaseCamInit();

#endif
    }

    private void LateUpdate()
    {
#if PC_BASE_CONTROLLER
        PcBaseCamZoom();
        PcBaseCamRotate();
        PcBaseCamMove();
#else
        MobilcBaseCamZoom();
        MobilcBaseCamRotate();
        MobilcBaseCamMove();
#endif
    }

#if PC_BASE_CONTROLLER
    
    private void PcBaseCamInit()
    {
        _camSpeed = 5.0f;
        _wheelSpeed = 2.0f;
        _followDistX = 0.3f;
        _followDistY = 1.3f;
    }
    private void PcBaseCamMove()
    {
        RaycastHit hit;

        int layerMask = 6;
        float dist = 0.0f;

        Vector3 delta = transform.position - _followTarget.position;

        if (Physics.Raycast(_followTarget.position + (Vector3.up * 1.0f), delta, out hit, layerMask))
        {
            dist = (hit.point - _followTarget.position).magnitude * 0.8f;
            transform.position = _followTarget.position + delta.normalized * dist ;
        }
        else
            transform.position = _followTarget.position + delta;

        _centralAxis.position = _followTarget.position + (Vector3.up * _followDistY) + (_followTarget.transform.right * _followDistX);
    }

    private void PcBaseCamRotate()
    {
        _rotAxisX = _centralAxis.rotation.x + Managers.Input.MouseRotInputY;
        _rotAxisY = _centralAxis.rotation.y + Managers.Input.MouseRotInputX;

        _centralAxis.rotation = Quaternion.Euler(new Vector3(_rotAxisX, _rotAxisY, 0) * _camRotateSpeed);
    }

    private void PcBaseCamZoom()
    {
        transform.localPosition = new Vector3(0, 0, Managers.Input.MouseWheelValue * _wheelSpeed);
    }

        
#else
    private void MobileBaseCamInit()
    {
        _camRotateSpeed = 5.0f;
        _wheelSpeed = -1.0f;
        _followDistX = 0.3f;
        _followDistY = 1.3f;

        transform.position = new Vector3(0.0f, 3.0f, -5.0f);

        Managers.Input._originAngleX = _camPivot.rotation.eulerAngles.x;
        Managers.Input._originAngleY = _camPivot.rotation.eulerAngles.y;
    }
    private void MobilcBaseCamMove()
    {
        RaycastHit hit;

        int layerMask = 6;
        float dist = 0.0f;

        Vector3 delta = transform.position - _followTarget.position;

        if (Physics.Raycast(_followTarget.position + (Vector3.up * 1.0f), delta, out hit, layerMask))
        {
            dist = (hit.point - _followTarget.position).magnitude * 0.8f;
            transform.position = Vector3.Lerp(transform.position, _followTarget.position + delta.normalized * dist, 0.4f);
        }
        else
            transform.position = Vector3.Lerp(transform.position, _followTarget.position + delta, 0.4f);
    }
    private void MobilcBaseCamZoom()
    {
        transform.localPosition = new Vector3(0, 0, Managers.Input._wheelValue * _wheelSpeed);
    }
    private void MobilcBaseCamRotate()
    {
        float yAngle = Managers.Input._tempAngleY + (Managers.Input.CamRotateInput.y - Managers.Input.CamRotateInput.x) * 180 / Screen.width * _camRotateSpeed;
        float xAngle = Managers.Input._tempAngleX - (Managers.Input.CamRotateInput.w - Managers.Input.CamRotateInput.z) * 90 / Screen.height * _camRotateSpeed;

        if (xAngle > 30) xAngle = 30;
        if (xAngle < -60) xAngle = -60;

        _camPivot.rotation = Quaternion.Euler(xAngle, yAngle, 0.0f);
    }

#endif
}
