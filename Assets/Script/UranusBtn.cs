using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UranusBtn : MonoBehaviour
{
    public void onBtnClicked()
    {
        OriginScript.btnSelected = "Uranus";
        OriginScript.buttonClicked = true;
    }
}
