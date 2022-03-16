﻿using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class AnimatedGifScript : MonoBehaviour
{
	// Token: 0x060009BA RID: 2490 RVA: 0x0005121A File Offset: 0x0004F41A
	private void Awake()
	{
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x060009BB RID: 2491 RVA: 0x0005121C File Offset: 0x0004F41C
	private float SecondsPerFrame
	{
		get
		{
			return 1f / this.FramesPerSecond;
		}
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0005122C File Offset: 0x0004F42C
	private void Update()
	{
		this.CurrentSeconds += Time.unscaledDeltaTime;
		while (this.CurrentSeconds >= this.SecondsPerFrame)
		{
			this.CurrentSeconds -= this.SecondsPerFrame;
			this.Frame++;
			if (this.Frame > this.Limit)
			{
				this.Frame = this.Start;
			}
		}
		this.Sprite.spriteName = this.SpriteName + this.Frame.ToString();
	}

	// Token: 0x04000A19 RID: 2585
	[SerializeField]
	private UISprite Sprite;

	// Token: 0x04000A1A RID: 2586
	[SerializeField]
	private string SpriteName;

	// Token: 0x04000A1B RID: 2587
	[SerializeField]
	private int Start;

	// Token: 0x04000A1C RID: 2588
	[SerializeField]
	private int Frame;

	// Token: 0x04000A1D RID: 2589
	[SerializeField]
	private int Limit;

	// Token: 0x04000A1E RID: 2590
	[SerializeField]
	private float FramesPerSecond;

	// Token: 0x04000A1F RID: 2591
	[SerializeField]
	private float CurrentSeconds;
}