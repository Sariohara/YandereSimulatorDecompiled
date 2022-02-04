﻿using System;
using UnityEngine;

// Token: 0x02000124 RID: 292
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/Weather/Rain_Pro_3D")]
public class CameraFilterPack_Atmosphere_Rain_Pro_3D : MonoBehaviour
{
	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0006C1E2 File Offset: 0x0006A3E2
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

	// Token: 0x06000B56 RID: 2902 RVA: 0x0006C216 File Offset: 0x0006A416
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_Atmosphere_Rain_FX") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/Atmosphere_Rain_Pro_3D");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x0006C24C File Offset: 0x0006A44C
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
			this.material.SetFloat("_Value", this.Fade);
			this.material.SetFloat("_Value2", this.Intensity);
			if (this.DirectionFollowCameraZ)
			{
				float z = base.GetComponent<Camera>().transform.rotation.z;
				if (z > 0f && z < 360f)
				{
					this.material.SetFloat("_Value3", z);
				}
				if (z < 0f)
				{
					this.material.SetFloat("_Value3", z);
				}
			}
			else
			{
				this.material.SetFloat("_Value3", this.DirectionX);
			}
			this.material.SetFloat("_Value4", this.Speed);
			this.material.SetFloat("_Value5", this.Size);
			this.material.SetFloat("_Value6", this.Distortion);
			this.material.SetFloat("_Value7", this.StormFlashOnOff);
			this.material.SetFloat("_Value8", this.DropOnOff);
			this.material.SetFloat("_FixDistance", this._FixDistance);
			this.material.SetFloat("_Visualize", (float)(this._Visualize ? 1 : 0));
			this.material.SetFloat("Drop_Near", this.Drop_Near);
			this.material.SetFloat("Drop_Far", this.Drop_Far);
			this.material.SetFloat("Drop_With_Obj", 1f - this.Drop_With_Obj);
			this.material.SetFloat("Myst", this.Myst);
			this.material.SetColor("Myst_Color", this.Myst_Color);
			this.material.SetFloat("Drop_Floor_Fluid", this.Drop_Floor_Fluid);
			this.material.SetVector("_ScreenResolution", new Vector4((float)sourceTexture.width, (float)sourceTexture.height, 0f, 0f));
			this.material.SetTexture("Texture2", this.Texture2);
			base.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x0006C4D5 File Offset: 0x0006A6D5
	private void Update()
	{
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x0006C4D7 File Offset: 0x0006A6D7
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000F66 RID: 3942
	public Shader SCShader;

	// Token: 0x04000F67 RID: 3943
	public bool _Visualize;

	// Token: 0x04000F68 RID: 3944
	private float TimeX = 1f;

	// Token: 0x04000F69 RID: 3945
	private Material SCMaterial;

	// Token: 0x04000F6A RID: 3946
	[Range(0f, 100f)]
	public float _FixDistance = 3f;

	// Token: 0x04000F6B RID: 3947
	[Range(0f, 1f)]
	public float Fade = 1f;

	// Token: 0x04000F6C RID: 3948
	[Range(0f, 2f)]
	public float Intensity = 0.5f;

	// Token: 0x04000F6D RID: 3949
	public bool DirectionFollowCameraZ;

	// Token: 0x04000F6E RID: 3950
	[Range(-0.45f, 0.45f)]
	public float DirectionX = 0.12f;

	// Token: 0x04000F6F RID: 3951
	[Range(0.4f, 2f)]
	public float Size = 1.5f;

	// Token: 0x04000F70 RID: 3952
	[Range(0f, 0.5f)]
	public float Speed = 0.275f;

	// Token: 0x04000F71 RID: 3953
	[Range(0f, 0.5f)]
	public float Distortion = 0.025f;

	// Token: 0x04000F72 RID: 3954
	[Range(0f, 1f)]
	public float StormFlashOnOff = 1f;

	// Token: 0x04000F73 RID: 3955
	[Range(0f, 1f)]
	public float DropOnOff = 1f;

	// Token: 0x04000F74 RID: 3956
	[Range(-0.5f, 0.99f)]
	public float Drop_Near;

	// Token: 0x04000F75 RID: 3957
	[Range(0f, 1f)]
	public float Drop_Far = 0.5f;

	// Token: 0x04000F76 RID: 3958
	[Range(0f, 1f)]
	public float Drop_With_Obj = 0.2f;

	// Token: 0x04000F77 RID: 3959
	[Range(0f, 1f)]
	public float Myst = 0.1f;

	// Token: 0x04000F78 RID: 3960
	[Range(0f, 1f)]
	public float Drop_Floor_Fluid;

	// Token: 0x04000F79 RID: 3961
	public Color Myst_Color = new Color(0.5f, 0.5f, 0.5f, 1f);

	// Token: 0x04000F7A RID: 3962
	private Texture2D Texture2;
}