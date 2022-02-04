﻿using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Blend 2 Camera/ColorDodge")]
public class CameraFilterPack_Blend2Camera_ColorDodge : MonoBehaviour
{
	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0006D0C3 File Offset: 0x0006B2C3
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

	// Token: 0x06000B82 RID: 2946 RVA: 0x0006D0F8 File Offset: 0x0006B2F8
	private void Start()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
		this.SCShader = Shader.Find(this.ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0006D15C File Offset: 0x0006B35C
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			if (this.Camera2 != null)
			{
				this.material.SetTexture("_MainTex2", this.Camera2tex);
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.BlendFX);
			this.material.SetFloat("_Value2", this.SwitchCameraToCamera2);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0006D24C File Offset: 0x0006B44C
	private void OnValidate()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0006D284 File Offset: 0x0006B484
	private void Update()
	{
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x0006D286 File Offset: 0x0006B486
	private void OnEnable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2tex = new RenderTexture(Screen.width, Screen.height, 24);
			this.Camera2.targetTexture = this.Camera2tex;
		}
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0006D2BE File Offset: 0x0006B4BE
	private void OnDisable()
	{
		if (this.Camera2 != null)
		{
			this.Camera2.targetTexture = null;
		}
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000FA7 RID: 4007
	private string ShaderName = "CameraFilterPack/Blend2Camera_ColorDodge";

	// Token: 0x04000FA8 RID: 4008
	public Shader SCShader;

	// Token: 0x04000FA9 RID: 4009
	public Camera Camera2;

	// Token: 0x04000FAA RID: 4010
	private float TimeX = 1f;

	// Token: 0x04000FAB RID: 4011
	private Material SCMaterial;

	// Token: 0x04000FAC RID: 4012
	[Range(0f, 1f)]
	public float SwitchCameraToCamera2;

	// Token: 0x04000FAD RID: 4013
	[Range(0f, 1f)]
	public float BlendFX = 0.5f;

	// Token: 0x04000FAE RID: 4014
	private RenderTexture Camera2tex;
}