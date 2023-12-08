using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBtn : BaseBtn
{
    public override void LoadComponent()
    {
    }

    public override void OnClick()
    {
        Application.Quit();
    }
}
