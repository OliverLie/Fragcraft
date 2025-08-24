using UnityEngine;

public class AIWeaponHandler : MonoBehaviour
{
    [Header("References")]
    public Transform weaponHolder;     // Håndens "WeaponHolder"
    public GameObject weaponPrefab;    // Våben-prefab (med GripPoint)

    private GameObject currentWeapon;

    void Start()
    {
        EquipWeapon();
    }

    public void EquipWeapon()
    {
        if (weaponPrefab == null || weaponHolder == null) return;

        // Spawn våben
        currentWeapon = Instantiate(weaponPrefab);

        // Find grip point
        Transform grip = currentWeapon.transform.Find("GripPoint");
        if (grip == null)
        {
            Debug.LogError("Våbenet mangler et GripPoint!");
            Destroy(currentWeapon);
            return;
        }

        // Parent våben til AI's hånd
        currentWeapon.transform.SetParent(weaponHolder);

        // Match gripPoint til weaponHolder
        // (sørger for korrekt position/rotation i hånden)
        currentWeapon.transform.position = weaponHolder.position;
        currentWeapon.transform.rotation = weaponHolder.rotation;

        // Juster offset så GripPoint sidder præcis på holderen
        Vector3 offsetPos = currentWeapon.transform.position - grip.position;
        currentWeapon.transform.position += offsetPos;

        Quaternion offsetRot = Quaternion.Inverse(grip.localRotation);
        currentWeapon.transform.rotation *= offsetRot;
    }
}
