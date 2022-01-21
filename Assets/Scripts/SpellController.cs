using UnityEngine;

/// ------------------------------
/// Creating instance of particles
/// ------------------------------
public class SpellController : MonoBehaviour
{
    /// ------------------------------
    /// Singleton
    /// ------------------------------
    public static SpellController Instance;

    public ParticleSystem effectA;

    public KeyCode goHam;

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
                Explosion(rayCastHit.point + Vector3.up * 1.2f);
                Debug.DrawLine(ray.origin, rayCastHit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.red);
            }
		}

    }
    public void Explosion(Vector3 position)
    {
        instantiate(effectA, position);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;

        Destroy(
            newParticleSystem.gameObject,
            newParticleSystem.startLifetime
        );

        return newParticleSystem;
    }



}