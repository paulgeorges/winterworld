using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionBar : MonoBehaviour {
	public float actionBarLength = 440;
	public float actionBarSelectorTime = 2;
	
	private UISprite _actionBarBackgroundSprite;
	private Transform _actionBarOptions;
	private ActionBarSelector _actionBarSelector;
	
	private void Awake () {
		_actionBarBackgroundSprite = transform.Find("ActionBarBackgroundSprite").GetComponent<UISprite>();
		_actionBarOptions = transform.Find("ActionBarOptions");
		_actionBarSelector = transform.Find("ActionBarSelector").GetComponent<ActionBarSelector>();
		
		List<ActionBarOption> firstOptions = new List<ActionBarOption>();
		
		for(int i = 1; i <= 3; i++){
			GameObject actionBarOptionPrefab = (GameObject)Resources.Load("ActionBarOption");
			GameObject actionBarOptionGO = (GameObject)Instantiate(actionBarOptionPrefab);
			ActionBarOption actionBarOption = actionBarOptionGO.GetComponent<ActionBarOption>();
			actionBarOption.actionBarOptionLineSprite.alpha = 0;
			actionBarOption.percent = i / 3.0f * 0.75f;
			firstOptions.Add(actionBarOption);
		}
		
		Reset ();
		
		StartCoroutine("InitializeActionBarOptions", firstOptions);
	}
	
	private void OnEnable(){
		Messenger.AddListener(GameMessages.ACTION_BAR_TOUCHED, OnActionBarTouched);	
	}
	
	private void OnDisable(){
		Messenger.RemoveListener(GameMessages.ACTION_BAR_TOUCHED, OnActionBarTouched);	
	}
	
	private void OnActionBarTouched(){
		
	}
	
	private void Reset(){
		_actionBarBackgroundSprite.alpha = 0;
		_actionBarBackgroundSprite.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
	}
	
	private IEnumerator InitializeActionBarOptions(List<ActionBarOption> actionBarOptions){
		if(_actionBarBackgroundSprite.alpha == 1){
			TweenAlpha.Begin(_actionBarBackgroundSprite.gameObject, 1, 0);
			TweenScale.Begin(_actionBarBackgroundSprite.gameObject, 1, new Vector3(0.8f, 0.8f, 0.8f));
			yield return new WaitForSeconds(1);
			Reset();
		}
		
		TweenAlpha.Begin(_actionBarBackgroundSprite.gameObject, 1, 1);
		TweenScale.Begin(_actionBarBackgroundSprite.gameObject, 1, new Vector3(1, 1, 1));
		
		yield return new WaitForSeconds(1);
		
		foreach(ActionBarOption actionBarOption in actionBarOptions){
			actionBarOption.transform.parent = _actionBarOptions;
			actionBarOption.transform.localScale = new Vector3(0.1f, 0.3f, 1);
			actionBarOption.transform.localPosition = new Vector3(actionBarOption.percent * actionBarLength, 0, 0);
			TweenAlpha.Begin(actionBarOption.gameObject, 0.3f, 1);
			yield return new WaitForSeconds(0.3f);
		}
		
		OnActionBarSelectorOnEnd();
		yield return null;
	}
	
	private void OnActionBarSelectorOnEnd () {
		if(_actionBarSelector.transform.localPosition.x < 0){
			iTween.MoveTo(_actionBarSelector.gameObject, iTween.Hash("x", actionBarLength / 2,
																		"isLocal", true,
																		"time", actionBarSelectorTime,
																		"easeType", iTween.EaseType.linear,
																		"looptype", iTween.LoopType.pingPong));
		}
	}
}
