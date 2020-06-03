using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenHardwareMonitor.Hardware;

public class data_class : MonoBehaviour
{
    public float[] temps;
    public float[] loads;
    public float max_temp;
    public float min_temp;
    public float middle;
    public float temp_range;

    private Computer thisComputer;
    // Start is called before the first frame update
    void Start()
    {
        thisComputer = new Computer(){CPUEnabled = true};
        thisComputer.Open();
        max_temp = 90;
        min_temp = 30;
        middle = min_temp + (max_temp-min_temp)/2;
        temp_range = max_temp - min_temp;
        temps = new float[4];
        loads = new float[4];
        InvokeRepeating("update_data", 1.0f, 0.2f);
    }

    // Update is called once per frame
    void update_data()
    {
        foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            // if 1 is present this is core#1 etc. 0 is package
                            // this is a quadcore cpu so four temp measurements
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
                        if (sensor.SensorType == SensorType.Load)
                        {
                            // if 1 is present this is core#1 etc. 0 is package
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
