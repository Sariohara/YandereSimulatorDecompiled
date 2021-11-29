﻿using System;
using UnityEngine;

// Token: 0x020001CD RID: 461
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glow/Glow_Color")]
public class CameraFilterPack_Glow_Glow_Color : MonoBehaviour
{
	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0007EE69 File Offset: 0x0007D069
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

	// Token: 0x06000F91 RID: 3985 RVA: 0x0007EE9D File Offset: 0x0007D09D
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Glow_Glow_Color");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x0007EEC0 File Offset: 0x0007D0C0
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (!(this.SCShader != null))
		{
			Graphics.Blit(sourceTexture, destTexture);
			return;
		}
		int fastFilter = this.FastFilter;
		this.TimeX += Time.deltaTime;
		if (this.TimeX > 100f)
		{
			this.TimeX = 0f;
		}
		this.material.SetFloat("_TimeX", this.TimeX);
		this.material.SetFloat("_Amount", this.Amount);
		this.material.SetFloat("_Value1", this.Threshold);
		this.material.SetFloat("_Value2", this.Intensity);
		this.material.SetFloat("_Value3", this.Precision);
		this.material.SetColor("_GlowColor", this.GlowColor);
		this.material.SetVector("_ScreenResolution", new Vector2((float)(Screen.width / fastFilter), (float)(Screen.height / fastFilter)));
		int width = sourceTexture.width / fastFilter;
		int height = sourceTexture.height / fastFilter;
		if (this.FastFilter > 1)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary(width, height, 0);
			temporary.filterMode = FilterMode.Trilinear;
			Graphics.Blit(sourceTexture, temporary, this.material, 3);
			Graphics.Blit(temporary, temporary2, this.material, 2);
			Graphics.Blit(temporary2, temporary, this.material, 0);
			this.material.SetFloat("_Amount", this.Amount * 2f);
			Graphics.Blit(temporary, temporary2, this.material, 2);
			Graphics.Blit(temporary2, temporary, this.material, 0);
			this.material.SetTexture("_MainTex2", temporary);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			Graphics.Blit(sourceTexture, destTexture, this.material, 1);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture, this.material, 0);
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x0007F09E File Offset: 0x0007D29E
	private void Update()
	{
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x0007F0A0 File Offset: 0x0007D2A0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013FE RID: 5118
	public Shader SCShader;

	// Token: 0x040013FF RID: 5119
	private float TimeX = 1f;

	// Token: 0x04001400 RID: 5120
	private Material SCMaterial;

	// Token: 0x04001401 RID: 5121
	[Range(0f, 20f)]
	public float Amount = 4f;

	// Token: 0x04001402 RID: 5122
	[Range(2f, 16f)]
	public int FastFilter = 4;

	// Token: 0x04001403 RID: 5123
	[Range(0f, 1f)]
	public float Threshold = 0.5f;

	// Token: 0x04001404 RID: 5124
	[Range(0f, 3f)]
	public float Intensity = 2.25f;

	// Token: 0x04001405 RID: 5125
	[Range(-1f, 1f)]
	public float Precision = 0.56f;

	// Token: 0x04001406 RID: 5126
	public Color GlowColor = new Color(0f, 0.7f, 1f, 1f);
}