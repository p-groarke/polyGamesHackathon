using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	private bool m_isPaused = false;
	private bool m_isDead = false;
	private GUIStyle m_BackgroundStyle = new GUIStyle();		// Style for background tiling
	private Texture2D m_FadeTexture;				// 1x1 pixel texture used for fading
	private Color m_startScreenColor = new Color(0,0,0,0);
	private Color m_CurrentScreenOverlayColor = new Color(0,0,0,0);	// default starting color: black and fully transparrent
	private Color m_TargetScreenOverlayColor = new Color(0,0,0,0);	// default target color: black and fully transparrent
	private Color m_DeltaColor = new Color(0,0,0,0);		// the delta-color is basically the "speed / second" at which the current color should change
	private int m_FadeGUIDepth = -1000;				// make sure this texture is drawn on top of everything
	
	
	// initialize the texture, background-style and initial color:
	private void Awake()
	{		
		m_FadeTexture = new Texture2D(1, 1);        
		m_BackgroundStyle.normal.background = m_FadeTexture;
		SetScreenOverlayColor(m_CurrentScreenOverlayColor);
		m_startScreenColor = m_CurrentScreenOverlayColor;

	}

	private void Update()
	{
		m_isDead = GameObject.Find ("Main Camera").GetComponent<Death> ().m_isDead;
		// TEMP:
		// usage: use "SetScreenOverlayColor" to set the initial color, then use "StartFade" to set the desired color & fade duration and start the fade
		//SetScreenOverlayColor(new Color(0,0,0,1));
		if (m_isPaused) 
		{
			StartFade (new Color (0, 0, 0, 0.6f), 0.4f);
			if(m_CurrentScreenOverlayColor == m_TargetScreenOverlayColor)
			{
				Time.timeScale = 0;
				GameObject.Find("GameTheme").audio.Pause();
			}
		} 
		else
		{
			m_CurrentScreenOverlayColor = m_startScreenColor;
			if(!GameObject.Find("GameTheme").audio.isPlaying)
				GameObject.Find("GameTheme").audio.Play();
			Time.timeScale = 1;
		}
	}
	
	
	// draw the texture and perform the fade:
	private void OnGUI()
	{   
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);  //Keeps the button looking like a button
		buttonStyle.fontSize = 40;  //changes font size of button
		buttonStyle.normal.textColor = Color.white;
		buttonStyle.fontStyle = FontStyle.Bold;
		if(!m_isDead)
			if (GUI.Button (new Rect (Screen.width/2 - 75,30,150,100), "Pause", buttonStyle)) {
				m_isPaused = !m_isPaused;
		}
		// if the current color of the screen is not equal to the desired color: keep fading!
		if (m_CurrentScreenOverlayColor != m_TargetScreenOverlayColor)
		{			
			SetScreenOverlayColor(m_CurrentScreenOverlayColor + (m_DeltaColor + m_DeltaColor) * Time.deltaTime);
		}
		
		// only draw the texture when the alpha value is greater than 0:
		if (m_CurrentScreenOverlayColor.a > 0)
		{			
			GUI.depth = m_FadeGUIDepth;
			GUI.Label(new Rect(-10, -10, Screen.width + 10, Screen.height + 10), m_FadeTexture, m_BackgroundStyle);
		}
	}
	
	
	// instantly set the current color of the screen-texture to "newScreenOverlayColor"
	// can be usefull if you want to start a scene fully black and then fade to opague
	public void SetScreenOverlayColor(Color newScreenOverlayColor)
	{
		m_CurrentScreenOverlayColor = newScreenOverlayColor;
		m_FadeTexture.SetPixel(0, 0, m_CurrentScreenOverlayColor);
		m_FadeTexture.Apply();
	}
	
	
	// initiate a fade from the current screen color (set using "SetScreenOverlayColor") towards "newScreenOverlayColor" taking "fadeDuration" seconds
	public void StartFade(Color newScreenOverlayColor, float fadeDuration)
	{
		if (fadeDuration <= 0.0f)		// can't have a fade last -2455.05 seconds!
		{
			SetScreenOverlayColor(newScreenOverlayColor);
		}
		else					// initiate the fade: set the target-color and the delta-color
		{
			m_TargetScreenOverlayColor = newScreenOverlayColor;
			m_DeltaColor = (m_TargetScreenOverlayColor - m_CurrentScreenOverlayColor) / fadeDuration;
		}
	}
}
