using JfranMora.Inspector;
using UnityEngine;

public class Example : MonoBehaviour
{
	public float value = 0;

	[Button]
    private void SomeFunction()
	{
		Debug.Log("A");
	}

	[Button(HideInEditMode = true)]
	private void EditorModeOnlyFunction()
	{
		Debug.Log("B");
	}

	[Button(HideInPlayMode = true)]
	private void PlayModeOnlyFunction()
	{
		Debug.Log("C");
	}
}
