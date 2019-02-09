using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorControl : MonoBehaviour {

	SpriteRenderer mySR;

	[SerializeField] float[] alphas;
	[SerializeField] float[] timeAlphas;
	[SerializeField] int offsetAlpha;
	[SerializeField] Color[] colors;
	[SerializeField] float[] timeColors;
	[SerializeField] int offsetColor;
	[SerializeField] bool playOnAwake;

	int idxAlpha, idxColor;
	float tAlpha, tColor;
	public bool active;

	void Awake(){
		mySR = GetComponent<SpriteRenderer>();
		idxAlpha = offsetAlpha; idxColor = offsetColor;
		tAlpha = 0; tColor = 0;
		active = playOnAwake;
	}

	void Update(){
		if(active){
			Color c = mySR.color;
			//アルファ値の変更
			if(alphas.Length > 0){
				tAlpha += Time.deltaTime / timeAlphas[idxAlpha];
				c = ChangeAlpha(c, Mathf.Lerp(alphas[idxAlpha], alphas[(idxAlpha + 1) % alphas.Length], tAlpha));
				if(tAlpha >= 1f){
					tAlpha = 0;
					idxAlpha = (idxAlpha + 1) % alphas.Length;
				}
			}
			//RGBの変更
			if(colors.Length > 0){
				tColor += Time.deltaTime / timeColors[idxColor];
				c = Color.Lerp(colors[idxColor], colors[(idxColor + 1) % colors.Length], tColor);
				if(tColor >= 1f){
					tColor = 0;
					idxColor = (idxColor + 1) % colors.Length;
				}
			}
			mySR.color = c;
		}
	}

	Color ChangeAlpha(Color c, float a){
		Color tmp = c;
		tmp.a = a;
		return tmp;
	}
}
