﻿using System;
using UnityEngine;

// Token: 0x020002C1 RID: 705
public class FakeStudentScript : MonoBehaviour
{
	// Token: 0x0600147B RID: 5243 RVA: 0x000C7DE9 File Offset: 0x000C5FE9
	private void Start()
	{
		this.targetRotation = base.transform.rotation;
		this.Student.Club = this.Club;
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x000C7E10 File Offset: 0x000C6010
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

	// Token: 0x04001FAB RID: 8107
	public StudentManagerScript StudentManager;

	// Token: 0x04001FAC RID: 8108
	public DialogueWheelScript DialogueWheel;

	// Token: 0x04001FAD RID: 8109
	public SubtitleScript Subtitle;

	// Token: 0x04001FAE RID: 8110
	public YandereScript Yandere;

	// Token: 0x04001FAF RID: 8111
	public StudentScript Student;

	// Token: 0x04001FB0 RID: 8112
	public PromptScript Prompt;

	// Token: 0x04001FB1 RID: 8113
	public Quaternion targetRotation;

	// Token: 0x04001FB2 RID: 8114
	public float RotationTimer;

	// Token: 0x04001FB3 RID: 8115
	public bool Rotate;

	// Token: 0x04001FB4 RID: 8116
	public ClubType Club;

	// Token: 0x04001FB5 RID: 8117
	public string LeaderAnim;
}