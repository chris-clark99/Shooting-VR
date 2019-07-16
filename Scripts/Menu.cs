using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	[SerializeField]
	private UnityEngine.UI.Text highScore;

	void Start()
	{
		highScore.text = "Highscore: " + PlayerPrefs.GetInt("highscore");
	}
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene("Level1");
		}
    }
}
