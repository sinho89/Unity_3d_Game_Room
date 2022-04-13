using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private Transform _followTarget;

    public void SetFollowTarget(Transform followtarget) { _followTarget = followtarget; }

    private void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        this.transform.position = Vector3.Lerp(pos, _followTarget.position + (Vector3.up * 1.0f), 0.3f);
    }
}
