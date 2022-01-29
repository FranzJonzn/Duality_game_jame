using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackFlower : MonoBehaviour
{
    public bool isAlive = true;
    MainFlowerBrain ref_mainFlower;

    public float costPerSecond = 1f;
    public float lifeBackOnDef = 5f;

    public GameObject alive;
    public GameObject dead;

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
        }

    }



    public float Kill()
    {
        alive.SetActive(false);
        dead.SetActive(true);
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
