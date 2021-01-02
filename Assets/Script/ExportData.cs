using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ExportData : MonoBehaviour
{
    public static void exportData(List<string> data, string filepath)
    {
        try
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath);
            foreach (String line in data)
                file.WriteLine(line);

            file.Close();
            System.Diagnostics.Process.Start(filepath);

        }
        catch (Exception e)
        {
            throw(e);
        }


            
    }
}
