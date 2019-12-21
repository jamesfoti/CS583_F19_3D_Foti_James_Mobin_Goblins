using UnityEngine;
using System.Collections;

public class TowerTrigger : MonoBehaviour {

	public Tower twr;    
    public bool lockE;
	public GameObject curTarget;
    


    void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("enemyBug") && !lockE)
		{
			twr.target = other.gameObject.transform;            
            curTarget = other.gameObject;
			lockE = true;
		}
       
    }
	void Update()
	{
        if (curTarget)
        {
            if (curTarget.CompareTag("Dead")) // get it from EnemyHealth
            {
                lockE = false;
                twr.target = null;
				Collider[] possibleTargets = Physics.OverlapSphere(twr.transform.position, twr.GetComponentInChildren<SphereCollider>().radius);
				int i = 0;
				while (twr.target == null && i < possibleTargets.Length-1)
				{
					if (possibleTargets[i].CompareTag("enemyBug"))
					{
						twr.target = possibleTargets[i].gameObject.transform;
						curTarget = possibleTargets[i].gameObject;
						lockE = true;
					}
					else if(i < possibleTargets.Length-1)
					{
						i++;
					}
				}
				possibleTargets = null;
            }
        }




        if (!curTarget) 
		{
			lockE = false;            
        }
	}
	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("enemyBug") && other.gameObject == curTarget)
		{
			lockE = false;
            twr.target = null;            
        }
	}
	
}
