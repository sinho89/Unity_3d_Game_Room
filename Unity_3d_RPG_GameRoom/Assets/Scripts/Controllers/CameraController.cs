using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform _camPivot;
    private Transform _followTarget;

    private Vector3 _distFixPos;

    private float _rotAxisX;
    private float _rotAxisY;

    public void SetCamPivot(Transform campivot) 
    {
        _camPivot = campivot;
        transform.SetParent(campivot);
    }
    public void SetFollowTarget(Transform followtarget) { _followTarget = followtarget; }


    private void Start()
    {
        CamInit();
    }

    private void LateUpdate()
    {
        CamMove();
    }

    private void CamInit()
    {
        _distFixPos = new Vector3(0.0f, 2.0f, -5.0f);
        transform.localPosition = _distFixPos;
    }
    private void CamMove()
    {
        RaycastHit hit;

        int layerMask = 6;
        float dist = 0.0f;

        Vector3 delta = transform.position - _camPivot.position;
        Vector3 movePos;

        if (Physics.Raycast(_camPivot.position + (Vector3.up * 1.0f), delta, out hit, layerMask))
        {
            dist = (hit.point - _camPivot.position).magnitude * 0.8f;
            movePos = _camPivot.position + delta.normalized * dist;
        }
        else
            movePos = _camPivot.position + delta;

        if (transform.position.y < 0.5f)
            transform.position = Vector3.Lerp(transform.position, new Vector3(movePos.x, 0.5f, movePos.z), 0.4f);
        else
            transform.position = Vector3.Lerp(transform.position, movePos, 0.4f);

        transform.LookAt(_camPivot.position);

    }

}
