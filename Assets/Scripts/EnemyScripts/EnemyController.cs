using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float maxHitPoints = 100;
    public float hitPoints = 1;

    private Slider healthBar;
    private int spellDamage;
    private Canvas enemyWorldSpace;
    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxHitPoints;

        Slider findHealthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();

        enemyWorldSpace = GameObject.FindWithTag("EnemyWorldSpace").GetComponent<Canvas>();

        healthBar = Instantiate(findHealthBar, gameObject.transform.position + Vector3.up * 2, Quaternion.identity) as Slider;
        healthBar.transform.SetParent(enemyWorldSpace.transform);

        healthBar.value = maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        hitPoints = Mathf.Clamp(hitPoints, 0, maxHitPoints);
        healthBar.value = hitPoints;

        if(hitPoints <= 0) {
            Destroy(gameObject);
            Destroy(healthBar.gameObject);
        }
    }



    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Spell")
        {
            if (hitPoints > 0)
            {
                spellDamage = collider.gameObject.GetComponent<DamageController>().damage;
                substractHitpoints(spellDamage);
            }
        }
    }

    void substractHitpoints(int amount)
    {
        hitPoints = hitPoints -= amount;
    }
}
