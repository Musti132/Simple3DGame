using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float maxHitPoints = 1000;
    private float hitPoints;
    private GameObject spell;
    public Image healthBar;
    private RectTransform health;
    private int spellDamage;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        healthBar.transform.localScale = new Vector2(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        hitPoints = Mathf.Clamp(hitPoints, 0, 100);
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
