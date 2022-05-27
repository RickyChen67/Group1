using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] private float chargePercent = 100f;
    public float minChargePercent = 5f;

    public float dechargeRate = 2.5f;
    public float dechargeDelay = 10f;
    public float chargeRate = 50f;

    public float lightIntensity = 5f;
    //public float lightRange = 5f;

    [SerializeField] private bool lightOn = true;
    [SerializeField] private bool charging = false;
    [SerializeField] private bool graceActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) == true)
        {
            Switch();
            //play sound?
        }

        //raise charge over time
        if (charging == true)
        {
            if (chargePercent > 100)
            {
                chargePercent = 100;
                StartCoroutine(GracePeriod()); //start grace period when full
            }
            else
            {
                chargePercent += chargeRate * Time.deltaTime;
            }
        }

        if (graceActive == false && lightOn == true)
        {
            //drop charge over time
            if (chargePercent > minChargePercent)
            {
                chargePercent -= dechargeRate * Time.deltaTime;
            }
            else
            {
                chargePercent = minChargePercent;
            }

            this.transform.GetChild(0).GetComponent<Light>().intensity = lightIntensity * (chargePercent / 100);
            //this.transform.GetChild(0).GetComponent<Light>().range = Mathf.Round(lightRange * (chargePercent / 100);
        }
    }


    public void Switch()
    {
        if (lightOn == true)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            lightOn = false;
        }
        else if (lightOn == false)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            lightOn = true;
        }
    }


    public void Refill()
    {
        charging = true;
    }


    IEnumerator GracePeriod()
    {
        graceActive = true;
        yield return new WaitForSeconds(dechargeDelay);
        
        charging = false;
        graceActive = false;
    }
}