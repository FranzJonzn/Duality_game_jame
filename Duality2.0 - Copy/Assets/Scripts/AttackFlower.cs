using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlower : MonoBehaviour
{
    public bool alive = true;
    MainFlowerBrain ref_mainFlower;

    public float costPerSecond = 1f;
    public float lifeBackOnDef = 5f;

    public GameObject aliveGraphic;
    public GameObject deadGraphic;


    public Collider2D thisCollider;
    private Coroutine draineRoutine;

    public float MaxLifeRetun = 100;
    private float lifeDrainde = 0;
    public void ActiveFLower(MainFlowerBrain brain)
    {
        if(ref_mainFlower == null)
        {
            ref_mainFlower = brain;
            draineRoutine = StartCoroutine(draineLife());
        }

    }



    public float Kill()
    {
        aliveGraphic.SetActive(false);
        deadGraphic.SetActive(true);
        thisCollider.enabled = false;
        alive = false;
        StopCoroutine(draineRoutine);
        return Mathf.Clamp( lifeDrainde, 0, MaxLifeRetun);
    }

  

    private IEnumerator draineLife()
    {
        while (alive)
        {
            lifeDrainde += costPerSecond;
            ref_mainFlower.Damage(costPerSecond);
            yield return new WaitForSeconds(1);
        }

    } 


}
