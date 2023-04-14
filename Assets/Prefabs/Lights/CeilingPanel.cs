using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CeilingPanel : MonoBehaviour
{
    public int spawnX, spawnZ;
    public GameObject tile;
    public int panelPerLight;
    public float xMoveBy = 1.1f,
                 yMoveBy = 1.1f;
    public Material lightMat;
    public GameObject light;
    // Start is called before the first frame update
    void Start()
    {
        int counter = 0;
        for (int i = 0; i <Mathf.Abs(spawnX); i++)
        {
            for (int j = 0; j < Mathf.Abs(spawnZ); j++)
            {
                GameObject newTile = Instantiate(tile);
                newTile.transform.parent = transform;
                newTile.transform.position = new Vector3(gameObject.transform.position.x + i * xMoveBy * Mathf.Sign(spawnX),
                                                        gameObject.transform.position.y,
                                                        gameObject.transform.position.z + j * yMoveBy * Mathf.Sign(spawnX));
                if (counter == panelPerLight)
                {
                    newTile.transform.parent = gameObject.transform;
                    newTile.GetComponent<Renderer>().material = lightMat;
                    GameObject newLight = Instantiate(light);
                    newLight.transform.position = newTile.transform.position;
                    counter = 0;
                }
                counter++;
            }
            
        }
    }
}
