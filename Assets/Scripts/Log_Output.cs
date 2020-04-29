using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Log_Output : MonoBehaviour
{
    static string fileName = "", fileNameExt = "/testsubject0.txt";
    static StreamWriter writer;
    int ext;

    void Awake(){

        // create new file for subject
        fileName = Application.persistentDataPath + fileNameExt;
        while(File.Exists(fileName)){
            string intstr = "";
            for(int i = 0; i < fileNameExt.Length; i++)
                if(char.IsDigit(fileNameExt,i)) intstr += fileNameExt.Substring(i,1);
            ext = int.Parse(intstr)+1;
            fileNameExt = "/testsubject" + ext.ToString() + ".txt";
            fileName = Application.persistentDataPath + fileNameExt;
        }
        
        // determine order for cue on/off
        if(ext % 2 == 0) Hot_Cold.evenOn = true;

    }

    public static void writeTime(string objname, float time, bool cueAvailable){

        writer = new StreamWriter(fileName,true);
        writer.WriteLine(objname + " found in " + time.ToString() + 
            " (seconds) ; cue available: " + cueAvailable.ToString());
        writer.Close();

    }
}
