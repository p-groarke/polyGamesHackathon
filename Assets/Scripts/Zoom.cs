using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	int zoom = 3;
	float defaultFov = 4.5f;
	int smoothness = 5;
	bool isZoomed = false;
	
	public int cameraBackgroundInitialPositionX = 0;
	public int cameraBackgroundInitialPositionY = 30;
	public int cameraBackgroundInitialPositionZ = -9;

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
			GameObject objectToFollow = GameObject.Find ("Quad1");
			Vector3 vecteurObjectToFollow = objectToFollow.transform.position;
			vecteurObjectToFollow.x = /*Mathf.Lerp(camera.transform.position.x,vecteurObjectToFollow.x,1) + */vecteurObjectToFollow.x +cameraBackgroundInitialPositionX;
			vecteurObjectToFollow.y = /*Mathf.Lerp(camera.transform.position.y,vecteurObjectToFollow.y,1) + */vecteurObjectToFollow.y + cameraBackgroundInitialPositionY;
			vecteurObjectToFollow.z = cameraBackgroundInitialPositionZ;
			camera.transform.position = vecteurObjectToFollow;
		}
		else{
			camera.orthographicSize = Mathf.Lerp(camera.orthographicSize,defaultFov,Time.deltaTime*smoothness);
			Vector3 vecteurPositionCameraBackground;
			vecteurPositionCameraBackground.x = Mathf.Lerp(camera.transform.position.x,cameraBackgroundInitialPositionX,Time.deltaTime*smoothness);
			vecteurPositionCameraBackground.y = Mathf.Lerp(camera.transform.position.y,cameraBackgroundInitialPositionY,Time.deltaTime*smoothness);
			vecteurPositionCameraBackground.z = cameraBackgroundInitialPositionZ;
			camera.transform.position = vecteurPositionCameraBackground;
		} 
	}
}
