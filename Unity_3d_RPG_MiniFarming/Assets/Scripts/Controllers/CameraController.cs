using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _centralAxis;
    private Transform _followTarget;

    private float _camSpeed;
    private float _wheelSpeed;

    private float _rotAxisX;
    private float _rotAxisY;

    private float _followDistX;
    private float _followDistY;

    public void SetCentralAxis(Transform centralaxis) 
    { 
        _centralAxis = centralaxis;
        transform.SetParent(centralaxis);
    }
    public void SetFollowTarget(Transform followtarget) { _followTarget = followtarget; }


    private void Start()
    {
        Init();
    }

    private void LateUpdate()
    {
        CamZoom();
        CamRotate();
        CamMove();
    }

    private void Init()
    {
        _camSpeed = 5.0f;
        _wheelSpeed = 2.0f;
        _followDistX = 0.3f;
        _followDistY = 1.3f;
    }

    private void CamZoom()
    {
        transform.localPosition = new Vector3(0, 0, Managers.Input.MouseWheelValue * _wheelSpeed);
    }

    private void CamRotate()
    {
        _rotAxisX = _centralAxis.rotation.x + Managers.Input.MouseRotInputY;
        _rotAxisY = _centralAxis.rotation.y + Managers.Input.MouseRotInputX;

        _centralAxis.rotation = Quaternion.Euler(new Vector3(_rotAxisX, _rotAxisY, 0) * _camSpeed);
    }

    private void CamMove()
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
}
