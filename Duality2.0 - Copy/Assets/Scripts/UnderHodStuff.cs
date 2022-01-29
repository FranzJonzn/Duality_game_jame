using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * den h�r klassen ska hanter 
 * lite defust spellogick saker, 
 * som att exemeplvis tillhandah�lla spelplanes storlek
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



    private void Update()
    {

        SortAllSPrites();

    }
    //not effeicent, just need it to work
    private void SortAllSPrites()
    {
        SpriteRenderer[] all = FindObjectsOfType<SpriteRenderer>();
        List<SpriteRenderer> renderers = new List<SpriteRenderer>();




        foreach (SpriteRenderer sr in all)
        {
            if (sr.gameObject.tag == "back")
            {
                sr.sortingOrder = -all.Length;
          
            }
            else if (sr.gameObject.tag == "front")
            {
                sr.sortingOrder = all.Length+1;
            }
            else
            {
                renderers.Add(sr);
            }
        }

        //if (renderers.Count > 7)
        //{
        //    int curretn = 0;
        //    Vector3 lase = Vector3.zero;
        //    foreach (SpriteRenderer sr in renderers)
        //    {
        //        if (curretn != 0)
        //        {
        //            Debug.DrawLine(sr.transform.parent.position, lase, Color.red);
        //        }
        //        sr.sortingOrder = curretn++;
        //        lase = sr.transform.parent.position;

        //    }

        //}



        //sortList (antar att alla sprits utom back ligger i �rdning Graphic_grafiken
        renderers.Sort(delegate (SpriteRenderer a, SpriteRenderer b) {
            return Vector3.Distance(new Vector3(a.transform.parent.position.x, -100,0), a.transform.parent.position).
         CompareTo(Vector3.Distance(new Vector3(b.transform.parent.position.x, -100,0), b.transform.parent.position));
        });


        int curretn = renderers.Count;
        foreach (SpriteRenderer sr in renderers)
        {

            sr.sortingOrder = curretn--;
    

        }

        // shows the order of the object 
        //if (renderers.Count > 7)
        //{
        //    bool skipFirst = false;
        //    Vector3 lase = Vector3.zero;
        //    foreach (SpriteRenderer sr in renderers)
        //    {
        //        if (skipFirst)
        //        {
        //            Debug.DrawLine(sr.transform.parent.position, lase, Color.red);
        //        }
        //        skipFirst = true;
        //        lase = sr.transform.parent.position;

        //    }
        //    Debug.LogError("pause");
        //}

    }

    public Vector3 getMousPosition
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = curretnCamera.nearClipPlane;

            Vector3 retrunValue = curretnCamera.ScreenToWorldPoint(mousePos);
            retrunValue = new Vector3(retrunValue.x, retrunValue.y, 0f);
            return  retrunValue;

        }
    }

    private Matrix4x4 RotatZ(float aANgleRad)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.m00 = m.m11 = Mathf.Cos(aANgleRad);
        m.m10 = Mathf.Sin(aANgleRad);
        m.m01 = -m.m10;
        return m;
    }
    public GameObject temp;
    public float angleForSIde = 0.4f;
    private void OnDrawGizmos()
    {

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(Vector3.zero, new Vector3(-0.8f, -0.6f,0.0f));
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(Vector3.zero, new Vector3(0.8f, -0.6f, 0.0f));
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(Vector3.zero, new Vector3(0.8f, 0.6f, 0.0f));
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawLine(Vector3.zero, new Vector3(-0.8f, 0.6f, 0.0f));
        //if (temp != null)
        //{



        //    Vector3 dir = (-temp.transform.position).normalized;
 


        //    Debug.Log(dir);


     
        //    if(dir.x < 0)// h�ger sida
        //    {

        //        Gizmos.color = Color.blue;

        //        if ( (dir.y < angleForSIde && dir.y > -angleForSIde))
        //        {

        //            Gizmos.color = Color.red;
        //        }
        //    }
        //    else //v�nster sida
        //    {


        //        if ((dir.y < angleForSIde && dir.y > -angleForSIde))
        //        {

        //            Gizmos.color = Color.red;
        //        }
        //        else
        //        {
        //            Gizmos.color = Color.green;
        //        }


        //    }

  

        //    Gizmos.DrawLine(Vector3.zero, temp.transform.position);

        //}

        //Camera curretn = Camera.main;
        //if (curretnCamera != null)
        //{


        //    Vector2 cameraCentger = new Vector2(playFeild.position.x, playFeild.position.y);

        //    Gizmos.DrawCube(playFeild.position, playFeild.size);
        //}
    }
}
