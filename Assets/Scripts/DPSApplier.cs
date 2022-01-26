using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPSApplier : MonoBehaviour
{
    public float delay = 2;
    public int damage = 5;
    public float applyDmgEveryNSeconds = 2;
    public int applyDmgNTimes = 5;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();

            if (enemy.hitPoints > 0)
            {
                StartCoroutine(damagePerSecond(enemy));
            }
        }

        if (collider.tag == "Player")
        {
            AddPlayerController player = collider.gameObject.GetComponent<AddPlayerController>();
        }
    }

    IEnumerator damagePerSecond(EnemyController enemy)
    {
        yield return new WaitForSeconds(delay);

        int _applyDmgNTimes = applyDmgNTimes;
        int _appliedTimes = 0;

        while (_appliedTimes < _applyDmgNTimes)
        {
            enemy.substractHitpoints(damage);
            yield return new WaitForSeconds(applyDmgEveryNSeconds);
            _appliedTimes++;
        }

        Destroy(this);
    }
}
