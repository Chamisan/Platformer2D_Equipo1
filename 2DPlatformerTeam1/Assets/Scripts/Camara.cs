using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
	public Transform player;

    void Update()
    {
	    transform.position = new Vector3(player.position.x, player.position.y+2, transform.position.z); //Incluí que lo siga en y con un +2 para que no se vea tan centrado
    }
}
