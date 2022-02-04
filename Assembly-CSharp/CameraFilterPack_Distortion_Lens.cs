﻿using System;
using UnityEngine;

// Token: 0x0200017E RID: 382
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Lens")]
public class CameraFilterPack_Distortion_Lens : MonoBehaviour
{
	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00077118 File Offset: 0x00075318
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

	// Token: 0x06000DB3 RID: 3507 RVA: 0x0007714C File Offset: 0x0007534C
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Lens");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x00077170 File Offset: 0x00075370
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
			this.material.SetFloat("_CenterX", this.CenterX);
			this.material.SetFloat("_CenterY", this.CenterY);
			this.material.SetFloat("_Distortion", this.Distortion);
			this.material.SetVector("_ScreenResolution", new Vector2((float)Screen.width, (float)Screen.height));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x0007724B File Offset: 0x0007544B
	private void Update()
	{
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x0007724D File Offset: 0x0007544D
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011F2 RID: 4594
	public Shader SCShader;

	// Token: 0x040011F3 RID: 4595
	private float TimeX = 1f;

	// Token: 0x040011F4 RID: 4596
	private Material SCMaterial;

	// Token: 0x040011F5 RID: 4597
	[Range(-1f, 1f)]
	public float CenterX;

	// Token: 0x040011F6 RID: 4598
	[Range(-1f, 1f)]
	public float CenterY;

	// Token: 0x040011F7 RID: 4599
	[Range(0f, 3f)]
	public float Distortion = 1f;
}