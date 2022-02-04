﻿using System;
using UnityEngine;

// Token: 0x020001CD RID: 461
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Glow/Glow")]
public class CameraFilterPack_Glow_Glow : MonoBehaviour
{
	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0007EDD7 File Offset: 0x0007CFD7
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

	// Token: 0x06000F8E RID: 3982 RVA: 0x0007EE0B File Offset: 0x0007D00B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Glow_Glow");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x0007EE2C File Offset: 0x0007D02C
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

	// Token: 0x06000F90 RID: 3984 RVA: 0x0007EFF4 File Offset: 0x0007D1F4
	private void Update()
	{
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x0007EFF6 File Offset: 0x0007D1F6
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040013FB RID: 5115
	public Shader SCShader;

	// Token: 0x040013FC RID: 5116
	private float TimeX = 1f;

	// Token: 0x040013FD RID: 5117
	private Material SCMaterial;

	// Token: 0x040013FE RID: 5118
	[Range(0f, 20f)]
	public float Amount = 4f;

	// Token: 0x040013FF RID: 5119
	[Range(2f, 16f)]
	public int FastFilter = 4;

	// Token: 0x04001400 RID: 5120
	[Range(0f, 1f)]
	public float Threshold = 0.5f;

	// Token: 0x04001401 RID: 5121
	[Range(0f, 1f)]
	public float Intensity = 0.75f;

	// Token: 0x04001402 RID: 5122
	[Range(-1f, 1f)]
	public float Precision = 0.56f;
}