using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorer : MonoBehaviour
{
    public Text scoreText;
    int lives = 5;
    //int hits = 0;

    void Update() 
    {
        scoreText.text = "Lives: " + lives;
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Hit")
        {
            lives--;
            //hits++;
            //Debug.Log("You've bumped into a thing this many times: " + hits);
        }
    }
}
