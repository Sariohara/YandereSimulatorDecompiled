﻿using System;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class DelinquentMaskScript : MonoBehaviour
{
	// Token: 0x06001365 RID: 4965 RVA: 0x000B1100 File Offset: 0x000AF300
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			this.ID++;
			if (this.ID > 4)
			{
				this.ID = 0;
			}
			this.MyRenderer.mesh = this.Meshes[this.ID];
		}
	}

	// Token: 0x04001C42 RID: 7234
	public MeshFilter MyRenderer;

	// Token: 0x04001C43 RID: 7235
	public Mesh[] Meshes;

	// Token: 0x04001C44 RID: 7236
	public int ID;
}
