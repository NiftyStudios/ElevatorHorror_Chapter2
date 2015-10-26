using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class TextureGenerator : MonoBehaviour {

    public Vector2 Size;
	public bool IsGenNormalMap;
	public enum EffectType 
	{
		Deffault,
		Grayscale
	}
	public EffectType Effect;

	public enum LineType
	{
		None,
		Horizontal
	}
	public LineType Line;

    private Vector2 offset;
	private delegate Color ShaderEmulate(int x, int y);

	public void Start () {
        Application.runInBackground = true;
		Generate();
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.1F, 0.1F);
	}
	
	void FixedUpdate () {
         if (offset.y > 10) offset.y = 0;
         if (offset.x > 10) offset.x = 0;
         offset.x += 0.05f;
         offset.y += 0.06f;
         GetComponent<Renderer>().material.mainTextureOffset = offset;
         if(IsGenNormalMap)
            GetComponent<Renderer>().material.SetTextureOffset("_BumpMap", offset);
	}


	public void Generate()
	{
		ShaderEmulate emulate = null;
		switch(Effect)
		{
		case EffectType.Deffault:
			emulate = GenerateDiffuse;
			break;
		case EffectType.Grayscale:
			emulate = GenerateGrayscale;
			break;
		}

		if(emulate == null)
		{
			throw new System.Exception("Shader emulate function not found");
		}

		Texture2D _tex = new Texture2D((int)Size.x, (int)Size.y, TextureFormat.RGB24, false);
		for (int x = 0; x < Size.x; x++)
		{
			for (int y = 0; y < Size.y; y++)
			{
				Color _color = emulate(x, y);
				switch(Line)
				{
				case LineType.Horizontal:
					_color += GetLine(0, y);
					break;
				}
				_tex.SetPixel(x, y, _color);
			}
		}
		_tex.Compress(false);
		_tex.Apply();
		GetComponent<Renderer>().material.mainTexture = _tex;
		if(IsGenNormalMap)
			NormalMap(_tex);
		System.GC.Collect();
	}

	Color GenerateGrayscale(int x, int y)
	{
		float _sourceColor = Vector3.Dot(new Vector3(Random.Range(0, 0.9F), Random.Range(0, 0.9F), Random.Range(0, 0.9F)), 
		            new Vector3(0.3f, 0.59f, 0.11f));
		Color _color = new Color(_sourceColor, _sourceColor, _sourceColor);
		return _color;
	}

	Color GenerateDiffuse(int x, int y)
    {
		return new Color(Random.Range(0, 0.9F), Random.Range(0, 0.9F), Random.Range(0, 0.9F));
    }

	Color GetLine(int x, int y)
	{
		float _frac = (float)(Frac((double)(x+y*0.1) * 5) - 0.5);
		Color _color = new Color(_frac, _frac, _frac);
		return _color;
	}

    void NormalMap(Texture2D tex)
    {
        Texture2D loadedTexture = tex;
        Texture2D normalTexture = new Texture2D(loadedTexture.width, loadedTexture.height, TextureFormat.ARGB32, false);
        Color theColour = new Color();
        for (int x = 0; x < loadedTexture.width; x++)
        {
            for (int y = 0; y < loadedTexture.height; y++)
            {
                theColour.r = loadedTexture.GetPixel(x, y).g;
                theColour.g = theColour.r;
                theColour.b = theColour.r;
                theColour.a = loadedTexture.GetPixel(x, y).r;
                normalTexture.SetPixel(x, y, theColour);
            }
        }
        normalTexture.Apply();
        GetComponent<Renderer>().material.SetTexture("_BumpMap", normalTexture);
    }

	public double Frac(double value) 
	{ 
		return value - System.Math.Truncate(value); 
	}
}
