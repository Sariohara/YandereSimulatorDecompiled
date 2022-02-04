﻿using System;
using UnityEngine;

// Token: 0x020001EC RID: 492
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Night Vision/Night Vision 4")]
public class CameraFilterPack_NightVision_4 : MonoBehaviour
{
	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x06001063 RID: 4195 RVA: 0x000831A2 File Offset: 0x000813A2
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

	// Token: 0x06001064 RID: 4196 RVA: 0x000831D6 File Offset: 0x000813D6
	private void ChangeFilters()
	{
		this.Matrix9 = new float[]
		{
			200f,
			-200f,
			-200f,
			195f,
			4f,
			-160f,
			200f,
			-200f,
			-200f,
			-200f,
			10f,
			-200f
		};
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x000831F0 File Offset: 0x000813F0
	private void Start()
	{
		this.ChangeFilters();
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x00083218 File Offset: 0x00081418
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
			this.material.SetFloat("_Red_R", this.Matrix9[0] / 100f);
			this.material.SetFloat("_Red_G", this.Matrix9[1] / 100f);
			this.material.SetFloat("_Red_B", this.Matrix9[2] / 100f);
			this.material.SetFloat("_Green_R", this.Matrix9[3] / 100f);
			this.material.SetFloat("_Green_G", this.Matrix9[4] / 100f);
			this.material.SetFloat("_Green_B", this.Matrix9[5] / 100f);
			this.material.SetFloat("_Blue_R", this.Matrix9[6] / 100f);
			this.material.SetFloat("_Blue_G", this.Matrix9[7] / 100f);
			this.material.SetFloat("_Blue_B", this.Matrix9[8] / 100f);
			this.material.SetFloat("_Red_C", this.Matrix9[9] / 100f);
			this.material.SetFloat("_Green_C", this.Matrix9[10] / 100f);
			this.material.SetFloat("_Blue_C", this.Matrix9[11] / 100f);
			this.material.SetFloat("_FadeFX", this.FadeFX);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x00083439 File Offset: 0x00081639
	private void OnValidate()
	{
		this.ChangeFilters();
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x00083441 File Offset: 0x00081641
	private void Update()
	{
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x00083443 File Offset: 0x00081643
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040014E4 RID: 5348
	private string ShaderName = "CameraFilterPack/NightVision_4";

	// Token: 0x040014E5 RID: 5349
	public Shader SCShader;

	// Token: 0x040014E6 RID: 5350
	[Range(0f, 1f)]
	public float FadeFX = 1f;

	// Token: 0x040014E7 RID: 5351
	private float TimeX = 1f;

	// Token: 0x040014E8 RID: 5352
	private Material SCMaterial;

	// Token: 0x040014E9 RID: 5353
	private float[] Matrix9;
}