using UnityEngine;
using System.Collections;

public class ScreenTouchZone : MonoBehaviour {
	private void OnClick(){
		Messenger.Invoke(GameMessages.ACTION_BAR_TOUCHED);
	}
}
