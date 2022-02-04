﻿using System;
using UnityEngine;

// Token: 0x02000176 RID: 374
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Distortion/Dissipation")]
public class CameraFilterPack_Distortion_Dissipation : MonoBehaviour
{
	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06000D82 RID: 3458 RVA: 0x0007664E File Offset: 0x0007484E
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

	// Token: 0x06000D83 RID: 3459 RVA: 0x00076682 File Offset: 0x00074882
	private void Start()
	{
		this.SCShader = Shader.Find("CameraFilterPack/Distortion_Dissipation");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x000766A4 File Offset: 0x000748A4
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
			this.material.SetFloat("_Value", this.Dissipation);
			this.material.SetFloat("_Value2", this.Colors);
			this.material.SetFloat("_Value3", this.Green_Mod);
			this.material.SetFloat("_Value4", this.Value4);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0007679C File Offset: 0x0007499C
	private void Update()
	{
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0007679E File Offset: 0x0007499E
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x040011CA RID: 4554
	public Shader SCShader;

	// Token: 0x040011CB RID: 4555
	private float TimeX = 1f;

	// Token: 0x040011CC RID: 4556
	private Material SCMaterial;

	// Token: 0x040011CD RID: 4557
	[Range(0f, 2.99f)]
	public float Dissipation = 1f;

	// Token: 0x040011CE RID: 4558
	[Range(0f, 16f)]
	private float Colors = 11f;

	// Token: 0x040011CF RID: 4559
	[Range(-1f, 1f)]
	private float Green_Mod = 1f;

	// Token: 0x040011D0 RID: 4560
	[Range(0f, 10f)]
	private float Value4 = 1f;
}