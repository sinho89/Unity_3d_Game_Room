using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_World : UI_Base
{
    enum WorldUIObjects
    {
        WU_Interaction,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(WorldUIObjects));
    }
}
