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

    public GameObject grafick;


    public void ActivateEnemy(EnemyMaster myMaster)
    {
        master = myMaster;

        life = StartCoroutine(LifeTime());
    }


    private void Update()
    {

        if (master != null)
        {
            //Vector2 dir = (master.targetFlower.transform.position - transform.position).normalized;

            //transform.rotation.SetLookRotation(dir);
   
            Move();
        }
        if (!isAlive)
        {
            master.KillMe(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        MainFlowerBrain brain = collision.gameObject.GetComponent<MainFlowerBrain>();
        if (brain != null)
        {
            brain.Damage(damageOnInpact);
            StopCoroutine(life);
            isAlive = false;
        }
    }


    private void Move()
    {
        //looking
        Vector2 dir =( master.targetFlower.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 275)
        {
            grafick.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            grafick.transform.localScale = new Vector3(1, 1, 1);
        }

        //move
        rig2D.velocity = dir * speed;
    }


    public IEnumerator LifeTime()
    {

        yield return new WaitForSeconds(Random.Range(lifeTimeMin,lifeTimeMax));

        isAlive = false;

    }



}
