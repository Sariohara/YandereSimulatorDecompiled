﻿using System;
using UnityEngine;

// Token: 0x0200047C RID: 1148
public class TranqCaseScript : MonoBehaviour
{
	// Token: 0x06001EC3 RID: 7875 RVA: 0x001AF343 File Offset: 0x001AD543
	private void Start()
	{
		this.Prompt.enabled = false;
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x001AF354 File Offset: 0x001AD554
	private void Update()
	{
		if (this.Yandere.transform.position.x > base.transform.position.x && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
		{
			if (this.Yandere.Dragging)
			{
				if (this.Ragdoll == null)
				{
					this.Ragdoll = this.Yandere.Ragdoll.GetComponent<RagdollScript>();
				}
				if (this.Ragdoll.Tranquil)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.enabled = true;
					}
				}
				else if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Prompt.enabled && this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TranquilHiding = true;
				this.Yandere.CanMove = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				this.Ragdoll.TranqCase = this;
				this.VictimClubType = this.Ragdoll.Student.Club;
				this.VictimID = this.Ragdoll.StudentID;
				this.Door.Prompt.enabled = true;
				this.Door.enabled = true;
				this.Occupied = true;
				this.Animate = true;
				this.Open = true;
			}
		}
		if (this.Animate)
		{
			if (this.Open)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 105f, Time.deltaTime * 10f);
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
				this.Ragdoll.Student.OsanaHairL.transform.localScale = Vector3.MoveTowards(this.Ragdoll.Student.OsanaHairL.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
				this.Ragdoll.Student.OsanaHairR.transform.localScale = Vector3.MoveTowards(this.Ragdoll.Student.OsanaHairR.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
				if (this.Rotation < 1f)
				{
					this.Animate = false;
					this.Rotation = 0f;
				}
			}
			this.Hinge.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		}
	}

	// Token: 0x04003FD0 RID: 16336
	public YandereScript Yandere;

	// Token: 0x04003FD1 RID: 16337
	public RagdollScript Ragdoll;

	// Token: 0x04003FD2 RID: 16338
	public PromptScript Prompt;

	// Token: 0x04003FD3 RID: 16339
	public DoorScript Door;

	// Token: 0x04003FD4 RID: 16340
	public Transform Hinge;

	// Token: 0x04003FD5 RID: 16341
	public bool Occupied;

	// Token: 0x04003FD6 RID: 16342
	public bool Open;

	// Token: 0x04003FD7 RID: 16343
	public int VictimID;

	// Token: 0x04003FD8 RID: 16344
	public ClubType VictimClubType;

	// Token: 0x04003FD9 RID: 16345
	public float Rotation;

	// Token: 0x04003FDA RID: 16346
	public bool Animate;
}