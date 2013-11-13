using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionBarOption : MonoBehaviour {
	public string optionName;
	public UISprite optionSprite;
	public float percent;
	public float size;
	public List<ActionBarOption> actionBarOptions = new List<ActionBarOption>();
	
	public UISprite actionBarOptionLineSprite;
	
	private void Awake(){
		actionBarOptionLineSprite = GetComponent<UISprite>();
	}
}
