using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercuryBtn : MonoBehaviour
{
    public void onBtnClicked()
    {
        OriginScript.btnSelected = "Mercury";
        OriginScript.buttonClicked = true;
    }
}
