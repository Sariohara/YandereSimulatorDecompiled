﻿using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Vision/Blood_Fast")]
public class CameraFilterPack_Vision_Blood_Fast : MonoBehaviour
{
	// Token: 0x1700032C RID: 812
	// (get) Token: 0x060011CF RID: 4559 RVA: 0x0008938B File Offset: 0x0008758B
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

	// Token: 0x060011D0 RID: 4560 RVA: 0x000893BF File Offset: 0x000875BF
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Vision_Blood_Fast");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x000893E0 File Offset: 0x000875E0
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
			this.material.SetFloat("_Value", this.HoleSize);
			this.material.SetFloat("_Value2", this.HoleSmooth);
			this.material.SetFloat("_Value3", this.Color1);
			this.material.SetFloat("_Value4", this.Color2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x000894D8 File Offset: 0x000876D8
	private void Update()
	{
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x000894DA File Offset: 0x000876DA
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x0400165C RID: 5724
	public Shader SCShader;

	// Token: 0x0400165D RID: 5725
	private float TimeX = 1f;

	// Token: 0x0400165E RID: 5726
	private Material SCMaterial;

	// Token: 0x0400165F RID: 5727
	[Range(0.01f, 1f)]
	public float HoleSize = 0.6f;

	// Token: 0x04001660 RID: 5728
	[Range(-1f, 1f)]
	public float HoleSmooth = 0.3f;

	// Token: 0x04001661 RID: 5729
	[Range(-2f, 2f)]
	public float Color1 = 0.2f;

	// Token: 0x04001662 RID: 5730
	[Range(-2f, 2f)]
	public float Color2 = 0.9f;
}