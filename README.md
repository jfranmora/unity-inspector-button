# [Unity] Inspector Extensions - Button

## Installation (Manual)
Download the release https://github.com/jfranmora/unity-inspector-button/archive/0.1.0.zip and unzip it in the project.

## Installation (UPM)
Add to `manifest.json` as dependency the following line:

`"com.jfranmora.inspector.required-field": "https://github.com/jfranmora/unity-inspector-button.git#upm"`

## How to use

Add the `[Button]` attribute to a function.
Optionally, you can set `HideInEditMode` or `HideInPlayMode` to hide the button in Editor Mode or Play Mode.

``` C#
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
```

![Example](https://i.imgur.com/64ZJT80.png)
