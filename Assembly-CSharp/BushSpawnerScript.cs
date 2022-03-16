﻿using System;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class BushSpawnerScript : MonoBehaviour
{
	// Token: 0x06000A9D RID: 2717 RVA: 0x000617A8 File Offset: 0x0005F9A8
	private void Update()
	{
		if (Input.GetKeyDown("z"))
		{
			this.Begin = true;
		}
		if (this.Begin)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Bush, new Vector3(UnityEngine.Random.Range(-16f, 16f), UnityEngine.Random.Range(0f, 4f), UnityEngine.Random.Range(-16f, 16f)), Quaternion.identity);
		}
	}

	// Token: 0x04000CBE RID: 3262
	public GameObject Bush;

	// Token: 0x04000CBF RID: 3263
	public bool Begin;
}