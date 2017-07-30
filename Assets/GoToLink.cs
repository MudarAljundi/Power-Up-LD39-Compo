using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoToLink : MonoBehaviour, IPointerDownHandler {

	public string link;

	public void OnPointerDown(PointerEventData eventData) {

		Application.OpenURL(link);
	}
	
}
