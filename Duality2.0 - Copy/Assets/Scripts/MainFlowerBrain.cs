using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFlowerBrain : MonoBehaviour
{
    public float lifeMin = 0;
    public float life = 100f;
    public float lifeMax = 100f;
    [Space]
    [Space]

    public GameObject prefab_flower;
    public LayerMask floweMask;
    [Space]

    public List<GameObject> ActiveFlowers = new List<GameObject>();
    public List<GameObject> DeadFLowers = new List<GameObject>();
    [Space]
    [Space]

    public Sprite[] sprits;
    public SpriteRenderer sRenderer;
    public int part = 0;


    private Transform flowerContaintger;
   
    private void Start()
    {



        flowerContaintger = new GameObject().transform;
        flowerContaintger.name = "lowerBucket";
        flowerContaintger.position = new Vector3(0, 0, 0);

        life = lifeMax;
        
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {



            GameObject newFlowe = Instantiate(prefab_flower, UnderHodStuff.instance.getMousPosition, prefab_flower.transform.rotation, flowerContaintger); //, Quaternion.identity);

            newFlowe.GetComponent<AttackFlower>().ActiveFLower(this);
            ActiveFlowers.Add(newFlowe);

        }
        else if (Input.GetMouseButtonDown(1))
        {


            RaycastHit2D hit = Physics2D.Raycast(UnderHodStuff.instance.getMousPosition, Vector2.zero, Mathf.Infinity, floweMask.value);
  

            if (hit)
            {
                DeadFLowers.Add(hit.collider.gameObject);
                ActiveFlowers.Remove(hit.collider.gameObject);
                life = Mathf.Clamp(life+ hit.collider.GetComponent<AttackFlower>().Kill(), lifeMin, lifeMax);
                UppdateSprite();
            }
            

        }




        
    }

  


    public void Damage(float damage)
    {


        life = Mathf.Clamp(life-damage, lifeMin, lifeMax);

        UppdateSprite();

    }

    private void UppdateSprite()
    {
        part = Mathf.Clamp((int)((life / lifeMax) * 10), 0, sprits.Length);
        sRenderer.sprite = sprits[part];
    }

    //public float blincSec = 1;
    //public int blinckCount = 3;
    //private IEnumerator losLife()
    //{
        
    //    for(int i = 0; i < blinckCount; ++i)
    //    {
    //        yield return new WaitForSeconds(blincSec);
    //    }
    //}


}
