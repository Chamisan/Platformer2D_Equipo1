using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
	public Transform Player;

    void Update()
    {
	    transform.position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
    }
}
