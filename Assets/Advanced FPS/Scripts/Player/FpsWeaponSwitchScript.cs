using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsWeaponSwitchScript : MonoBehaviour
{
    public Transform weaponPos;
    private Queue<int> slotQ;
    private Transform activeSlot;
    private Transform dropOffPos;
    private KeyCode[] NUMBER_KEYCODES = {
                    KeyCode.Alpha0,
                    KeyCode.Alpha1,
                    KeyCode.Alpha2,
                    KeyCode.Alpha3,
                    KeyCode.Alpha4,
                    KeyCode.Alpha5,
                    KeyCode.Alpha6,
                    KeyCode.Alpha7,
                    KeyCode.Alpha8,
                    KeyCode.Alpha9
                    };

    void Awake()
    {
        dropOffPos = GetComponent<FpsPickupDropScript>().dropOffPos;

        // Default activeSlot to first slot
        activeSlot = weaponPos.GetChild(0);

        slotQ = new Queue<int>();

        slotQ.Enqueue(1);
        slotQ.Enqueue(1);

        AssignActiveWeapon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) AssignActiveWeapon(slotQ.Peek());

        for (int i = 0; i < NUMBER_KEYCODES.Length; i++) {
            if (Input.GetKeyDown(NUMBER_KEYCODES[i])) {
                if (i == 0) {
                    AssignActiveWeapon(10);
                } else {
                    AssignActiveWeapon(i);
                }
            }
        }
    }

    public bool AssignActiveWeapon(Transform newWeapon)
    {
        WeaponPickupScript pickupScript = newWeapon.GetComponent<WeaponPickupScript>();

        bool activeValid = false;
        // Search for empty, valid slot for new weapon.
        foreach (int slot in pickupScript.slots) {
            // If active slot is among valid slots, set activeValid to true
            if (activeSlot.GetSiblingIndex() == slot) activeValid = true;
            
            Transform parentSlot = weaponPos.GetChild(slot);
            
            // Check if weapon has transferable ammo
            ITransferable newTransferable = newWeapon.GetComponent<ITransferable>();
            
            // Check if current valid slot is occupied
            if (parentSlot.childCount != 0) {
                // Skip if new weapon does not have transferable ammo
                if (newTransferable == null) {
                    continue;
                } else {

                    // Check if weapon in valid slot has transferable ammo
                    ITransferable parentTransferable = parentSlot.GetChild(0).GetComponent<ITransferable>();
                    // Check if weapon in valid slot has same type of transferable ammo
                    if (parentTransferable == null) continue;
                    if (newTransferable.Type() == parentTransferable.Type()) {

                        // Transfer ammo to weapon in valid slot
                        newTransferable.Transfer(parentTransferable);

                        // FpsEvents.UpdateHeldWeapon.Invoke();
                        FpsEvents.FpsUpdateHud();
                        
                        return false;
                    } else {

                        // Different types of ammo, continue to next valid slot
                        continue;
                    }       
                }
            }
            
            // Empty valid slot found!
            // Check if activeSlot has an active weapon
            if (activeSlot.childCount > 0) {
                // Deactivate active weapon
                activeSlot.GetChild(0).gameObject.SetActive(false);  
            }

            // Assign new weapon to empty slot
            pickupScript.transform.SetParent(parentSlot, false);

            // Update activeSlot
            activeSlot = parentSlot;

            // FpsEvents.UpdateHeldWeapon.Invoke();
            FpsEvents.FpsUpdateHud();

            // Add this slot to the Q
            UpdateQ(slot + 1);

            return true;
        }

        // If all valid slots for new weapon are filled, proceed below.
        // Check if activeSlot is valid slot.
        if (!activeValid) {
            // activeSlot is not a valid slot.
            // Deactivate active weapon
            activeSlot.GetChild(0).gameObject.SetActive(false);
            // Reassign activeSlot to first valid slot for new weapon
            activeSlot = weaponPos.GetChild(pickupScript.slots[0]);

            UpdateQ(pickupScript.slots[0] + 1);
        }

        GameObject weaponToDrop = activeSlot.GetChild(0).gameObject;

        // Activate activeSlot weapon. Redundant if activeSlot is a valid slot (activeValid = true)
        weaponToDrop.SetActive(true);
        weaponToDrop.GetComponent<WeaponPickupScript>().Drop(dropOffPos);

        // Assign new weapon to vacated activeSlot
        pickupScript.transform.SetParent(activeSlot, false);

        // FpsEvents.UpdateHeldWeapon.Invoke();
        FpsEvents.FpsUpdateHud();

        return true;
    }

    // Assign active weapon at slot
    public void AssignActiveWeapon(int num)
    {
        int index = num - 1;

        if (index >= weaponPos.childCount) return;
        Transform parentSlot = weaponPos.GetChild(index);
        if (parentSlot.childCount == 0) return;
        if (activeSlot == parentSlot) return;

        // Deactivate activeSlot weapon
        if (activeSlot.childCount != 0) activeSlot.GetChild(0).gameObject.SetActive(false);

        // Reassign activeSlot
        activeSlot = parentSlot;

        // Activate activeSlot weapon
        activeSlot.GetChild(0).gameObject.SetActive(true);

        // Update weapon reference
        // FpsEvents.UpdateHeldWeapon.Invoke();
        FpsEvents.FpsUpdateHud();
        
        UpdateQ(num);
        
    }

    // Assign next active weapon
    public void AssignActiveWeapon()
    {
        // Cycle through slots starting from next slot, then looping around at the last slot
        for (int i = 1; i <= weaponPos.childCount - 1; i++) {
            int slot = (activeSlot.GetSiblingIndex() + i) % weaponPos.childCount;
            Transform parentSlot = weaponPos.GetChild(slot);

            // Switch to valid slot if found
            if (parentSlot.childCount == 1) {
                // Deactivate activeSlot weapon, reassign activeSlot and activate weapon
                if (activeSlot.childCount != 0) activeSlot.GetChild(0).gameObject.SetActive(false);
                activeSlot = parentSlot;
                activeSlot.GetChild(0).gameObject.SetActive(true);

                

                UpdateQ(slot + 1);

                break;
            }
        }

        // Update weapon reference
        // FpsEvents.UpdateHeldWeapon.Invoke();
        FpsEvents.FpsUpdateHud();
        
    }

    void UpdateQ(int key)
    {
        slotQ.Dequeue();
        slotQ.Enqueue(key);
    }
}
