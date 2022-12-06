using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//TODO interval casu
public class Fuel : MonoBehaviour
{
   [SerializeField] float Gas = 100f;

   [SerializeField] float changePerSecond;

   [SerializeField] Text UIGas;

    private float variableToChange;
    private float startTime;
    float minusTime;
    private float myTime;


    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag )
        {
            case "Fuel":
                Debug.Log("Fuel");
                GetGas();
                Debug.Log(Gas);
                break;
            case "Gold":
                Debug.Log("gold");
                 break;
        }
    }
    private void GetGas()
    {
        float gasIncrement = Gas + 1;
        Gas = gasIncrement;
        return;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            variableToChange += changePerSecond * Time.deltaTime;
            Gas -= variableToChange ;
             Debug.Log("vari " + variableToChange);

           // minusTime = Time.time - startTime;
           // Debug.Log(minusTime + "minus1");
           // //Gas -= minusTime;
           //// Debug.Log("gasTime" +  Gas);
           // if (Time.time > 0)
           // {
           //     myTime = Time.time * 0;
           //     minusTime = myTime;
           //     Debug.Log(minusTime + "minustime2");
                
           // }
           // Debug.Log(minusTime + "minustime3");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key was released.");
        }
      

        UIGas.text = ((float)Gas).ToString();
       // minusTime = 0;
     
    }

}
