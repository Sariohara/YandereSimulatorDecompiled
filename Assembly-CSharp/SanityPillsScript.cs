﻿using System;
using UnityEngine;

// Token: 0x020003EE RID: 1006
public class SanityPillsScript : MonoBehaviour
{
	// Token: 0x06001BD2 RID: 7122 RVA: 0x00142F58 File Offset: 0x00141158
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Yandere.Alphabet.Cheats++;
			this.Prompt.Yandere.Alphabet.UpdateDifficultyLabel();
			this.Prompt.Yandere.SanityPills = true;
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040030CD RID: 12493
	public PromptScript Prompt;
}
