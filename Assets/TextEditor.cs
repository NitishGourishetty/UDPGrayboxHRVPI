using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEditor : MonoBehaviour
{
    //[SerializeField] TextMeshPro textMeshPro;
    public TMPro.TMP_Text myText;
    public static double heartRate = 70;
    public static double sigmoidHeartRate = 0.5;
    public static double sigmoidIBI = 0.5; //remove this implement RMSDD
    public static double IBI = 600; //work on vairability
    public static bool isHeartRate = true;
    public static double difference = 0;
    public static double RMSDD = 0;
    //add arrays for RMSDD and heart beat, 0.5 second thing

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        System.Random rnd = new System.Random();
        //Add try catches here btw bub
        if (true)//if (sp.IsOpen)
        {
            // incomingData = int.Parse(sp.ReadLine());
            //we are sending 1 at a time
            if (isHeartRate)
            {
                //sigmoidHeartRate = Convert.ToDouble(sp.ReadLine());
                sigmoidHeartRate = rnd.NextDouble();

                Debug.Log(sigmoidHeartRate);
                //heartRate = Convert.ToDouble(sp.ReadLine());
                heartRate = rnd.Next(40, 120);
                Debug.Log(heartRate);
                isHeartRate = false;

            }
            else
            {
                //IBI = int.Parse(sp.ReadLine());
                sigmoidIBI = rnd.NextDouble();

                Debug.Log(sigmoidIBI);
                //heartRate = Convert.ToDouble(sp.ReadLine());
                IBI = rnd.Next(600, 1200);
                Debug.Log("IBI" + IBI);
                isHeartRate = true;

            }

        }

        myText.text = "heart rate: " + heartRate + "\nsigmoid heart rate: " + sigmoidHeartRate + "\nIBI: " + IBI; 
    }
}
