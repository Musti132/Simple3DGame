using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpellController : MonoBehaviour
{
    public GameObject prefab;
    public KeyCode goHam;
    public float lifeTime = 20;
	public int damage = 15;
    public LayerMask layerMask;
    void Awake()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(goHam))
        {
			Vector3 mousePos = Input.mousePosition;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayCastHit;

            if (Physics.Raycast(ray, out rayCastHit, 20f, layerMask))
            {
                Explosion(rayCastHit.point + Vector3.up * 0.2f);
                Debug.DrawLine(ray.origin, rayCastHit.point, Color.green);
            }
            else
            {
                print("OUT OF RANGE");
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.red);
            }
		}

    }
    public void Explosion(Vector3 position)
    {
        instantiate(prefab, position);
    }

    private GameObject instantiate(GameObject prefab, Vector3 position)
    {
        GameObject newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as GameObject;

        newParticleSystem.tag = "Spell";
        DamageController damageController = newParticleSystem.gameObject.AddComponent(typeof(DamageController)) as DamageController;
        damageController.damage = damage;

        SphereCollider sphereCollider = newParticleSystem.gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;

        Destroy(
            newParticleSystem.gameObject,
            lifeTime
        );

        return newParticleSystem;
    }



}