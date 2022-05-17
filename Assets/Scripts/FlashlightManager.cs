using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    //fog and light range need to be added

    [SerializeField] float chargePercent = 100f;
    public float minChargePercent = 5f;

    public float dechargeRate = 2.5f;
    public float dechargeDelay = 10f;
    public float chargeRate = 50f;

    public float lightIntensity = 5f;
    //public float lightRange = 5f;
    //public float fogIntensity = 0.2f;

    [SerializeField] bool charge = true;
    [SerializeField] bool grace = false;

    void Update()
    {
        if (grace == false)
        {
            //raise charge over time
            if (charge == true)
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
            //RenderSettings.fogDensity = fogIntensity * (chargePercent / 100);
        }
    }

    public void Refill()
    {

        charge = true;
    }

    IEnumerator GracePeriod()
    {
        grace = true;
        yield return new WaitForSeconds(dechargeDelay);

        charge = false;
        grace = false;
    }
}