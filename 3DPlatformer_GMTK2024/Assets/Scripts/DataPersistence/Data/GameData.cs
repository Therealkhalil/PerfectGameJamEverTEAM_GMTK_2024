using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/// <summary>
/// The GameData class stores all of the data we'd like to
/// collect and save on a per player basis
/// </summary>
public class GameData
{
    public int deathCount;
    //Storing complex Data types (such as a list of custom objects)
    //becomes quite troublesome when serializing, to counter this we will
    //only be storing the quantities of each resource.
    //public List<Loot> Inventory;
    public int[] itemQuantity;
    public int weaponDamageMultiplier;
    public int playerHealthMultiplier;
    public int playerSpeedMultiplier;
    public int playerLightMultiplier;
    public int WeaponLevel,SecondaryWeaponLevel;
    public Vector3 playerWaypoint;

    public int CurrentArmor;



    // Values found in this constructor represent initial values for a new save-state
    public GameData()
    {
        this.WeaponLevel = 0;
        this.SecondaryWeaponLevel = 0;
        this.itemQuantity = new int[] { 0, 0, 0, 0, 0 ,0 ,0 ,0 ,0 ,0 ,0 ,0};
        this.weaponDamageMultiplier = 1;
        this.playerHealthMultiplier = 1;
        this.playerSpeedMultiplier = 1;
        this.playerLightMultiplier = 1;
        this.deathCount = 0;
        this.playerWaypoint = new Vector3(0, 0, 0);
        this.CurrentArmor = 0;
    }
}
