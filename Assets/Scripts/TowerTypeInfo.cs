using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TowerTypeInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject prefab;
    public GameObject ghost;
    public int cost;
    public string name;
    public Toggle tog;
    public GameObject toolTip;
    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
    }
}
