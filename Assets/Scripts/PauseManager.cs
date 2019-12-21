using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool paused = false;
    public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }
    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        GetComponent<TowerPlacement>().enabled = false;
    }
    public void unPause()
    {
        paused = false;
        
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        GetComponent<TowerPlacement>().enabled = true;
    }
}
