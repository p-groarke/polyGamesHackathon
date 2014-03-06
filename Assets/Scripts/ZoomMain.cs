using UnityEngine;
using System.Collections;

public class ZoomMain : MonoBehaviour {
	
	int zoom = 3;
	float defaultFov = 4.5f;
	int smoothness = 5;
	bool isZoomed = false;

	public string characterToFollow;

	public int cameraInitialPositionX = 0;
	public int cameraInitialPositionY = 0;
	public int cameraInitialPositionZ = -10;

	public float minX = 2.5f;
	public float minY = 1.5f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse1)){
			isZoomed=!isZoomed;
		}
		if(isZoomed==true){
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize,zoom,Time.deltaTime*smoothness);
			GameObject objectToFollow = GameObject.Find (characterToFollow);
			Vector3 vecteurObjectToFollow = objectToFollow.transform.position;
			vecteurObjectToFollow.x = Mathf.Lerp(camera.transform.position.x,vecteurObjectToFollow.x,Time.deltaTime*smoothness);
			vecteurObjectToFollow.y = Mathf.Lerp(camera.transform.position.y,vecteurObjectToFollow.y,Time.deltaTime*smoothness);
			vecteurObjectToFollow.z = cameraInitialPositionZ;

			vecteurObjectToFollow.x = Mathf.Clamp (vecteurObjectToFollow.x, -2.5f, 2.5f);
			vecteurObjectToFollow.y = Mathf.Clamp (vecteurObjectToFollow.y, -1.5f, 1.5f);

			camera.transform.position = vecteurObjectToFollow;
		}
		else{
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize,defaultFov,Time.deltaTime*smoothness);
			Vector3 vecteurPositionCamera;
			vecteurPositionCamera.x = Mathf.Lerp(camera.transform.position.x,cameraInitialPositionX,Time.deltaTime*smoothness);
			vecteurPositionCamera.y = Mathf.Lerp(camera.transform.position.y,cameraInitialPositionY,Time.deltaTime*smoothness);
			vecteurPositionCamera.z = cameraInitialPositionZ;
			camera.transform.position = vecteurPositionCamera;
		} 
	}
}
