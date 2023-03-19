using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; //Para la variable TMP del canvas

public class Collectables : MonoBehaviour
{
    //Corregí a minúsculas sus iniciales
    public int scoreNum = 0; //Lo tuve que pasar a entero para poderlo pasar en mejor formato al Texto del Canvas
    public TextMeshProUGUI score; //Agregué un TextMeshPro para que nos de el score en pantalla
    public GameObject kiwi;
    public GameObject apple;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Kiwi"))
        {
            scoreNum += 300;
            Destroy(other.gameObject); //No funcionaba porque estaba como Destroy(Kiwi.gameObject); 
        }

        if (other.gameObject.CompareTag("Apple"))
        {
            scoreNum += 100;
            Destroy(other.gameObject); //No funcionaba porque estaba como Destroy(Apple.gameObject); 
        }
    }

    private void Update()
    {
        score.text = "Score: " + scoreNum.ToString("D5"); //Pasé a string de 5 dígitos el scoreNum al TextMeshPro en Update para que se actualice
    }

}
