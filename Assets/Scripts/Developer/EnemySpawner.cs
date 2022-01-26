using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int hitPoints = 100;
    public GameObject enemy;
    public KeyCode spawnKey;
    public LayerMask layerMask;
    void Awake()
    {
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

        EnemyController damageController = newEnemy.gameObject.AddComponent(typeof(EnemyController)) as EnemyController;
        damageController.maxHitPoints = hitPoints;
        

        SphereCollider sphereCollider = newEnemy.gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 6;

        Rigidbody rb = newEnemy.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.isKinematic = true;
        
        return newEnemy;
    }
}
