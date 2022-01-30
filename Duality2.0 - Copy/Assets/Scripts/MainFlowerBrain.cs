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
    public List<GameObject> DeadFlowers = new List<GameObject>();
    [Space]
    [Space]

    public Sprite[] sprits;
    public SpriteRenderer sRenderer;
    public int part = 0;


    private Transform flowerContaintger;

    public bool pDead { get { return life == lifeMin; } }
    private bool dead = false;
    private string[] förnamn = { "Kerstin","Annet","Solvej","Jimmy", "Jeniffer", "Gustav", "Albert", "Erik", "Erika", "Anna", "Kristian", "Kristina", "Josef", "Sven", "Ingirid", "Bert", "Knut", "Hugo", "Anton", "Anders", "Anika", "Ulrika", "Tau", "Majsan" };
    private string[] efternamn = { "Svensson", "Eriskson","Ek", "Silverspare","Gustaffson", "Svärd", "Dag och Nat", "Jonson", "Jonsson", "Jonzon", "Jonzzon", "Vasa", "Björnson", "Eriskdotter", "Björklund", "Brosson" };


    private void Start()
    {



        flowerContaintger = new GameObject().transform;
        flowerContaintger.name = "lowerBucket";
        flowerContaintger.position = new Vector3(0, 0, 0);

        life = lifeMax;
        
    }


    void Update()
    {
        if (life == lifeMin)
            return;

        if (Input.GetMouseButtonDown(0))
        {



            GameObject newFlowe = Instantiate(prefab_flower, UnderHodStuff.instance.getMousPosition, prefab_flower.transform.rotation, flowerContaintger); //, Quaternion.identity);
            newFlowe.name = förnamn[Random.Range(0,förnamn.Length)] +" "+ efternamn[Random.Range(0,efternamn.Length)];
            newFlowe.GetComponent<AttackFlower>().ActiveFLower(this);
            ActiveFlowers.Add(newFlowe);

        }
        else if (Input.GetMouseButtonDown(1))
        {


            RaycastHit2D hit = Physics2D.Raycast(UnderHodStuff.instance.getMousPosition, Vector2.zero, Mathf.Infinity, floweMask.value);
  

            if (hit)
            {
                DeadFlowers.Add(hit.collider.gameObject);
                ActiveFlowers.Remove(hit.collider.gameObject);
                life = Mathf.Clamp(life+ hit.collider.GetComponent<AttackFlower>().Kill(), lifeMin, lifeMax);
                UppdateSprite();

                ControllIfDead();
            }
            

        }




        
    }

  
    private void ControllIfDead()
    {
        if (!dead && life == lifeMin)
        {
            dead = true;
            UnderHodStuff.instance.GameOver(this);
        }
    }

    public void Damage(float damage)
    {


        life = Mathf.Clamp(life-damage, lifeMin, lifeMax);

        UppdateSprite();



        ControllIfDead();


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
