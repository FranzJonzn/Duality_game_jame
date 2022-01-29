using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public Canvas _canvas;
    public GameObject prefab_enemy;
    public RectTransform bacground;
    public GameObject targetFlower;

    public float spawnDelayMin = 1f;
    public float spawnDelayMax = 10f;
    public List<GameObject> CurretnEnemys = new List<GameObject>();



    private Coroutine spawnerLogich;


    private Transform enemyBucket;
    private void Start()
    {


        enemyBucket = new GameObject().transform;
        enemyBucket.name = "enemyBucket";
        enemyBucket.position = new Vector3(0, 0, 0);
        enemyBucket.parent = _canvas.transform;
        spawnerLogich =  StartCoroutine(SpawnLogik());
  

    }

    private void Update()
    {



    }




    private void spwanEnemy()
    {

        Rect plane = UnderHodStuff.instance.playFeild;

        int random1 = (Random.Range(0f, 1f) > 0.5f) ? -1 : 1;
        int random2 = (Random.Range(0f, 1f) > 0.5f) ? -1 : 1;
        Vector2 pos;
        if ((Random.Range(0f, 1f) > 0.5f))
        {
             pos = (Vector2)bacground.position - new Vector2(Random.Range(0f, 1f) * plane.size.x * 0.5f, random2 * plane.size.y * 0.5f);
        }
        else
        {
            pos = (Vector2)bacground.position - new Vector2(random1 * plane.size.x * 0.5f, Random.Range(0f, 1f) * plane.size.y * 0.5f);
        }


      


     

        GameObject temp = Instantiate(prefab_enemy, pos, prefab_enemy.transform.rotation, enemyBucket);
        temp.GetComponent<Enemy>().ActivateEnemy(this);
        CurretnEnemys.Add(temp);
    }

    public void KillMe(Enemy toKill)
    {
        CurretnEnemys.Remove(toKill.gameObject);

        Destroy(toKill.gameObject);
    }


    public IEnumerator SpawnLogik()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
            spwanEnemy();
        }
    }

}
