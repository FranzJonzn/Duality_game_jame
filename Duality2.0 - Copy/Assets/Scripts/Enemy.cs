using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyMaster master;

    public float lifeTime = 12;
    public float damageOnInpact = 10f;
    public Coroutine life;

    public bool isAlive = true;


    private void Update()
    {
        if (!isAlive)
        {
            master.KillMe(this);
        }
    }

    public void ActivateEnemy(EnemyMaster myMaster)
    {
        master = myMaster;

        life = StartCoroutine(LifeTime());


    }


    public IEnumerator LifeTime()
    {

        yield return new WaitForSeconds(lifeTime);

        isAlive = false;

    }



}
