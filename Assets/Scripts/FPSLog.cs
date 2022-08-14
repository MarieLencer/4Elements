using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class FPSLog : MonoBehaviour
{
    private float fps;
    private List<float> fpsLog;
    private Dictionary<float, float> fpsLog2;

    public TMP_Text fpsText;
    // Start is called before the first frame update
    void Start()
    {
        fpsLog = new List<float>();
        fpsLog2 = new Dictionary<float, float>();
        //InvokeRepeating(nameof(LogFPS), 1,0.5f);
    }

    private void LogFPS()
    {
        /*fps = (1f / Time.unscaledDeltaTime);
        fpsText.text = fps + "fps";*/
    }

    private void Update()
    {
        fps = (1f / Time.unscaledDeltaTime);
        fpsText.text = fps + "fps";
        fpsLog.Add(fps);
        if (!fpsLog2.ContainsKey(Time.time))
        {
            fpsLog2.Add(Time.time, fps);
        }
    }

    public bool WriteData()
    {
        string path = Application.persistentDataPath + "/data_Perform_MaxQ2.csv";
        StreamWriter writer = new StreamWriter(path);
        writer.WriteLine("Timestamp;FPS");
        //foreach (var fpsCount in fpsLog)
        {
            //writer.WriteLine(fpsCount);
            
        }
        foreach (KeyValuePair<float,float> entry  in fpsLog2)
        {
            writer.WriteLine(entry.Key + ";" + entry.Value);
        }
        
        writer.Flush();
        writer.Close();
        Debug.Log(fpsLog.Count);
        return true;

    }
}
