using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenHardwareMonitor.Hardware;

public class data_class : MonoBehaviour
{
    public float[] temps;       //Array to store per core temperatures (public so it is easy to monitor in editor)
    public float[] loads;       //Array to store per core load (public so it is easy to monitor in editor)
    private float max_temp;     //Value to store the maximum temp of the cpu set by this script(at this temperature the whole cube is red)
    private float min_temp;     //Value to store the minimum temp of the cpu set by this script(at this temperature the whole cube is green)
    public float middle;        //Value to store the middle value between the min/max
    public float temp_range;    //The range of temperature between min/max (for example if min=30 and max=40 the range =10)

    private Computer thisComputer; //Computer object used by OpenHardwareMonitor
    // Start is called before the first frame update
    void Start()
    {
        thisComputer = new Computer(){CPUEnabled = true}; //Construct the Computer object with only the CPU enabled since no other sensors are used
        thisComputer.Open();
        //Set min/max and calculate middle/temp_range
        max_temp = 90;
        min_temp = 30;
        middle = min_temp + (max_temp-min_temp)/2;
        temp_range = max_temp - min_temp;

        //Init the temps/floats array with empty arrays
        temps = new float[4];
        loads = new float[4];
        //Start the update data function to repeat the update_data every 200ms
        InvokeRepeating("update_data", 1.0f, 0.2f);
    }

    // Update is called once per frame
    void update_data()
    {
        //Loop thourgh all hardware items
        foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU) //Check if the curent item is CPU
                {
                    //Update the values of the hardwareItem 
                    hardwareItem.Update(); 
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();
                    // Loop Through all sensors
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature) //Check if sensor is temperature sensor
                        {
                            // if 1 is present this is core#1 etc. 0 is package
                            // this is a quadcore cpu so four temp measurements
                            // store measurements in array
                            if (sensor.Name.Contains("1")){
                                temps[0] = sensor.Value.Value;
                            }
                            else if (sensor.Name.Contains("2")){
                                temps[1] = sensor.Value.Value;
                            }
                            else if (sensor.Name.Contains("3")){
                                temps[2] = sensor.Value.Value;
                            }
                            else if (sensor.Name.Contains("4")){
                                temps[3] = sensor.Value.Value;
                            }
                        }
                        if (sensor.SensorType == SensorType.Load) //Check if sensor is Load sensor
                        {
                            // if 1 is present this is core#1 etc. 0 is package
                            // store measurements in array
                            if (sensor.Name.Contains("1")){
                                loads[0] = sensor.Value.Value;
                            }
                            else if (sensor.Name.Contains("2")){
                                loads[1] = sensor.Value.Value;
                            }
                            else if (sensor.Name.Contains("3")){
                                loads[2] = sensor.Value.Value;
                            }
                            else if (sensor.Name.Contains("4")){
                                loads[3] = sensor.Value.Value;
                            }
                        }
                    }
                }
            }
    }
}
