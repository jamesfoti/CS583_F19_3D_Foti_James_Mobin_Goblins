using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
	public static GameObject HUD_Panel;
	public static GameObject Win_Panel;
	public static int loser;
	public GameObject Lose_Panel;
	public GameObject Pause_Panel;
	public int currentHealth;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
		HUD_Panel.SetActive(true);
		Lose_Panel.SetActive(false);
		Win_Panel.SetActive(false);
		Pause_Panel.SetActive(false);
        takeDamage(0);
    }

	void Awake()
	{
		HUD_Panel = GameObject.Find("HUDPanel");
		Lose_Panel = GameObject.Find("LosePanel");
		Win_Panel = GameObject.Find("WinPanel");
		Pause_Panel = GameObject.Find("PausePanel");
		loser = 0;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 1)
            Lose();
        healthText.text = currentHealth.ToString();
    }
    public void Lose()
	{
		loser = 1;
		Debug.Log("Player lost!");
		HUD_Panel.SetActive(false);
		Lose_Panel.SetActive(true);
	}
	public void Restart()
	{
		Debug.Log("Restarting level.");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void MainMenu()
	{
		Debug.Log("Going to main menu.");
		SceneManager.LoadScene(0);
	}
	public static void Win()
	{
		if (loser == 0) {
			Debug.Log("Player won!");
			HUD_Panel.SetActive(false);
			Win_Panel.SetActive(true);
		}
	}
}
