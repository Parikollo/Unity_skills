using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresScript : CubesScript
{


    public override void Action(int reDigit)           // can override virtual methods from parent class
    {
        base.Action(reDigit);
        SetColor();
    }
}
