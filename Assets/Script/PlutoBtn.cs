using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlutoBtn : MonoBehaviour
{    
    public void onBtnClicked()
    {
        OriginScript.btnSelected = "Pluto";
        OriginScript.buttonClicked = true;
    }
}
