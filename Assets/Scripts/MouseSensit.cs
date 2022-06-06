using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensit : MonoBehaviour
{

    public Slider slider;
    public MouseLook mouseLook;

    public void changeSens()
    {
        int newSens = (int)slider.value;
        mouseLook.ChangeSens(newSens);
    }
    
}
