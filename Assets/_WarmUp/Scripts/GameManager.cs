using System;
using UnityEngine;


//TODO: Make this a singleton, and persistent through scenes
public class GameManager : MonoBehaviour
{

    public static event EventHandler TimeTicked;

    public const float TICK_RATE = 0.5f; //in seconds
    public static int chestCoinValue = 2;
    public static int chaliceHealingAmount = 5;
    public static int trainingStatueTrainingAmount = 1;
    public static int trainingCost = 2;


    private float timeOfLastTick;


    void Update()
    {
        if (timeOfLastTick + TICK_RATE < Time.time)
        {
            TimeTicked?.Invoke(this, EventArgs.Empty);
            timeOfLastTick = Time.time;
        }
    }
}
