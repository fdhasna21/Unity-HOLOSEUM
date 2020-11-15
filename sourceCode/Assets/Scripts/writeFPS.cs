using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tayx.Graphy;
using System.Text;
using System.IO;
using System;

public class writeFPS : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();
    public DM_MainScene dM_MainScene;
    public GestureChecker gestureChecker;
    public GraphyManager graphyManager;
    public int id=0, activatedObject;
    public float currFPS, minFPS, maxFPS, avgFPS;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    void Start()
    {
        string filePath = getPath();
        StreamWriter writer = System.IO.File.AppendText(filePath);
        writer.WriteLine("ID,Object,currFPS,avgFPS,minFPS,maxFPS,ExploreActive,ZoomActive,IsPlaying,IsPrevious,IsNext");
        writer.Flush();
        writer.Close();
    }

    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            getData();
        }

    }

    public void getData()
    {
        id++;
        currFPS = graphyManager.CurrentFPS;
        avgFPS = graphyManager.AverageFPS;
        minFPS = graphyManager.MinFPS;
        maxFPS = graphyManager.MaxFPS;
        activatedObject = dM_MainScene.selectedModel;

        string filePath = getPath();
        StreamWriter writer = System.IO.File.AppendText(filePath);
        writer.WriteLine(id + "," + activatedObject + "," + currFPS + "," + avgFPS + "," + minFPS + "," + maxFPS +  "," + gestureChecker.ExploreActive + "," + gestureChecker.ZoomActive + "," + dM_MainScene.IsPlaying + "," + dM_MainScene.IsPrevious + "," + dM_MainScene.IsNext);
        writer.Flush();
        writer.Close();
    }

    private string getPath()
    {
        #if UNITY_EDITOR
                return Application.dataPath + "/CSV/" + "coba.csv";
        #elif UNITY_ANDROID
                return Application.persistentDataPath+"coba.csv";
        #elif UNITY_IPHONE
                return Application.persistentDataPath+"/"+"coba.csv";
        #else
                return Application.dataPath +"/"+"coba.csv";
        #endif
    }
}


