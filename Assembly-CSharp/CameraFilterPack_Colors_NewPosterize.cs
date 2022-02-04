﻿using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Colors/NewPosterize")]
public class CameraFilterPack_Colors_NewPosterize : MonoBehaviour
{
	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00075BFD File Offset: 0x00073DFD
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

	// Token: 0x06000D59 RID: 3417 RVA: 0x00075C31 File Offset: 0x00073E31
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Colors_NewPosterize");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x00075C54 File Offset: 0x00073E54
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
			this.material.SetFloat("_Value", this.Gamma);
			this.material.SetFloat("_Value2", this.Colors);
			this.material.SetFloat("_Value3", this.Green_Mod);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x00075D4C File Offset: 0x00073F4C
	private void Update()
	{
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x00075D4E File Offset: 0x00073F4E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400119D RID: 4509
	public Shader SCShader;

	// Token: 0x0400119E RID: 4510
	private float TimeX = 1f;

	// Token: 0x0400119F RID: 4511
	private Material SCMaterial;

	// Token: 0x040011A0 RID: 4512
	[Range(0f, 2f)]
	public float Gamma = 1f;

	// Token: 0x040011A1 RID: 4513
	[Range(0f, 16f)]
	public float Colors = 11f;

	// Token: 0x040011A2 RID: 4514
	[Range(-1f, 1f)]
	public float Green_Mod = 1f;

	// Token: 0x040011A3 RID: 4515
	[Range(0f, 10f)]
	private float Value4 = 1f;
}