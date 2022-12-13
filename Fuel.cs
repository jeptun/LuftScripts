using UnityEngine;
using UnityEngine.UI;

namespace GasStation{
public class Fuel : MonoBehaviour
{
    // referenèní promìnná na palivovou stanici
    public GameObject vehicle;
    [SerializeField] Text UIGas;

    // promìnná pro uložení aktuálního stavu paliva vozidla
    public float fuelLevel;

    void Update()
    {
        // zjištìní, zda vozidlo dotýká palivové stanice
        if (IsTouchingFuelStation())
        {
            // pokud ano, zvýší se stav paliva pozvolna
            fuelLevel = Mathf.Min(fuelLevel + Time.deltaTime, 100f);
        }
            UIGas.text = vehicle.ToString();
        }

    // metoda pro zjištìní, zda vozidlo dotýká palivové stanice
    bool IsTouchingFuelStation()
    {
        // získání Collider komponenty palivové stanice
        Collider fuelStationCollider = GetComponent<Collider>();
       
        // získání Collider komponenty vozidla
        Collider vehicleCollider = vehicle.GetComponent<Collider>();
            
        // zjištìní, zda vozidlo a palivová stanice se dotýkají
        return vehicleCollider.bounds.Intersects(fuelStationCollider.bounds);

    }
}

}
