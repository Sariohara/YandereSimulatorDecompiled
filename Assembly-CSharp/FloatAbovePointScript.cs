﻿using System;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class FloatAbovePointScript : MonoBehaviour
{
	// Token: 0x06001499 RID: 5273 RVA: 0x000CA475 File Offset: 0x000C8675
	private void Update()
	{
		base.transform.position = this.Target.position + new Vector3(0f, 0.15f, 0f);
	}

	// Token: 0x04002029 RID: 8233
	public Transform Target;
}