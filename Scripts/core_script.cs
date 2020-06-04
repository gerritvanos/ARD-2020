using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenHardwareMonitor.Hardware;

public class core_script : MonoBehaviour
{
    public data_class data; //create object ob data_class
    public GameObject data_getter; //create refference to game object(needs to be filled in editor)
    public int core_number; //set the core number to get correct temp and load for this core
    // Start is called before the first frame update
    void Start()
    {
        //Set the data_class of the object to the internal data_class variable
        data_class data = data_getter.GetComponent<data_class>();
    }

    // Update is called once per frame
    void Update()
    {
        //get all the data from the data_getter object and send it to the shader
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Middle_temp", (float)data.middle); //Send the middle temp for calculating color value
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Temp_range", (float)data.temp_range); //Send the temp range for calculating color value
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Temp", (float)data.temps[core_number]); //Send current core temperature
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Load", (float)data.loads[core_number]); //Send current core Load 
    }
}


