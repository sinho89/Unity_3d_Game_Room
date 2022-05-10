using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEnterLight : MonoBehaviour
{
    private Transform _InteractionUI;
    private Transform _EnterButtonUI;
        
    private void Start()
    {
        _InteractionUI = transform.Find("UI_Interaction");
        _EnterButtonUI = Managers.UI.ShowSceneUI<UI_EnterButton>().transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        _InteractionUI.gameObject.SetActive(true);
        _EnterButtonUI.gameObject.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
        _InteractionUI.gameObject.SetActive(false);
        _EnterButtonUI.gameObject.SetActive(false);
    }
}
