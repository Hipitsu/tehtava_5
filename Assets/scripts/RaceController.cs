using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceController : MonoBehaviour

{
    public int lapsInRace;
    public Text LapInfoText;
    public Text CheckpointInfoText;
    public Text RaceOverText;

    private int nextCheckpointNumber;
    private int checkpointCount;
    private int lapCount;
    private float lapStartTime;
    private bool isRaceActive;

    private List<float> lapTimes = new List<float>();
    private Checkpoint activeCheckpoint;
    
    void Start()
    {
        isRaceActive = true;
        lapStartTime = Time.time;
        nextCheckpointNumber = 0;
        lapCount = 0;
        checkpointCount = this.transform.childCount;

        for (int i = 0; i < checkpointCount; i++)
        {
            Checkpoint cp = transform.GetChild(i).GetComponent<Checkpoint>();
            cp.checkpointNumber = i;
            cp.isActiveCheckpoint = false;
        }

        StartRace();
        
    } // Start

    void Update()
    {
        if (isRaceActive)
        {
            LapInfoText.text = TimeParser(Time.time - lapStartTime);
            CheckpointInfoText.text = ("CHECKPOINT " + (nextCheckpointNumber + 1) + " / " + checkpointCount + "\nLAP " + (lapCount + 1) + " / " + lapsInRace);
        }
        else
        {
            LapInfoText.text = "";
            CheckpointInfoText.text = "";
            RaceOverText.color = Color.HSVToRGB(Mathf.Abs(Mathf.Sin(Time.time)), 1, 1);
        }
        
    } // Update

    public void StartRace()
    {
        activeCheckpoint = transform.GetChild(nextCheckpointNumber).GetComponent<Checkpoint>();
        activeCheckpoint.isActiveCheckpoint = true;
        lapStartTime = Time.time;
    } // StartRace

    public void CheckpointPassed()
    {
        activeCheckpoint.isActiveCheckpoint = false;
        nextCheckpointNumber++;
        if (nextCheckpointNumber < checkpointCount)
        {
            activeCheckpoint = transform.GetChild(nextCheckpointNumber).GetComponent<Checkpoint>();
            activeCheckpoint.isActiveCheckpoint = true;
        }
        else
        {
            lapTimes.Add(Time.time - lapStartTime);
            lapCount++;

            lapStartTime = Time.time;
            nextCheckpointNumber = 0;

            if (lapCount < lapsInRace)
            {
                activeCheckpoint = transform.GetChild(nextCheckpointNumber).GetComponent<Checkpoint>();
                activeCheckpoint.isActiveCheckpoint = true;

            }
            else
            {
                isRaceActive = false;
                float raceTotalTime = 0.0f;
                float fastestLapTime = lapTimes[0];
                for (int i = 0; i < lapsInRace; i++)
                {
                    if (lapTimes[i] < fastestLapTime)
                    {
                        fastestLapTime = lapTimes[i];
                    }

                    raceTotalTime += lapTimes[i];
                }
                RaceOverText.text = "RACE COMPLETE! \n \nTotal Time:" + TimeParser(raceTotalTime) + "\nBest Lap: " + TimeParser(fastestLapTime);
            }
        }
    } // CheckpointPassed

    private string TimeParser(float time)
    {
        float minutes = Mathf.Floor((time) / 60);
        float seconds = Mathf.Floor((time) % 60);
        float msecs = Mathf.Floor((time) * 100) % 100;
        return (minutes.ToString() + ":" + seconds.ToString("00") + ":" + msecs.ToString("00"));
    }
}
