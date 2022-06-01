using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertMouseButoon : MonoBehaviour
{

    public MouseLook mouseLook;
    public Toggle toggle;

    public void Invert()
    {
        Debug.Log("Button clicked");
        mouseLook.InvertMouse();
        toggle.isOn = !toggle.isOn;
    }
}
