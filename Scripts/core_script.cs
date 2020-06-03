using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenHardwareMonitor.Hardware;

public class core_script : MonoBehaviour
{
    public data_class data;
    public GameObject data_getter;
    public int core_number;
    // Start is called before the first frame update
    void Start()
    {
        data_class data = data_getter.GetComponent<data_class>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Middle_temp", (float)data.middle);
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Temp_range", (float)data.temp_range);
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Temp", (float)data.temps[core_number]);
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Load", (float)data.loads[core_number]);

    }
}


