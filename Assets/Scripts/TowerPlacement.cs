using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowerPlacement : MonoBehaviour
{
    TowerTypeInfo selectedTowerType;
    GameObject selectedTowerModelInstance;
    public Camera cm;
    public Vector3 placementOffset;
    Material ghostMat;
    public int cash;
    public TextMeshProUGUI cashText;
    public ToggleGroup togs;
    public LayerMask terrain;
    public Transform towerParent;
    // Start is called before the first frame update
    void Start()
    {
        selectedTowerType = null;
        changeCashAmount(0);
    }
    public void setTowerType(TowerTypeInfo newTower)
    {
        AudioManager.instance.Play("ButtonClick");
        if (newTower.tog.isOn)
        {
            selectedTowerType = newTower;
            Destroy(selectedTowerModelInstance);
            selectedTowerModelInstance = Instantiate(newTower.ghost, towerParent);
            selectedTowerModelInstance.active = false;
        }
        else
        {
            selectedTowerType.tog.isOn = false;
            selectedTowerType = null;
            Destroy(selectedTowerModelInstance);
            selectedTowerModelInstance = null;
        }
    }
    
    public bool changeCashAmount(int toSpend)
    {
        cash -= toSpend;
        cashText.text = cash.ToString();

        foreach(TowerTypeInfo t in togs.GetComponentsInChildren<TowerTypeInfo>())
        {
            
            if (t.cost > cash)
            {
                t.tog.interactable =false;
                
            }
            else
            {
                t.tog.interactable = true;
            }
        }
        if (selectedTowerType&&selectedTowerType.cost > cash)
        {
            selectedTowerType.tog.isOn = false;
            selectedTowerType = null;
            Destroy(selectedTowerModelInstance);
            selectedTowerModelInstance=null;
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (selectedTowerType&&selectedTowerModelInstance)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cm.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin,ray.direction, out hit,Mathf.Infinity,terrain))
                {
                    print(LayerMask.LayerToName(hit.collider.gameObject.layer));
                    var spot = hit.collider.GetComponent<TowerSpot>();
                    if (spot && !spot.occupied)
                    {
                        if (cash>=selectedTowerType.cost){ 
                            
                            spot.occupied = true;
                            print("tower placed");
                            AudioManager.instance.PlaySoundAtPoint("PlaceTower", spot.transform.position);
                            var tower=Instantiate(selectedTowerType.prefab, spot.transform.position + placementOffset, spot.transform.rotation);
                            tower.transform.parent = towerParent;
                            changeCashAmount(selectedTowerType.cost);
                        }
                    }
                }
                else
                {
                    print("ray failure");
                }
            }
            else
            {
                Ray ray = cm.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, terrain))
                {
                    var spot = hit.collider.GetComponent<TowerSpot>();
                    if (spot && !spot.occupied)
                    {
                       
                            selectedTowerModelInstance.active = true;
                            selectedTowerModelInstance.transform.position = spot.transform.position + placementOffset;
                            selectedTowerModelInstance.transform.rotation = spot.transform.rotation;
                        
                    }
                    else
                    {
                        selectedTowerModelInstance.active = false;
                    }
                }
                else
                {
                    selectedTowerModelInstance.active = false;
                }
            }
        }
    }
}
