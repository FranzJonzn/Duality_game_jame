using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyMaster master;
    public Rigidbody2D rig2D = null;
    public float speed = 20;
    public float lifeTimeMax = 30;
    public float lifeTimeMin = 12;
    public float damageOnInpact = 10f;
    public Coroutine life;

    public bool isAlive = true;


    private void Update()
    {

        if (master != null)
        {
            Vector2 dir = (master.targetFlower.transform.position - transform.position).normalized;
            rig2D.velocity = dir * speed;
            transform.rotation.SetLookRotation(dir);
        }
        if (!isAlive)
        {
            master.KillMe(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("here");
        MainFlowerBrain brain = collision.gameObject.GetComponent<MainFlowerBrain>();
        if (brain != null)
        {
            Debug.Log("here2");
            brain.Damage(damageOnInpact);
            StopCoroutine(life);
            isAlive = false;
        }
    }


    public void ActivateEnemy(EnemyMaster myMaster)
    {
        master = myMaster;
 
        life = StartCoroutine(LifeTime());



    }


    public IEnumerator LifeTime()
    {

        yield return new WaitForSeconds(Random.Range(lifeTimeMin,lifeTimeMax));

        isAlive = false;

    }



}
