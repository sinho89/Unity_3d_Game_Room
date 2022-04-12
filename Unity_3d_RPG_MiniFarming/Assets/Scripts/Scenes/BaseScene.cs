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

    protected virtual void SceneSetting()
    {

    }

    protected virtual void CommonSetting()
    {
        //Player Setting
        GameObject player = Managers.Actor.Spawn(Defines.Actors.Player, $"{SceneName}/Player/Player");

        //Envis Setting
        GameObject envis = Managers.Actor.Spawn(Defines.Actors.Envis, $"{SceneName}/Envis/Envis");

        Transform EnterLights = envis.transform.Find("EnterLights");
        foreach (Transform child in EnterLights)
            Managers.UI.MakeWorldSpaceUI<UI_Interaction>(child.transform);

        //Camera Setting
        GameObject centralAxis = new GameObject { name = "@Camera" };
        CameraController cc = Camera.main.gameObject.GetOrAddComponent<CameraController>();
        cc.SetCentralAxis(centralAxis.transform);
        cc.SetFollowTarget(player.transform);
    }

    public abstract void Clear();
}
