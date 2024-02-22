using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(3);
    public int[] weaponLevels = new int[3]; //musi odpowiadaæ pozycjom z listy weponSlots
    //public List<passiveItem> pasiveitemSlots = new List<passiveItem>(3);
    //public int[] passiveItemLevel = new int[3];
    public int shootingLvl = 0;
    public int axeLvl = 0;
    public int bookLvl = 0;
    public int fireballLvl = 0;
    public int scytheLvl = 0;
    public int maxlvl = 5;

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
    }
    /*
    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;
    }
    */
    public void LevelUpWeapon(int slotIndex)
    {
        weaponLevels[slotIndex] += 1;
    }
    /*
    public void LevelUpPassiveItem(int slotIndex)
    {

    }
    */
}