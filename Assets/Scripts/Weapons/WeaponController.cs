using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    protected  Transform firePoint;
    InventoryManager inventory;
    [SerializeField] GameObject playerShooting;
    Transform player;

    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject _playerShooting = (GameObject)Instantiate(playerShooting, player.transform.position, player.rotation);
        _playerShooting.transform.SetParent(player);
        PlayerShooting ps = _playerShooting.GetComponent<PlayerShooting>();

        inventory.AddWeapon(0, ps);
        inventory.weaponLevels[0] = 1;
        inventory.shootingLvl = 1;
    }
}