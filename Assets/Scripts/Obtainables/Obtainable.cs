using UnityEngine;

public class Obtainable : ScriptableObject {
	public string title;
	[TextArea()]
	public string description;
	public int baseValue;

	public Sprite sprite;
}