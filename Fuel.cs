using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
   [SerializeField] int Gas = 100;

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
       
        Gas = Gas - 1;
        return;
    }
}
