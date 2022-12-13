using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : BaseWeapon
{    
    [SerializeField] public int armorPoints = 2;    

    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.PlayerMaxHP += armorPoints * level;        
    }

}
