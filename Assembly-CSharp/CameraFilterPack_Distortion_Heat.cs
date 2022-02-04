﻿using System;
using UnityEngine;

// Token: 0x0200017D RID: 381
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Heat")]
public class CameraFilterPack_Distortion_Heat : MonoBehaviour
{
	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00076FD2 File Offset: 0x000751D2
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

	// Token: 0x06000DAD RID: 3501 RVA: 0x00077006 File Offset: 0x00075206
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Heat");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x00077028 File Offset: 0x00075228
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
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x000770DE File Offset: 0x000752DE
	private void Update()
	{
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x000770E0 File Offset: 0x000752E0
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011EE RID: 4590
	public Shader SCShader;

	// Token: 0x040011EF RID: 4591
	private float TimeX = 1f;

	// Token: 0x040011F0 RID: 4592
	private Material SCMaterial;

	// Token: 0x040011F1 RID: 4593
	[Range(1f, 100f)]
	public float Distortion = 35f;
}