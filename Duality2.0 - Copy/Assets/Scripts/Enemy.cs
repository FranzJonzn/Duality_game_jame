using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyMaster master;
    public Rigidbody2D rig2D = null;
    [Space]
    [Space]
    public float speed = 20;
    [Space]
    [Space]

    public bool isAlive = true;
    public float lifeTimeMax = 30;
    public float lifeTimeMin = 12;
    [Space]
    [Space]
    public float damageOnInpact = 10f;

    [Space]
    [Space]
    public GameObject grafick;
    public GameObject grafick_up_diagonal;
    public GameObject grafick_down_diagonal;
    public GameObject grafick_side;
    [Space]
    [Space]
    public GameObject colidders;
    public Collider2D collider_up_diagonal;
    public Collider2D collider_down_diagonal;
    public Collider2D collider_side;

    Coroutine life;
    public void ActivateEnemy(EnemyMaster myMaster)
    {
        master = myMaster;

        life = StartCoroutine(LifeTime());
    }


    private void Update()
    {

        if (master != null)
        {

            Move();
        }
        if (!isAlive)
        {
            master.KillMe(this);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("her2");
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


        Oriante(dir);


        //move
        rig2D.velocity = dir * speed;
    }


    private void Oriante(Vector2 dir)
    {



        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



      

        Debug.Log(transform.eulerAngles.z);


        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 275)
        {
            grafick.transform.localScale = new Vector3(1, -1, 1);
            colidders.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            grafick.transform.localScale = new Vector3(1, 1, 1);
            colidders.transform.localScale = new Vector3(1, 1, 1);
        }

    }



    public IEnumerator LifeTime()
    {

        yield return new WaitForSeconds(Random.Range(lifeTimeMin,lifeTimeMax));

        isAlive = false;

    }



}
