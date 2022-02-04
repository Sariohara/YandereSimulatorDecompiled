﻿using System;
using UnityEngine;

// Token: 0x020001F0 RID: 496
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Night Vision/Night Vision 1")]
public class CameraFilterPack_Oculus_NightVision1 : MonoBehaviour
{
	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x0600107D RID: 4221 RVA: 0x000839D7 File Offset: 0x00081BD7
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

	// Token: 0x0600107E RID: 4222 RVA: 0x00083A0B File Offset: 0x00081C0B
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Oculus_NightVision1");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x0600107F RID: 4223 RVA: 0x00083A2C File Offset: 0x00081C2C
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
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetFloat("_Vignette", this.Vignette);
			this.material.SetFloat("_Linecount", this.Linecount);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001080 RID: 4224 RVA: 0x00083B0E File Offset: 0x00081D0E
	private void Update()
	{
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x00083B10 File Offset: 0x00081D10
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04001502 RID: 5378
	public Shader SCShader;

	// Token: 0x04001503 RID: 5379
	private float TimeX = 1f;

	// Token: 0x04001504 RID: 5380
	private float Distortion = 1f;

	// Token: 0x04001505 RID: 5381
	private Material SCMaterial;

	// Token: 0x04001506 RID: 5382
	[Range(0f, 100f)]
	public float Vignette = 1.3f;

	// Token: 0x04001507 RID: 5383
	[Range(1f, 150f)]
	public float Linecount = 90f;
}