using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int hitPoints = 100;
    public GameObject enemy;
    public KeyCode spawnKey;
    public LayerMask layerMask;

    private GameObject[] spawnPoints;
    private int index;
    void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");

    }

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayCastHit;

            if (Physics.Raycast(ray, out rayCastHit, 20f, layerMask))
            {
                index = Random.Range(0, spawnPoints.Length);

                GameObject spawnPointPosition = spawnPoints[index];
                spawnEnemy(rayCastHit.point);
                Debug.DrawLine(ray.origin, rayCastHit.point, Color.green);
            }
            else
            {
                print("OUT OF RANGE");
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.red);
            }
        }

    }
    public void spawnEnemy(Vector3 position)
    {
        instantiate(enemy, position);
    }

    private GameObject instantiate(GameObject prefab, Vector3 position)
    {
        GameObject newEnemy = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        
        newEnemy.layer = 7;
        newEnemy.tag = "Enemy";

        EnemyController damageController = newEnemy.gameObject.AddComponent(typeof(EnemyController)) as EnemyController;
        damageController.maxHitPoints = hitPoints;
        

        SphereCollider sphereCollider = newEnemy.gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 2;

        Rigidbody rb = newEnemy.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.isKinematic = true;
        
        return newEnemy;
    }
}
