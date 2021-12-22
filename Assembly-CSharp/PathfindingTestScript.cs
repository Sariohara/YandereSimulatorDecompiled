﻿using System;
using UnityEngine;

// Token: 0x020004FC RID: 1276
public class PathfindingTestScript : MonoBehaviour
{
	// Token: 0x06002106 RID: 8454 RVA: 0x001E3200 File Offset: 0x001E1400
	private void Update()
	{
		if (Input.GetKeyDown("left"))
		{
			this.bytes = AstarPath.active.astarData.SerializeGraphs();
		}
		if (Input.GetKeyDown("right"))
		{
			AstarPath.active.astarData.DeserializeGraphs(this.bytes);
			AstarPath.active.Scan(null);
		}
	}

	// Token: 0x04004883 RID: 18563
	private byte[] bytes;
}
