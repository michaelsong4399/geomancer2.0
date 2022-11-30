using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsRecorder : MonoBehaviour
{
    public int numThrown = 0;
    public int numHit = 0;
    public float playerAccuracy = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void rockThrown ()
    {
        numThrown++;
        playerAccuracy = (float)numThrown / numHit;
    }
}
