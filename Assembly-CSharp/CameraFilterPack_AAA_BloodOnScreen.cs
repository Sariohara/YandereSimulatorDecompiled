﻿using System;
using UnityEngine;

// Token: 0x02000118 RID: 280
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood On Screen")]
public class CameraFilterPack_AAA_BloodOnScreen : MonoBehaviour
{
	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0006A7D0 File Offset: 0x000689D0
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

	// Token: 0x06000B0E RID: 2830 RVA: 0x0006A804 File Offset: 0x00068A04
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_BloodOnScreen1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_BloodOnScreen");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x0006A83C File Offset: 0x00068A3C
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
			this.material.SetFloat("_Value", Mathf.Clamp(this.Blood_On_Screen, 0.02f, 1.6f));
			this.material.SetFloat("_Value2", Mathf.Clamp(this.Blood_Intensify, 0f, 2f));
			this.material.SetFloat("_Value3", Mathf.Clamp(this.Blood_Darkness, 0f, 2f));
			this.material.SetFloat("_Value4", Mathf.Clamp(this.Blood_Fade, 0f, 1f));
			this.material.SetFloat("_Value5", Mathf.Clamp(this.Blood_Distortion_Speed, 0f, 2f));
			this.material.SetColor("_Color2", this.Blood_Color);
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0006A994 File Offset: 0x00068B94
	private void Update()
	{
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x0006A996 File Offset: 0x00068B96
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000EEA RID: 3818
	public Shader SCShader;

	// Token: 0x04000EEB RID: 3819
	private float TimeX = 1f;

	// Token: 0x04000EEC RID: 3820
	[Range(0.02f, 1.6f)]
	public float Blood_On_Screen = 1f;

	// Token: 0x04000EED RID: 3821
	public Color Blood_Color = Color.red;

	// Token: 0x04000EEE RID: 3822
	[Range(0f, 2f)]
	public float Blood_Intensify = 0.7f;

	// Token: 0x04000EEF RID: 3823
	[Range(0f, 2f)]
	public float Blood_Darkness = 0.5f;

	// Token: 0x04000EF0 RID: 3824
	[Range(0f, 1f)]
	public float Blood_Distortion_Speed = 0.25f;

	// Token: 0x04000EF1 RID: 3825
	[Range(0f, 1f)]
	public float Blood_Fade = 1f;

	// Token: 0x04000EF2 RID: 3826
	private Material SCMaterial;

	// Token: 0x04000EF3 RID: 3827
	private Texture2D Texture2;
}