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
        SceneSetting();
        ObjectInit();
    }

    protected virtual void Init()
    {
        // 게임에 필수적으로 필요한 EventSystem을 모든 Scene에서 생성 처리
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("Common/EventSystem").name = "@EventSystem";
    }

    // 각 Child Scene들에 필요한 공통적인 매서드 추상화
    protected abstract void SceneSetting();
    protected abstract void ObjectInit();
    public abstract void Clear();
}
