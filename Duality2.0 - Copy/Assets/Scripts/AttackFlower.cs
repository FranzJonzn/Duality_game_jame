using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackFlower : MonoBehaviour
{
     bool isAlive = true;
    MainFlowerBrain ref_mainFlower;

    public float costPerSecond = 1f;
    public float lifeBackOnDef = 5f;
    
    public GameObject alive;
    public GameObject dead;
    public GameObject coliders;
    public Collider2D clickColider;
    private Coroutine draineRoutine;

    public ParticleSystem drainePowe;
    [Space]
    [Space]
    public AudioManager soundManager;
    //public Color weakColor = Color.yellow;
    //public Color strongColor = Color.red;


    public float MaxLifeRetun = 100;
    private float lifeDrainde = 0;
    public void ActiveFLower(MainFlowerBrain brain)
    {



  

        if (ref_mainFlower == null)
        {


            ref_mainFlower = brain;
            draineRoutine = StartCoroutine(draineLife());

         
            //sets so the flowers apper behing the maine flower then there behínde it (main floweer should be 0)
            alive.GetComponentInChildren<SpriteRenderer>().sortingOrder = (ref_mainFlower.transform.position.y < transform.position.y) ? -1 : 1;

            //randomly flips it to get variation
            int random1 = (Random.Range(0f, 1f) > 0.5f) ? -1 : 1;
            transform.localScale = new Vector3(random1, 1, 1);

        }
        soundManager.PlayOnBorn();
        if (drainePowe != null)
        {
            drainePowe.transform.position = new Vector3(ref_mainFlower.transform.position.x, ref_mainFlower.transform.position.y, -5f);
            drainePowe.transform.localScale = transform.localScale;

           
               drainePowe.transform.LookAt(transform.position);


            drainePowe.collision.AddPlane(UnderHodStuff.instance.particlePlaneColider);
        }

    }


    private void Update()
    {
        if (isAlive && ref_mainFlower.pDead)
        {
            drainePowe.Stop();
        }
    }
    public float Kill()
    {
        alive.SetActive(false);
        dead.SetActive(true);
        coliders.SetActive(false);
        drainePowe.Stop();// .SetActive(false);

        clickColider.enabled = false;
        isAlive = false;
        StopCoroutine(draineRoutine);
        soundManager.PlayOnDeath();
        return lifeDrainde;
    }

  

    private IEnumerator draineLife()
    {
        while (isAlive)
        {
            lifeDrainde = Mathf.Clamp(lifeDrainde+ costPerSecond, 0, MaxLifeRetun); ;
            ref_mainFlower.Damage(costPerSecond);


        
            //SpriteRenderer[] sr = alive.GetComponentsInChildren<SpriteRenderer>();

            //foreach (Image i in sr)
            //{
            //    i.color = Color.Lerp(weakColor, strongColor, lifeDrainde / MaxLifeRetun);
            //}

            yield return new WaitForSeconds(1);
        }

    } 


}
