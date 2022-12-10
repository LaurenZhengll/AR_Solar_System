using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeptuneBtn : MonoBehaviour
{   
    public void onBtnClicked()
    {
        OriginScript.btnSelected = "Neptune";
        OriginScript.buttonClicked = true;
    }
}
