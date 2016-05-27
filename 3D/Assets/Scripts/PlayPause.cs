using UnityEngine;
using System.Collections;

public class PlayPause : MonoBehaviour {
	
	public MediaPlayerCtrl scrMedia;
	bool isPlaying= true;
	public GameObject play_btn_Left;
	public GameObject play_btn_Right;

	void Start(){
	
		// Make Play button transparent
		Color color_Left = play_btn_Left.GetComponent<Renderer>().material.color;
		Color color_Right = play_btn_Left.GetComponent<Renderer>().material.color;
		color_Left.a = 0.0f;
		color_Right.a = 0.0f;
		play_btn_Left.GetComponent<Renderer> ().material.color = color_Left;
		play_btn_Right.GetComponent<Renderer> ().material.color = color_Right;

	}
	void Update () {
		
		// Touch screen at any position or left mouse button on any position
		if (Input.GetMouseButtonDown (0)) {

			// abspielen
			if (!isPlaying) {
				scrMedia.Play ();
				Debug.Log ("play");

				StartCoroutine (FadeTo (0.0f, 0.3f));
				isPlaying = true;
			} 

			// pause
			else if (isPlaying) {
				scrMedia.Pause();
				Debug.Log ("pause");

				play_btn_Left.SetActive (true);
				play_btn_Right.SetActive (true);
				StartCoroutine (FadeTo (1.0f, 0.3f));
				isPlaying = false;
			}
		}

		// Quit the application on Android back button
	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit (); 
		}


	}

	// Fade-Animation des Alpha-Kanals
	IEnumerator FadeTo(float aValue, float aTime){
		float alpha = play_btn_Left.GetComponent<Renderer> ().material.color.a;
		bool isfadingOut = false;
		if (alpha > 0.5f) 
			isfadingOut = true;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			play_btn_Left.GetComponent<Renderer> ().material.color = newColor;
			play_btn_Right.GetComponent<Renderer> ().material.color = newColor;

			// Lerp does never reach 1 or 0, so the button will be disabled when opacity is almost 0
			if (isfadingOut == true && play_btn_Left.GetComponent<Renderer> ().material.color.a < 0.1f) {
				play_btn_Left.SetActive (false);
				play_btn_Right.SetActive (false);
				isfadingOut = false;
			}
			yield return null;
		}
	}
}