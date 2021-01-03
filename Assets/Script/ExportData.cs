using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.IO;

public class ExportData : MonoBehaviour
{
    private static Text errorLabel;
    public static void exportData(List<string> data, string filepath)
    {
        errorLabel = GameObject.Find("ExportErrorMessage").GetComponent<Text>();
        try
        {
                errorLabel.text = "";
                System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath);
                foreach (String line in data)
                    file.WriteLine(line);

                file.Close();
                System.Diagnostics.Process.Start(filepath);

           

        }
        catch (Exception e)
        {
            errorLabel.text = "Ungültiger Dateipfad";
            throw (e);
        }

            
    }


}
