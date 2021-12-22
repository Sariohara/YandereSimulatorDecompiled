﻿using System;
using UnityEngine;

// Token: 0x0200047C RID: 1148
public class TrailScript : MonoBehaviour
{
	// Token: 0x06001ECB RID: 7883 RVA: 0x001B00B8 File Offset: 0x001AE2B8
	private void Start()
	{
		Physics.IgnoreCollision(GameObject.Find("YandereChan").GetComponent<Collider>(), base.GetComponent<Collider>());
		Physics.IgnoreLayerCollision(20, 8, false);
		Physics.IgnoreLayerCollision(8, 20, false);
		Physics.IgnoreLayerCollision(20, 15, true);
		Physics.IgnoreLayerCollision(15, 20, true);
		UnityEngine.Object.Destroy(this);
	}
}
