﻿using System;
using UnityEngine;

// Token: 0x0200022E RID: 558
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Rainbow")]
public class CameraFilterPack_Vision_Rainbow : MonoBehaviour
{
	// Token: 0x17000332 RID: 818
	// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0008A00B File Offset: 0x0008820B
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

	// Token: 0x060011F7 RID: 4599 RVA: 0x0008A03F File Offset: 0x0008823F
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Rainbow");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x0008A060 File Offset: 0x00088260
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
			this.material.SetFloat("_Value", this.Speed);
			this.material.SetFloat("_Value2", this.PosX);
			this.material.SetFloat("_Value3", this.PosY);
			this.material.SetFloat("_Value4", this.Colors);
			this.material.SetFloat("_Value5", this.Vision);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x0008A16E File Offset: 0x0008836E
	private void Update()
	{
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x0008A170 File Offset: 0x00088370
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400168F RID: 5775
	public Shader SCShader;

	// Token: 0x04001690 RID: 5776
	private float TimeX = 1f;

	// Token: 0x04001691 RID: 5777
	private Material SCMaterial;

	// Token: 0x04001692 RID: 5778
	[Range(0f, 10f)]
	public float Speed = 1f;

	// Token: 0x04001693 RID: 5779
	[Range(0f, 1f)]
	public float PosX = 0.5f;

	// Token: 0x04001694 RID: 5780
	[Range(0f, 1f)]
	public float PosY = 0.5f;

	// Token: 0x04001695 RID: 5781
	[Range(0f, 5f)]
	public float Colors = 0.5f;

	// Token: 0x04001696 RID: 5782
	[Range(0f, 1f)]
	public float Vision = 0.5f;
}