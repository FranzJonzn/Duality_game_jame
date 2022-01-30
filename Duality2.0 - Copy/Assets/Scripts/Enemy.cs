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
    [Tooltip("har den här pga dödar fienden här")]
    public AudioManager soundManager;
    [Space]
    [Space]
    //public GameObject colidders;
    //public Collider2D collider_up_diagonal;
    //public Collider2D collider_down_diagonal;
    //public Collider2D collider_side;
    //[Space]
    public bool mainFLowerDerad = false;
    Coroutine life;
    public void ActivateEnemy(EnemyMaster myMaster)
    {
        master = myMaster;

        life = StartCoroutine(LifeTime());
    }

    bool starteSOund = false;
    private void Update()
    {

        if (master != null)
        {

            Move();
        }
        if (!isAlive && !starteSOund)
        {
            starteSOund = true;
            StartCoroutine(whaitForSOund());

        }
    }
    bool playsound = true;
    float delay = 4f;
    private IEnumerator cooldow()
    {
        yield return new WaitForSeconds(Random.Range(4, 20));
        playsound = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
 
      
          
 
   


        MainFlowerBrain brain = collision.gameObject.GetComponent<MainFlowerBrain>();
        if (brain != null)
        {

            soundManager.PlayOnBorn();

            brain.Damage(damageOnInpact);
            StopCoroutine(life);
            isAlive = false;
        }
        else if (playsound && Random.Range(-2, 34) < 0)
        {
            soundManager.PlayOnBorn();
            StartCoroutine(cooldow());
            playsound = false;
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


        if (dir.x < 0)// höger sida
        {

            grafick.transform.localScale   = new Vector3(1, -1, 1);


        }
        else //vänster sida
        {

            grafick.transform.localScale   = new Vector3(1, 1, 1);
  
        }



    }



    public IEnumerator LifeTime()
    {

        yield return new WaitForSeconds(Random.Range(lifeTimeMin,lifeTimeMax));

        UnderHodStuff.instance.starevSlugs += 1;

        isAlive = mainFLowerDerad; // låt snigeln fortsätta leva efter tiden gåt ut så den kan åka av skärmen
    


    }






    public IEnumerator whaitForSOund()
    {
        soundManager.PlayOnDeath();

        grafick.SetActive(false);

        yield return new WaitForSeconds( lifeTimeMax);

        UnderHodStuff.instance.starevSlugs += 1;

        isAlive = mainFLowerDerad; // låt snigeln fortsätta leva efter tiden gåt ut så den kan åka av skärmen



        if (!isAlive)
        {
            master.KillMe(this);
        }
    }

}
