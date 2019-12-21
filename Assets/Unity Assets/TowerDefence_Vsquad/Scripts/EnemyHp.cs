using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour {

    public int EnemyHP = 30;
    public Animator anim;
    public void Dmg(int DMGcount)
    {
        EnemyHP -= DMGcount;
        AudioManager.instance.PlaySoundAtPoint("EnemyDamage", transform.position);
        //anim.SetTrigger("Take Damage");
    }

    private void Update()
    {        

        if (EnemyHP <= 0)
        {
            gameObject.tag = "Dead"; // send it to TowerTrigger to stop the shooting
           
        }
    }
    
}
