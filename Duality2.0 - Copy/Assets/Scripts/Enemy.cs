using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum currentDirr {  UP, DOWN, SIDE, NON}
    currentDirr curretD = currentDirr.NON;

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
    [Space]
    public bool mainFLowerDerad = false;
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

        if (mainFLowerDerad)
            dir = -dir;

        Oriante(dir);


        //move
        rig2D.velocity = dir * speed;
    }


    private void Oriante(Vector2 dir)
    {



        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float angleForSIde = 0.2f;

        if (dir.x < 0)// höger sida
        {

            grafick.transform.localScale   = new Vector3(1, -1, 1);
            colidders.transform.localScale = new Vector3(1, -1, 1);

        }
        else //vänster sida
        {

            grafick.transform.localScale   = new Vector3(1, 1, 1);
            colidders.transform.localScale = new Vector3(1, 1, 1);
  
        }

        collider_side.gameObject.SetActive(true);

        if ((dir.y < angleForSIde && dir.y > -angleForSIde))
        {
            if (curretD != currentDirr.SIDE)
            {
                grafick_down_diagonal.SetActive(false);
                grafick_up_diagonal.SetActive(false);
                //collider_down_diagonal.gameObject.SetActive(false);
                //collider_up_diagonal.gameObject.SetActive(false);



                grafick_side.SetActive(true);
                //collider_side.gameObject.SetActive(true);
                curretD = currentDirr.SIDE;
            }
        }
        else if (dir.y < 0)
        {

            if (curretD != currentDirr.DOWN)
            {
                grafick_up_diagonal.SetActive(false);
                grafick_side.SetActive(false);
                //collider_up_diagonal.gameObject.SetActive(false);
                //collider_side.gameObject.SetActive(false);

                grafick_down_diagonal.SetActive(true);
                //collider_down_diagonal.gameObject.SetActive(true);

                curretD = currentDirr.DOWN;
            }
        }
        else
        {

            if(curretD != currentDirr.UP)
            {
                grafick_down_diagonal.SetActive(false);
                grafick_side.SetActive(false);
                //collider_side.gameObject.SetActive(false);


                grafick_up_diagonal.SetActive(true);
                //collider_up_diagonal.gameObject.SetActive(true);

                curretD = currentDirr.UP;
            }

  

        }





        //Debug.Log(transform.eulerAngles.z);


        //if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 275)
        //{
        //    grafick.transform.localScale = new Vector3(1, -1, 1);
        //    colidders.transform.localScale = new Vector3(1, -1, 1);
        //}
        //else
        //{
        //    grafick.transform.localScale = new Vector3(1, 1, 1);
        //    colidders.transform.localScale = new Vector3(1, 1, 1);
        //}

    }



    public IEnumerator LifeTime()
    {

        yield return new WaitForSeconds(Random.Range(lifeTimeMin,lifeTimeMax));

        UnderHodStuff.instance.starevSlugs += 1;

        isAlive = mainFLowerDerad; // låt snigeln fortsätta leva efter tiden gåt ut så den kan åka av skärmen

    }



}
