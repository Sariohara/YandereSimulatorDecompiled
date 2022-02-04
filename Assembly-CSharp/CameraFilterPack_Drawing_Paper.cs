﻿using System;
using UnityEngine;

// Token: 0x0200019A RID: 410
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Drawing/Paper")]
public class CameraFilterPack_Drawing_Paper : MonoBehaviour
{
	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000799D5 File Offset: 0x00077BD5
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x00079A09 File Offset: 0x00077C09
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Paper1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Drawing_Paper");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x00079A40 File Offset: 0x00077C40
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetColor("_PColor", this.Pencil_Color);
			this.material.SetFloat("_Value1", this.Pencil_Size);
			this.material.SetFloat("_Value2", this.Pencil_Correction);
			this.material.SetFloat("_Value3", this.Intensity);
			this.material.SetFloat("_Value4", this.Speed_Animation);
			this.material.SetFloat("_Value5", this.Corner_Lose);
			this.material.SetFloat("_Value6", this.Fade_Paper_to_BackColor);
			this.material.SetFloat("_Value7", this.Fade_With_Original);
			this.material.SetColor("_PColor2", this.Back_Color);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x00079B8F File Offset: 0x00077D8F
	private void Update()
	{
	}

	// Token: 0x06000E5E RID: 3678 RVA: 0x00079B91 File Offset: 0x00077D91
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001297 RID: 4759
	public Shader SCShader;

	// Token: 0x04001298 RID: 4760
	private float TimeX = 1f;

	// Token: 0x04001299 RID: 4761
	public Color Pencil_Color = new Color(0.156f, 0.3f, 0.738f, 1f);

	// Token: 0x0400129A RID: 4762
	[Range(0.0001f, 0.0022f)]
	public float Pencil_Size = 0.0008f;

	// Token: 0x0400129B RID: 4763
	[Range(0f, 2f)]
	public float Pencil_Correction = 0.76f;

	// Token: 0x0400129C RID: 4764
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x0400129D RID: 4765
	[Range(0f, 2f)]
	public float Speed_Animation = 1f;

	// Token: 0x0400129E RID: 4766
	[Range(0f, 1f)]
	public float Corner_Lose = 0.5f;

	// Token: 0x0400129F RID: 4767
	[Range(0f, 1f)]
	public float Fade_Paper_to_BackColor;

	// Token: 0x040012A0 RID: 4768
	[Range(0f, 1f)]
	public float Fade_With_Original = 1f;

	// Token: 0x040012A1 RID: 4769
	public Color Back_Color = new Color(1f, 1f, 1f, 1f);

	// Token: 0x040012A2 RID: 4770
	private Material SCMaterial;

	// Token: 0x040012A3 RID: 4771
	private Texture2D Texture2;
}