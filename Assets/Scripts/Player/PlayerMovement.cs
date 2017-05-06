using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6;

	private Vector3 movement;
	private Animator anim;
	private Rigidbody rb;
	private int floorMask;
	private float rayLength = 100;

	void Awake() {
		rb = GetComponent<Rigidbody> ();
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();

	}

	void FixedUpdate () {
		float x = Input.GetAxisRaw ("Horizontal");
		float z = Input.GetAxisRaw ("Vertical");

		movement.Set (x, 0, z);
		movement = movement.normalized * speed * Time.deltaTime;
		rb.MovePosition (rb.transform.position + movement);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, rayLength, floorMask)) {
			Vector3 playerToMouse = hit.point - rb.transform.position;
			playerToMouse.y = 0;

			Quaternion rotation = Quaternion.LookRotation (playerToMouse);
			rb.MoveRotation(rotation);
		}

		bool walking = x != 0 || z != 0;
		anim.SetBool ("IsWalking", walking);
	}

	void LateUpdate () {

	}
}
