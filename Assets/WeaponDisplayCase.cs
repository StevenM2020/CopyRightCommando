using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDisplayCase : MonoBehaviour
{
    public GameObject[] stars;
    public int gunNum;
    public Material MatGreen, MatYellow;
    public GameObject screen;
    private bool blnIsActive = false;
    private List<int> gunList = new List<int>();
    public void SetStars(int num)
    {
        for(int i = 0; i < num; i++)
            stars[i].SetActive(true);
        if (num == 3)
        {
            blnIsActive = true;
            screen.GetComponent<Renderer>().material = MatYellow;
        }
    }
    public bool IsActive()
    {
        return blnIsActive;
    }
    public void SelectWeapon()
    {
        if (GameObject.Find("WeaponStarManager").GetComponent<WeaponStarManager>().AddWeapon(gunNum))
        {
            screen.GetComponent<Renderer>().material = MatGreen;
        }
        else
        {
            screen.GetComponent<Renderer>().material = MatYellow;
        }
    }
    
}

