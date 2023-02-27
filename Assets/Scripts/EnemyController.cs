using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth;
    public float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <=0 )
        {
            Debug.Log("Mati");
            Destroy(gameObject);
        }
        transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage){
        if (enemyHealth > 0)
        {
            enemyHealth -= damage;
        }

        Debug.Log("Kena damage");
    }
}
