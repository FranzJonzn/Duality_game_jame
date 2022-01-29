using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * den här klassen ska hanter 
 * lite defust spellogick saker, 
 * som att exemeplvis tillhandahålla spelplanes storlek
 * kanske kammara efekter osv
*/
public class UnderHodStuff : MonoBehaviour
{


    public static UnderHodStuff instance;


    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Camera curretnCamera;


    public Rect playFeild { get { return new Rect(new Vector2(curretnCamera.pixelWidth * 0.5f, curretnCamera.pixelHeight * 0.5f), 
                                                  new Vector2(curretnCamera.pixelWidth , curretnCamera.pixelHeight )); } }

    private void OnDrawGizmos()
    {
        //Camera curretn = Camera.main;
        //if (curretnCamera != null)
        //{


        //    Vector2 cameraCentger = new Vector2(playFeild.position.x, playFeild.position.y);

        //    Gizmos.DrawCube(playFeild.position, playFeild.size);
        //}
    }
}
