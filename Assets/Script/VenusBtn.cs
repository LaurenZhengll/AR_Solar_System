using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusBtn : MonoBehaviour
{
    public void onBtnClicked()
    {
        OriginScript.btnSelected = "Venus";
        OriginScript.buttonClicked = true;
    }
}
