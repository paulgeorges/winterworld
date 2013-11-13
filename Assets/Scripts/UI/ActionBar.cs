using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionBar : MonoBehaviour {
	public float actionBarLength = 440;
	public float actionBarSelectorTime = 2;
	
	private ActionBarSelector _actionBarSelector;
	
	private void Awake () {
		_actionBarSelector = transform.Find("ActionBarSelector").GetComponent<ActionBarSelector>();
		OnActionBarSelectorOnEnd();
	}
	
	void OnActionBarSelectorOnEnd () {
		if(_actionBarSelector.transform.localPosition.x < 0){
			iTween.MoveTo(_actionBarSelector.gameObject, iTween.Hash("x", actionBarLength / 2,
																		"isLocal", true,
																		"time", actionBarSelectorTime,
																		"easeType", iTween.EaseType.linear,
																		"looptype", iTween.LoopType.pingPong));
		}
	}
}
