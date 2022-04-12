using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEnterLight : MonoBehaviour
{
    private Transform _InteractionUI;
        
    private void Start()
    {
        _InteractionUI = transform.Find("UI_Interaction");
    }
    private void OnTriggerEnter(Collider other)
    {
        _InteractionUI.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _InteractionUI.gameObject.SetActive(false);
    }
}
