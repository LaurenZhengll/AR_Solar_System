using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaturnBtn : MonoBehaviour
{
    public void onBtnClicked()
    {
        OriginScript.btnSelected = "Saturn";
        OriginScript.buttonClicked = true;
    }
}
