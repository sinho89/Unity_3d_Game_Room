using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoginButton : UI_Scene
{
    public override void Init()
    {
    }

    public void OnPlayButton()
    {
        Managers.Scene.LoadScene(Defines.Scenes.Lobby);
    }
    public void OnExitButton()
    {
        Managers.Clear();
        Application.Quit();
    }
}
