using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void EndAnimAttack(){
        anim.SetBool("Attack", false);
    }
}
