using UnityEngine;
using System.Collections;

public class BackgroundScrolling : MonoBehaviour {

	private float xOffset;
	private float yOffset;

	public float horizontalCamScrollSpeed;
	public float verticalCamScrollSpeed;

	public float horizontalMoveSpeed;
	public float verticalMoveSpeed;

	void Start () {
		GetComponent<MeshRenderer> ().sortingOrder = -100;
	}
	

	void Update () {
		UpdateMovement ();

		float cameraX = - CameraXPosition () * horizontalCamScrollSpeed;
		float cameraY = - CameraYPosition () * verticalCamScrollSpeed;

		float x = Mathf.Repeat( cameraX + xOffset, 1);
		float y = Mathf.Repeat( cameraY + yOffset, 1);

		Vector2 newOffset = new Vector2 (x, y);

		foreach (Material m in GetComponent<MeshRenderer>().materials) {
			m.SetTextureOffset("_MainTex", newOffset);
		}
	}

	private void UpdateMovement() {
		xOffset += Mathf.Repeat(horizontalMoveSpeed * Time.deltaTime, 1);
		yOffset += Mathf.Repeat(verticalMoveSpeed * Time.deltaTime, 1);
	}

	private float CameraXPosition() {
		return Camera.main.transform.position.x;
	}

	private float CameraYPosition() {
		return Camera.main.transform.position.y;
	}
}
