using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
	[SerializeField]
	private GameObject enemy;

	[SerializeField]
	private Animator recoil;

	[SerializeField]
	private RectTransform bar;

	[SerializeField]
	private UnityEngine.UI.Image hurt;

	[SerializeField]
	private UnityEngine.UI.Text scoreUI;

	private Vector2 rand;

	private RaycastHit hit;

	[SerializeField]
	private float spawnRate = 5f;

	[SerializeField]
	private float spawnIncrease = 0.1f;

	private int score = 0;

	private int health = 10;

	[SerializeField]
	private int enemyDamage = 1;

    void Start()
    {
		StartCoroutine("Spawn");
    }

    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			recoil.Play("Recoil");
			if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f))
			{
				if (hit.collider.tag == "Enemy")
				{
					hit.collider.tag = "Dead";
					score++;
					scoreUI.text = "Score: " + score;
					StartCoroutine(hit.transform.gameObject.GetComponent<Enemy>().Death());
				}
			}
		}
		if (health < 0)
		{
			if (score > PlayerPrefs.GetInt("highscore"))
			{
				PlayerPrefs.SetInt("highscore", score);
			}
			SceneManager.LoadScene("Menu");
		}
    }

	private IEnumerator Spawn()
	{
		while (true)
		{
			rand = Random.insideUnitCircle.normalized * 50f;
			Instantiate(enemy, new Vector3(rand.x, 1.2f, rand.y), Quaternion.identity);
			yield return new WaitForSeconds(spawnRate);
			if (spawnRate > 2f)
			{
				spawnRate -= spawnIncrease;
			}
		}
	}

	public IEnumerator Damage()
	{
		health -= enemyDamage;
		Color tempColor = hurt.color;
		for (int i = 0; i < 8; i++)
		{
			tempColor.a += 0.05f;
			hurt.color = tempColor;
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds(0.5f);
		for (int i = 0; i < 8; i++)
		{
			tempColor.a -= 0.05f;
			hurt.color = tempColor;
			yield return new WaitForSeconds(0.01f);
		}
		bar.sizeDelta = new Vector2(health * 20, 10);
	}

}