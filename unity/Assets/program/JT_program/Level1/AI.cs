using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

	//	float dir = 1f;

	//	Collider myCollider;

	RaycastHit hit;

	float Distance;

	bool bo = true;



	void Start ()
	{
//		myCollider = transform.GetComponent<Collider> ();

		Distance = transform.localScale.x / 4;
	}


	void FixedUpdate ()
	{
		//上
		if (Physics.Raycast (transform.position, transform.up, out hit, Distance)) {

			transform.Rotate (Vector3.forward * Random.Range (0, 90));

			bo = !bo;
		}

		//下
		if (Physics.Raycast (transform.position, -transform.up, out hit, Distance)) {
			

		}

		//左
		if (Physics.Raycast (transform.position, -transform.right, out hit, Distance)) {

			transform.Rotate (Vector3.forward * Random.Range (0, 90));

			bo = !bo;

		}

		//右
		if (Physics.Raycast (transform.position, transform.right, out hit, Distance)) {

			transform.Rotate (Vector3.forward * Random.Range (0, 90));

			bo = !bo;

		}
			
		transform.Rotate (Vector3.forward);

		transform.position += transform.up * Time.deltaTime;
	}


	void OnDrawGizmos ()
	{
		Gizmos.DrawRay (transform.position, transform.right * transform.localScale.x / 4);//右
		Gizmos.DrawRay (transform.position, -transform.right * transform.localScale.x / 4);
		Gizmos.DrawRay (transform.position, transform.up * transform.localScale.y / 4);
		Gizmos.DrawRay (transform.position, -transform.up * transform.localScale.y / 4);
	}
}
