using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{

    public GameObject prefab_enemy;


    public List<GameObject> CurretnEnemys = new List<GameObject>();






    public void KillMe(Enemy toKill)
    {
        CurretnEnemys.Remove(toKill.gameObject);

        Destroy(toKill.gameObject);
    }

}
