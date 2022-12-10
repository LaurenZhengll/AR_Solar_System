using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JupiterBtn : MonoBehaviour
{
    public void onBtnClicked()
    {
        OriginScript.btnSelected = "Jupiter";
        OriginScript.buttonClicked = true;
    }
}
