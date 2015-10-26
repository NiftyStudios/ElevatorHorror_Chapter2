using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public TextureGenerator[] Generators;

	private Animator _anim;

	void Start () {
		_anim = GetComponent<Animator>();
	}

	void OnGUI() {
		GUI.Window(0, new Rect(0,0,200,400), delegate(int id) {

			TextureGenerator.LineType _line = Generators[0].Line;
			TextureGenerator.EffectType _effect = Generators[0].Effect;

			if(GUILayout.Button(_line == TextureGenerator.LineType.None ? "On line":"Off line"))
			{
				foreach(TextureGenerator _gen in Generators)
				{
					_gen.Line = _line == TextureGenerator.LineType.None ? TextureGenerator.LineType.Horizontal : 
																					   TextureGenerator.LineType.None;
				}
			}

			if(GUILayout.Button(_effect == TextureGenerator.EffectType.Grayscale ? "Deffault":"Grayscale"))
			{
				foreach(TextureGenerator _gen in Generators)
				{
					_gen.Effect = _effect == TextureGenerator.EffectType.Grayscale ? TextureGenerator.EffectType.Deffault : 
																								TextureGenerator.EffectType.Grayscale;
				}
			}

			GUILayout.Space(20);
			if(GUILayout.Button("Regenerate"))
			{
				foreach(TextureGenerator _gen in Generators)
				{
					_gen.Generate(); 
				}
			}
		}, "Options");
	}
}
