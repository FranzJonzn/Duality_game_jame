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


    public Rect playFeild { get { return new Rect(new Vector2(0,0),
                                                  curretnCamera.ScreenToWorldPoint( new Vector2(curretnCamera.scaledPixelWidth*1.5f , curretnCamera.scaledPixelHeight* 1.5f))); } }




    public Vector3 getMousPosition
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = curretnCamera.nearClipPlane;
            return curretnCamera.ScreenToWorldPoint(mousePos);

        }
    }

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
