using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Interaction : UI_World
{
    public override void Init()
    {
        gameObject.SetActive(false);        
    }
    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position - Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
    }
}
