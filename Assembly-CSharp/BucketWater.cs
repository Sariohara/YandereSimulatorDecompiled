﻿using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
[Serializable]
public class BucketWater : BucketContents
{
	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0005CB27 File Offset: 0x0005AD27
	// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0005CB2F File Offset: 0x0005AD2F
	public float Bloodiness
	{
		get
		{
			return this.bloodiness;
		}
		set
		{
			this.bloodiness = Mathf.Clamp01(value);
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0005CB3D File Offset: 0x0005AD3D
	// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0005CB45 File Offset: 0x0005AD45
	public bool HasBleach
	{
		get
		{
			return this.hasBleach;
		}
		set
		{
			this.hasBleach = value;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0005CB4E File Offset: 0x0005AD4E
	public override BucketContentsType Type
	{
		get
		{
			return BucketContentsType.Water;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0005CB51 File Offset: 0x0005AD51
	public override bool IsCleaningAgent
	{
		get
		{
			return this.hasBleach;
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0005CB59 File Offset: 0x0005AD59
	public override bool IsFlammable
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0005CB5C File Offset: 0x0005AD5C
	public override bool CanBeLifted(int strength)
	{
		return true;
	}

	// Token: 0x04000C2F RID: 3119
	[SerializeField]
	private float bloodiness;

	// Token: 0x04000C30 RID: 3120
	[SerializeField]
	private bool hasBleach;
}
