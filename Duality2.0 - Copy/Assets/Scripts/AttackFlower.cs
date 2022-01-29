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
    public Collider2D colider;
    private Coroutine draineRoutine;


    public Color weakColor = Color.yellow;
    public Color strongColor = Color.red;


    public float MaxLifeRetun = 100;
    private float lifeDrainde = 0;
    public void ActiveFLower(MainFlowerBrain brain)
    {
        if(ref_mainFlower == null)
        {
            ref_mainFlower = brain;
            draineRoutine = StartCoroutine(draineLife());

         
            //sets so the flowers apper behing the maine flower then there behínde it (main floweer should be 0)
            alive.GetComponentInChildren<SpriteRenderer>().sortingOrder = (ref_mainFlower.transform.position.y < transform.position.y) ? -1 : 1;

            //randomly flips it to get variation
            int random1 = (Random.Range(0f, 1f) > 0.5f) ? -1 : 1;
            transform.localScale = new Vector3(random1, 1, 1);

        }



    }



    public float Kill()
    {
        alive.SetActive(false);
        dead.SetActive(true);
        colider.enabled = false;
        isAlive = false;
        StopCoroutine(draineRoutine);
        return lifeDrainde;
    }

  

    private IEnumerator draineLife()
    {
        while (isAlive)
        {
            lifeDrainde = Mathf.Clamp(lifeDrainde+ costPerSecond, 0, MaxLifeRetun); ;
            ref_mainFlower.Damage(costPerSecond);

            Image[] images = alive.GetComponentsInChildren<Image>();
            
            foreach(Image i in images)
            {
                i.color = Color.Lerp(weakColor, strongColor, lifeDrainde / MaxLifeRetun);
            }

            yield return new WaitForSeconds(1);
        }

    } 


}
