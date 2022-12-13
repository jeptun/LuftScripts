using UnityEngine;
using UnityEngine.UI;

namespace GasStation{
public class Fuel : MonoBehaviour
{
    // referen�n� prom�nn� na palivovou stanici
    public GameObject vehicle;
    [SerializeField] Text UIGas;

    // prom�nn� pro ulo�en� aktu�ln�ho stavu paliva vozidla
    public float fuelLevel;

    void Update()
    {
        // zji�t�n�, zda vozidlo dot�k� palivov� stanice
        if (IsTouchingFuelStation())
        {
            // pokud ano, zv��� se stav paliva pozvolna
            fuelLevel = Mathf.Min(fuelLevel + Time.deltaTime, 100f);
        }
            UIGas.text = vehicle.ToString();
        }

    // metoda pro zji�t�n�, zda vozidlo dot�k� palivov� stanice
    bool IsTouchingFuelStation()
    {
        // z�sk�n� Collider komponenty palivov� stanice
        Collider fuelStationCollider = GetComponent<Collider>();
       
        // z�sk�n� Collider komponenty vozidla
        Collider vehicleCollider = vehicle.GetComponent<Collider>();
            
        // zji�t�n�, zda vozidlo a palivov� stanice se dot�kaj�
        return vehicleCollider.bounds.Intersects(fuelStationCollider.bounds);

    }
}

}
