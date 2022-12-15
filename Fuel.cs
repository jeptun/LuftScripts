using UnityEngine;
using UnityEngine.UI;


public class Fuel : MonoBehaviour
{
    // referenèní promìnná na palivovou stanici
    public GameObject vehicle;
    [SerializeField] Text UIGas;
    public float Gas = 100.0f;
    public float MaxGas = 100.0f;

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
            UIGas.text = fuelLevel.ToString();
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
