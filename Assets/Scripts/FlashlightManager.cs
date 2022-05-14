using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    //fog needs to be adjusted, just turn on from editor to enable

    float chargePercent = 100;

    public float dechargeRate = 1f;
    public float chargeRate = 50;

    public float lightIntensity = 10;
    //public float fogIntensity = 0.2f;

    public float minChargePercent = 5;

    bool charge = true;

    void Update()
    {
        //raise charge over time
        if (charge == true)
        {
            this.transform.GetChild(0).GetComponent<Light>().intensity = lightIntensity * (chargePercent / 100);
            //RenderSettings.fogDensity = fogIntensity / (chargePercent / 100);
            chargePercent += chargeRate * Time.deltaTime;

            if (chargePercent > 100)
            {
                charge = false;
            }

            //start coroutine for grace period?
        }

        //drop charge over time
        else if (chargePercent > minChargePercent)
        {
            this.transform.GetChild(0).GetComponent<Light>().intensity = lightIntensity * (chargePercent / 100);
            //RenderSettings.fogDensity = fogIntensity / (chargePercent / 100);
            chargePercent -= dechargeRate * Time.deltaTime;
        }
    }

    public void Refill()
    {
        //set to charge
        charge = true;
    }
}