using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * den här klassen ska hanter 
 * lite defust spellogick saker, 
 * som att exemeplvis tillhandahålla spelplanes storlek
 * kanske kammara efekter osv
*/
public class UnderHodStuff : MonoBehaviour
{


    public static UnderHodStuff instance;

    public Text hischorText;
    public Text bragingText;
    public Text deadList;
    public Slider deathScroll;
    public RectTransform deathScroll_RectTransform;
    public GameObject gameOverScrfeen;
    private Coroutine memorialScroll;
    public float scrollSpeed = 0.01f;

    [Space]
    [Space]
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    [Space]
    [Space]
    public float startTime = 0;
    public float endTime;

    [Space]
    public int starevSlugs = 0;

    private void Start()
    {

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);


        if(instance == null)
        {
            startTime = Time.time;
            instance = this;
            gameOverScrfeen.SetActive(false);
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





        //sortList (antar att alla sprits utom back ligger i årdning Graphic_grafiken
        renderers.Sort(delegate (SpriteRenderer a, SpriteRenderer b) {
            return Vector3.Distance(new Vector3(a.transform.parent.position.x, -100,0), a.transform.parent.position).
         CompareTo(Vector3.Distance(new Vector3(b.transform.parent.position.x, -100,0), b.transform.parent.position));
        });


        int curretn = renderers.Count;
        foreach (SpriteRenderer sr in renderers)
        {

            sr.sortingOrder = curretn--;
    

        }



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

    //private Matrix4x4 RotatZ(float aANgleRad)
    //{
    //    Matrix4x4 m = Matrix4x4.identity;
    //    m.m00 = m.m11 = Mathf.Cos(aANgleRad);
    //    m.m10 = Mathf.Sin(aANgleRad);
    //    m.m01 = -m.m10;
    //    return m;
    //}




    public void GameOver(MainFlowerBrain flowerBrain)
    {

        endTime = Time.time;
        float playedTime = endTime - startTime;
        hischorText.text += " " + playedTime + " seconds";

        bragingText.text = starevSlugs + " " + bragingText.text;

       float scrollLengh = (float)flowerBrain.DeadFlowers.Count * deadList.fontSize;

        float oldLengh = deathScroll_RectTransform.sizeDelta.y;
        deathScroll_RectTransform.sizeDelta = new Vector2( deathScroll_RectTransform.sizeDelta.x, scrollLengh*1.1f + deathScroll_RectTransform.sizeDelta.y);
        //deadList.rectTransform.sizeDelta =new Vector2(deadList.rectTransform.sizeDelta.x, scrollLengh);



        string listOfDeadFlowers = "";
        foreach(GameObject g in flowerBrain.DeadFlowers)
        {
            listOfDeadFlowers += g.name + "\n";
        }
        deadList.text = listOfDeadFlowers;

        gameOverScrfeen.SetActive(true);
        memorialScroll = StartCoroutine(CreditScrool());
    }



   
    private IEnumerator CreditScrool()
    {



        float time = 0 ;
        while (true)
        {
            yield return null;// new WaitForSeconds(1f);
                              // deathScroll.value = Mathf.Clamp(deathScroll.value+ scaledScrollSpeed * Time.deltaTime, deathScroll.minValue, deathScroll.maxValue);

            deathScroll.value = Mathf.Lerp(deathScroll.minValue, deathScroll.maxValue, time / scrollSpeed);

            time += Time.deltaTime;

            if(deathScroll.value == deathScroll.maxValue)
            {
                deathScroll.value = deathScroll.minValue;
                time = 0;
            }
        }
    }








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


     
        //    if(dir.x < 0)// höger sida
        //    {

        //        Gizmos.color = Color.blue;

        //        if ( (dir.y < angleForSIde && dir.y > -angleForSIde))
        //        {

        //            Gizmos.color = Color.red;
        //        }
        //    }
        //    else //vänster sida
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
