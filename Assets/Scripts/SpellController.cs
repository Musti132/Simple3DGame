using UnityEngine;
using System.Collections;

/// ------------------------------
/// Creating instance of particles
/// ------------------------------
public class SpellController : MonoBehaviour
{
    public GameObject prefab;
    public KeyCode goHam;
    public float lifeTime = 20;
    public int damage = 15;
    public LayerMask layerMask;
    public float coolDown = 5;
    private bool _isOnCoolDown;
    void Awake()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(goHam) && !isOnCoolDown)
        {
            Vector3 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayCastHit;

            if (Physics.Raycast(ray, out rayCastHit, 30f, layerMask))
            {
                Explosion(rayCastHit.point + Vector3.up * 0.2f);
                //Debug.DrawLine(ray.origin, rayCastHit.point, Color.green);
                StartCoroutine(startCoolDownTimer());
            }
            else
            {
                print("OUT OF RANGE");
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.red);
            }
        }
        else if (!isOnCoolDown)
        {
            print("Spell on cooldown");
        }

    }

    public bool isOnCoolDown
    {
        get { return _isOnCoolDown; }
        set { _isOnCoolDown = value; }
    }

    private IEnumerator startCoolDownTimer()
    {
        isOnCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isOnCoolDown = false;
    }

    public void Explosion(Vector3 position)
    {
        instantiate(prefab, position);
    }

    private GameObject instantiate(GameObject prefab, Vector3 position)
    {
        GameObject spellObject = Instantiate(prefab, position, Quaternion.identity) as GameObject;

        DamageController damageController = spellObject.gameObject.AddComponent(typeof(DamageController)) as DamageController;
        damageController.damage = damage;

        Destroy(
            spellObject.gameObject,
            lifeTime
        );

        return spellObject;
    }

}