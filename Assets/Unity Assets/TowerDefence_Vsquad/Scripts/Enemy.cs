﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    
    public Transform shootElement;
    public GameObject bullet;
    public GameObject Enemybug;
    public int Creature_Damage = 10;    
    public float Speed;
    // 
    public Transform[] waypoints;
    int curWaypointIndex = 0;
    public float previous_Speed;
    public Animator anim;
    public EnemyHp Enemy_Hp;
    public Transform target;
    public GameObject EnemyTarget;
    public int bounty;
    [HideInInspector]
    public EnemySpawner spawner;
    void Start()
    {            
        anim = GetComponent<Animator>();
        anim.SetBool("Run Forward", true);
        Enemy_Hp = Enemybug.GetComponent<EnemyHp>();
        previous_Speed = Speed;        
    }

    // Attack

    void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Castle")
        {

            if (Speed != 0)
            {
                spawner.killEnemy();
                AudioManager.instance.Play("CastleDamage");
            }
            Speed = 0;
            EnemyTarget = other.gameObject;
            target = other.gameObject.transform;
            Vector3 targetPosition = new Vector3(EnemyTarget.transform.position.x, transform.position.y, EnemyTarget.transform.position.z);            
            transform.LookAt(targetPosition);
            FindObjectOfType<PlayerHealth>().takeDamage(1);
            Destroy(gameObject);
        }

    }

    // Attack
    void Shooting ()
    {
        //if (EnemyTarget)
       // {           
            GameObject с = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
            с.GetComponent<EnemyBullet>().target = target;
            с.GetComponent<EnemyBullet>().twr = this;
       // }  

    }

    

    void GetDamage ()

    {        
            EnemyTarget.GetComponent<TowerHP>().Dmg_2(Creature_Damage);       
    }

    


    void Update () 
	{

        
        //Debug.Log("Animator  " + anim);


        // MOVING

        if (curWaypointIndex < waypoints.Length){
	transform.position = Vector3.MoveTowards(transform.position,waypoints[curWaypointIndex].position,Time.deltaTime*Speed);
            
            if (!EnemyTarget)
            {
                transform.LookAt(waypoints[curWaypointIndex].position);
            }
	
	if(Vector3.Distance(transform.position,waypoints[curWaypointIndex].position) < 0.5f)
	{
		curWaypointIndex++;
	}    
	}          

        else
        {
            ;
        }

        // DEATH

        if (Enemy_Hp.EnemyHP <= 0)
        {
            if (Speed != 0)
            {
                TowerPlacement t = FindObjectOfType<TowerPlacement>();
                spawner.killEnemy();
                if (t != null)
                    t.changeCashAmount(bounty);
            }
            Speed = 0;
            
            Destroy(gameObject, 1.75f);
            anim.SetBool("Run Forward", false);
            anim.SetTrigger("Die");            
        }

        // Attack to Run
                

        if (EnemyTarget)        {

          
            if (EnemyTarget.CompareTag("Castle_Destroyed")) // get it from BuildingHp
            {
                Speed = previous_Speed;               
                EnemyTarget = null;                
            }
        }


    }
       
   
}

