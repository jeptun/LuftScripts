using UnityEngine;

public class Fuel : MonoBehaviour
{
    // referenèní promìnná na palivovou stanici
    public GameObject fuelStation;

    // promìnná pro uložení aktuálního stavu paliva vozidla
    private float fuelLevel = 10f;

    void Update()
    {
        // zjištìní, zda vozidlo dotýká palivové stanice
        if (IsTouchingFuelStation())
        {
            // pokud ano, zvýší se stav paliva pozvolna
            fuelLevel = Mathf.Min(fuelLevel + Time.deltaTime, 100f);
        }
    }

    // metoda pro zjištìní, zda vozidlo dotýká palivové stanice
    bool IsTouchingFuelStation()
    {
        // získání Collider komponenty vozidla
        Collider vehicleCollider = GetComponent<Collider>();

        // získání Collider komponenty palivové stanice
        Collider fuelStationCollider = fuelStation.GetComponent<Collider>();

        // zjištìní, zda vozidlo a palivová stanice se dotýkají
        return vehicleCollider.bounds.Intersects(fuelStationCollider.bounds);

    }
}
