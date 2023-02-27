using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    private float timeBtwAttack;
    [SerializeField] private float startAttack;

    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRadius;
    public float damage;

    [SerializeField] Animator anim;

    private void Update() {
        if (timeBtwAttack <= 0){

            if(Input.GetKeyDown(KeyCode.L))
            {
                anim.SetBool("Attack", true);
                Collider2D [] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackableLayer);

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<EnemyController>().TakeDamage(damage);
                }
            }

            timeBtwAttack = startAttack;
        }
        else
        {
            
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void EndAttack(){
        anim.SetBool("Attack", false);
    }
}
