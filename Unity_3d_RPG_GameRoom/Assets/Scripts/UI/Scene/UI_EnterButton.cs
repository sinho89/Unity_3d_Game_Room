using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EnterButton : UI_Scene
{
    public override void Init()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        //Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
