﻿using System;
using UnityEngine;

// Token: 0x02000347 RID: 839
public class KittenScript : MonoBehaviour
{
	// Token: 0x06001932 RID: 6450 RVA: 0x000FBE28 File Offset: 0x000FA028
	private void LateUpdate()
	{
		if (Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 5f)
		{
			if (!this.Yandere.Aiming)
			{
				Vector3 b = (this.Yandere.Head.transform.position.x < base.transform.position.x) ? this.Yandere.Head.transform.position : (base.transform.position + base.transform.forward + base.transform.up * 0.139854f);
				this.Target.position = Vector3.Lerp(this.Target.position, b, Time.deltaTime * 5f);
				this.Head.transform.LookAt(this.Target);
				return;
			}
			this.Head.transform.LookAt(this.Yandere.transform.position + Vector3.up * this.Head.position.y);
		}
	}

	// Token: 0x040027A6 RID: 10150
	public YandereScript Yandere;

	// Token: 0x040027A7 RID: 10151
	public GameObject Character;

	// Token: 0x040027A8 RID: 10152
	public string[] AnimationNames;

	// Token: 0x040027A9 RID: 10153
	public Transform Target;

	// Token: 0x040027AA RID: 10154
	public Transform Head;

	// Token: 0x040027AB RID: 10155
	public string CurrentAnim = string.Empty;

	// Token: 0x040027AC RID: 10156
	public string IdleAnim = string.Empty;

	// Token: 0x040027AD RID: 10157
	public bool Wait;

	// Token: 0x040027AE RID: 10158
	public float Timer;
}
