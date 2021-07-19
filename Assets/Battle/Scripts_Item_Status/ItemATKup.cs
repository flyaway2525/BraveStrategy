using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemATKup : Item_Status
{
    public override void passive_First() {
        base.passive_First();
        status.atk_add += 50;
        Debug.Log(gameObject + "get 50 ATK");
    }
    public void remove_it() {
        status.atk_add -= 50;
    }
}
