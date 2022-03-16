﻿using System;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public class FakeStudentScript : MonoBehaviour
{
	// Token: 0x0600148D RID: 5261 RVA: 0x000C90C5 File Offset: 0x000C72C5
	private void Start()
	{
		this.targetRotation = base.transform.rotation;
		this.Student.Club = this.Club;
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x000C90EC File Offset: 0x000C72EC
	private void Update()
	{
		if (!this.Student.Talking)
		{
			if (this.LeaderAnim != "")
			{
				base.GetComponent<Animation>().CrossFade(this.LeaderAnim);
			}
			if (this.Rotate)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.targetRotation, 10f * Time.deltaTime);
				this.RotationTimer += Time.deltaTime;
				if (this.RotationTimer > 1f)
				{
					this.RotationTimer = 0f;
					this.Rotate = false;
				}
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f && !this.Yandere.Chased && this.Yandere.Chasers == 0)
		{
			this.Yandere.TargetStudent = this.Student;
			this.Subtitle.UpdateLabel(SubtitleType.ClubGreeting, (int)this.Student.Club, 4f);
			this.DialogueWheel.ClubLeader = true;
			this.StudentManager.DisablePrompts();
			this.DialogueWheel.HideShadows();
			this.DialogueWheel.Show = true;
			this.DialogueWheel.Panel.enabled = true;
			this.Student.Talking = true;
			this.Student.TalkTimer = 0f;
			this.Yandere.ShoulderCamera.OverShoulder = true;
			this.Yandere.WeaponMenu.KeyboardShow = false;
			this.Yandere.WeaponMenu.Show = false;
			this.Yandere.YandereVision = false;
			this.Yandere.CanMove = false;
			this.Yandere.Talking = true;
			this.RotationTimer = 0f;
			this.Rotate = true;
		}
	}

	// Token: 0x04001FDF RID: 8159
	public StudentManagerScript StudentManager;

	// Token: 0x04001FE0 RID: 8160
	public DialogueWheelScript DialogueWheel;

	// Token: 0x04001FE1 RID: 8161
	public SubtitleScript Subtitle;

	// Token: 0x04001FE2 RID: 8162
	public YandereScript Yandere;

	// Token: 0x04001FE3 RID: 8163
	public StudentScript Student;

	// Token: 0x04001FE4 RID: 8164
	public PromptScript Prompt;

	// Token: 0x04001FE5 RID: 8165
	public Quaternion targetRotation;

	// Token: 0x04001FE6 RID: 8166
	public float RotationTimer;

	// Token: 0x04001FE7 RID: 8167
	public bool Rotate;

	// Token: 0x04001FE8 RID: 8168
	public ClubType Club;

	// Token: 0x04001FE9 RID: 8169
	public string LeaderAnim;
}