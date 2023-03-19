using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public float ScoreNum = 0;
    public GameObject Kiwi;
    public GameObject Apple;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Kiwi"))
        {
            ScoreNum += 300;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Apple"))
        {
            ScoreNum += 100;
            Destroy(other.gameObject);
        }
    }

    
}
