﻿using System;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class HomeCyberstalkScript : MonoBehaviour
{
	// Token: 0x06001857 RID: 6231 RVA: 0x000EAD28 File Offset: 0x000E8F28
	private void Update()
	{
		if (Input.GetButtonDown("A"))
		{
			this.HomeDarkness.Sprite.color = new Color(0f, 0f, 0f, 0f);
			this.HomeDarkness.Cyberstalking = true;
			this.HomeDarkness.FadeOut = true;
			base.gameObject.SetActive(false);
			for (int i = 1; i < 26; i++)
			{
				ConversationGlobals.SetTopicLearnedByStudent(i, this.HomeDarkness.HomeCamera.HomeInternet.Student, true);
				ConversationGlobals.SetTopicDiscovered(i, true);
			}
		}
		if (Input.GetButtonDown("B"))
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0400242E RID: 9262
	public HomeDarknessScript HomeDarkness;
}