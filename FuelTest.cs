using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelTest : MonoBehaviour
{
    // Start is called before the first frame update
public float Gas = 100.0f;
public float MaxGas = 100.0f;
[SerializeField] Text UIGas;
    //---------------------------------------------------------
    private const float GasDecreasePerFrame = 1.0f;

    //---------------------------------------------------------

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
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


    private void Update()
{
    bool isFlying = Input.GetKey(KeyCode.Space);
    if (isFlying)
    {
        Gas = Mathf.Clamp(Gas - (GasDecreasePerFrame * Time.deltaTime), 0.0f, MaxGas);
    }

        UIGas.text = ((float)Gas).ToString();
        //else if (Gas < MaxGas)
        //{
        //    if (GasRegenTimer >= GasTimeToRegen)
        //        Gas = Mathf.Clamp(Gas + (GasIncreasePerFrame * Time.deltaTime), 0.0f, MaxGas);
        //    else
        //        GasRegenTimer += Time.deltaTime;
        //}
        Debug.Log("Hodnota Gasu" + Gas);
}
}
