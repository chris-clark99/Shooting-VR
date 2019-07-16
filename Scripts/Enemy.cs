using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private RaycastHit hit;

	private bool alive = true;

	private bool active = true;

	[SerializeField]
	private Rigidbody rb;

	private Transform target;

	[SerializeField]
	private float speed = 5f;

	void Start()
	{
		target = Camera.main.transform;
	}
		
	void FixedUpdate()
	{
		if ((Vector3.Distance(gameObject.transform.position, target.position) > 5) && (alive == true))
		{
			transform.LookAt(new Vector3(target.position.x, 1.2f, target.position.z));
			rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
		}
		else if ((Vector3.Distance(gameObject.transform.position, target.position) <= 5) && (active == true))
		{
			StartCoroutine("Wait");
			active = false;
		}
		if (alive)
		{
			rb.velocity = Vector3.zero;
			rb.Sleep();
		}
	}
	
	public IEnumerator Death()
	{
		alive = false;
		rb.AddRelativeForce(Vector3.back * 200, ForceMode.Force);
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}

	private IEnumerator Wait()
	{
		while (alive)
		{
			target.gameObject.GetComponent<Main>().StartCoroutine("Damage");
			yield return new WaitForSeconds(2f);
		}
	}
}