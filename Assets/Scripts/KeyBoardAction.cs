using UnityEngine;
using UnityEngine.Events;
public class KeyBoardAction : MonoBehaviour
{
	[SerializeField] private KeyCode keyCode;
	[SerializeField] private UnityEvent action;
	private bool isClick;
	
	private void Update()
	{
		if(Input.GetKeyDown(keyCode))
		{
			action?.Invoke();
		}
	}
}
