using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFlowerBrain : MonoBehaviour
{

    public Canvas _canvas;
    public Slider _lifeBar;
    public float life = 100f;
    public GameObject prefab_flower;

    public LayerMask floweMask;

    public List<GameObject> ActiveFlowers = new List<GameObject>();
    public List<GameObject> DeadFLowers = new List<GameObject>();

    private Transform flowerContaintger;
    private void Start()
    {

        flowerContaintger = new GameObject().transform;
        flowerContaintger.name = "lowerBucket";
        flowerContaintger.position = new Vector3(0, 0, 0);
        flowerContaintger.parent = _canvas.transform;
        _lifeBar.maxValue = 100f;
        _lifeBar.value = 100f;
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newFlowe = Instantiate(prefab_flower, Input.mousePosition, prefab_flower.transform.rotation, flowerContaintger); //, Quaternion.identity);
            newFlowe.GetComponent<AttackFlower>().ActiveFLower(this);
            ActiveFlowers.Add(newFlowe);

        }
        else if (Input.GetMouseButtonDown(1))
        {


            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, Mathf.Infinity, floweMask.value);
  

            if (hit)
            {
                Debug.Log("here");
                DeadFLowers.Add(hit.collider.gameObject);
                ActiveFlowers.Remove(hit.collider.gameObject);
                life += hit.collider.GetComponent<AttackFlower>().Kill();
            }
            

        }


    }

    public void Damage(float damage)
    {

        life -= damage;
        _lifeBar.value = life;
    }





}
