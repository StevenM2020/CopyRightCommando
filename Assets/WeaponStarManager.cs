using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponStarManager : MonoBehaviour
{
    public GameObject[] displays;
    private int numGun;
    private int gun1, gun2;
    // Start is called before the first frame update
    void Start()
    {
        int stars = 3;
        int next = 0;
        while (stars > 0)
        {
            displays[next].GetComponent<WeaponDisplayCase>().SetStars(stars > 2 ? 3 : stars);
            stars -= 3;
            next++;
        }
    }
    public bool AddWeapon(int gNum)
    {
        if (gun1 == gNum)
        {
            gun1 = gun2;
            gun2 = -1;
            numGun -=1;
        }
        else if (gun2 == gNum)
        {
            gun2 = 0;
            numGun -= 1;
        }
        else if( numGun == 0)
        {
            gun1 = gNum;
            numGun = 1;
            return true;
        }else if( numGun == 1)
        {
            gun2 = gNum;
            numGun = 2;
            return true;
        }
        return false;
    }
    public int GetNumGunsSelected()
    {
        return numGun;
    }
}
