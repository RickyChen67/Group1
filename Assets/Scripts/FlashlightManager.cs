using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    //fog and light range need to be added

    [SerializeField] private float chargePercent = 100f;
    public float minChargePercent = 5f;

    public float dechargeRate = 2.5f;
    public float dechargeDelay = 10f;
    public float chargeRate = 50f;

    public float lightIntensity = 5f;
    //public float lightRange = 5f;

    [SerializeField] private bool charging = true;
    [SerializeField] private bool paused = false;

    void Update()
    {
        if (paused == false)
        {
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

            //drop charge over time
            else if (chargePercent > minChargePercent)
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

    public void Refill()
    {

        charging = true;
    }

    IEnumerator GracePeriod()
    {
        paused = true;
        yield return new WaitForSeconds(dechargeDelay);

        charging = false;
        paused = false;
    }
}