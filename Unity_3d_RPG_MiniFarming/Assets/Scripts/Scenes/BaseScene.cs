using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Defines.Scenes SceneType { get; protected set; } = Defines.Scenes.Unknown;
    public string SceneName { get; protected set; } = "Unknown";

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("Common/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();
}
