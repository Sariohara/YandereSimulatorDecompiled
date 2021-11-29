﻿using System;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class PantyDetectorScript : MonoBehaviour
{
	// Token: 0x06001A2F RID: 6703 RVA: 0x001150E2 File Offset: 0x001132E2
	private void Update()
	{
		if (this.Frame == 1)
		{
			this.Yandere.StudentManager.UpdatePanties(false);
			UnityEngine.Object.Destroy(base.gameObject);
		}
		this.Frame++;
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x00115118 File Offset: 0x00113318
	private void OnTriggerEnter(Collider other)
	{
		if (this.Student == null && other.gameObject.name == "Panties")
		{
			this.Student = other.gameObject.transform.root.gameObject.GetComponent<StudentScript>();
			this.Yandere.ResetYandereEffects();
			this.Yandere.Shutter.PhotoDescLabel.text = "Photo of: " + this.Student.Name + "'s Panties";
			this.Yandere.Shutter.PhotoIcons.SetActive(true);
			this.Yandere.Shutter.PantiesX.SetActive(false);
			this.Yandere.Shutter.InfoX.SetActive(true);
			this.Yandere.Shutter.Student = this.Student;
			Time.timeScale = 0f;
			this.Yandere.Shutter.Panel.SetActive(true);
			this.Yandere.Shutter.MainMenu.SetActive(false);
			this.Yandere.PauseScreen.Show = true;
			this.Yandere.PauseScreen.Panel.enabled = true;
			this.Yandere.PromptBar.ClearButtons();
			this.Yandere.PromptBar.Label[1].text = "Exit";
			this.Yandere.PromptBar.UpdateButtons();
			this.Yandere.PromptBar.Show = true;
			this.Yandere.PauseScreen.Sideways = false;
			this.Yandere.Shutter.TextMessages.gameObject.SetActive(true);
			this.Yandere.Shutter.SpawnMessage();
		}
	}

	// Token: 0x04002AB4 RID: 10932
	public YandereScript Yandere;

	// Token: 0x04002AB5 RID: 10933
	public StudentScript Student;

	// Token: 0x04002AB6 RID: 10934
	public int Frame;
}