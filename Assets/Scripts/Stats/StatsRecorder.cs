using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsRecorder : MonoBehaviour
{
    private int numThrown = 0;
    private int numHit = 0;
    private int score = 0;
    private int highScore = 0;

    void rockThrown ()
    {
        numThrown++;
    }

    void hit()
    {
        numHit++;
    }

    void increaseScore(int points)
    {
        score += points;
    }
}
