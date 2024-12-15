using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoulDownAction : MonoBehaviour
{
	[SerializeField] private UnityEvent coulDownAction;
	
	public void InvokeCoulDownAction(float time)
	{
		StartCoroutine(CoulDown(time));
	}
	private IEnumerator CoulDown(float time)
	{
		yield return new WaitForSeconds(time);
		coulDownAction?.Invoke();
	}
}
