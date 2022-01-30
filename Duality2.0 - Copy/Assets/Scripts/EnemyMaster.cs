using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{

    public GameObject prefab_enemy;
    public GameObject targetFlower;
 
    public float spawnDelayMin = 1f;
    public float spawnDelayMax = 10f;
    public List<GameObject> CurretnEnemys = new List<GameObject>();



    private Coroutine spawnerLogich;

    MainFlowerBrain mb ;
    private bool callAllSoldiersBack = false;

    private Transform enemyBucket;
    private void Start()
    {
        mb = targetFlower.GetComponent<MainFlowerBrain>();

        enemyBucket = new GameObject().transform;
        enemyBucket.name = "enemyBucket";
        enemyBucket.position = new Vector3(0, 0, 0);
 
        spawnerLogich =  StartCoroutine(SpawnLogik());
  

    }

    private void Update()
    {

        


        if (mb.pDead && !callAllSoldiersBack)
        {
            callAllSoldiersBack = true;
            foreach(GameObject gb in CurretnEnemys)
            {
                gb.GetComponent<Enemy>().mainFLowerDerad = true;
            }
        }


    }




    private void spwanEnemy()
    {

        Rect plane = UnderHodStuff.instance.playFeild;

        int random1 = (Random.Range(0f, 1f) > 0.5f) ? -1 : 1;
        int random2 = (Random.Range(0f, 1f) > 0.5f) ? -1 : 1;
        Vector2 pos;
        if ((Random.Range(0f, 1f) > 0.5f))
        {
             pos = Vector2.zero - new Vector2(Random.Range(0f, 1f) * plane.size.x * 0.5f, random2 * plane.size.y * 0.5f);
        }
        else
        {
            pos = Vector2.zero - new Vector2(random1 * plane.size.x * 0.5f, Random.Range(0f, 1f) * plane.size.y * 0.5f);
        }


      


     

        GameObject temp = Instantiate(prefab_enemy, pos, prefab_enemy.transform.rotation, enemyBucket);
        temp.GetComponent<Enemy>().ActivateEnemy(this);
        temp.GetComponent<Enemy>().mainFLowerDerad = mb.pDead;
        CurretnEnemys.Add(temp);
    }

    public void KillMe(Enemy toKill)
    {



        CurretnEnemys.Remove(toKill.gameObject);

        Destroy(toKill.gameObject);
    }


    public IEnumerator SpawnLogik()
    {

        while (!mb.pDead)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));

            yield return null;
            //pausa fiender när man pausat
            if (Time.timeScale != 0)
                spwanEnemy();
        }
    }

}
