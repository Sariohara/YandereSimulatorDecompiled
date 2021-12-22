﻿using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000464 RID: 1124
public class TaskListScript : MonoBehaviour
{
	// Token: 0x06001E6E RID: 7790 RVA: 0x001AA544 File Offset: 0x001A8744
	private void Update()
	{
		if (this.InputManager.TappedUp)
		{
			if (this.ID == 1)
			{
				this.ListPosition--;
				if (this.ListPosition < 0)
				{
					this.ListPosition = this.Limit - 16;
					this.ID = 16;
				}
			}
			else
			{
				this.ID--;
			}
			this.UpdateTaskList();
			base.StartCoroutine(this.UpdateTaskInfo());
		}
		if (this.InputManager.TappedDown)
		{
			if (this.ID == 16)
			{
				this.ListPosition++;
				if (this.ID + this.ListPosition > this.Limit)
				{
					this.ListPosition = 0;
					this.ID = 1;
				}
			}
			else
			{
				this.ID++;
			}
			this.UpdateTaskList();
			base.StartCoroutine(this.UpdateTaskInfo());
		}
		if (this.Tutorials)
		{
			if (!this.TutorialWindow.Hide && !this.TutorialWindow.Show)
			{
				if (Input.GetButtonDown("A"))
				{
					OptionGlobals.TutorialsOff = false;
					this.TutorialWindow.ForceID = this.ListPosition + this.ID;
					this.TutorialWindow.ShowTutorial();
					this.TutorialWindow.enabled = true;
					this.TutorialWindow.SummonWindow();
					return;
				}
				if (Input.GetButtonDown("B"))
				{
					this.Exit();
					return;
				}
			}
		}
		else if (Input.GetButtonDown("B"))
		{
			this.Exit();
		}
	}

	// Token: 0x06001E6F RID: 7791 RVA: 0x001AA6B8 File Offset: 0x001A88B8
	public void UpdateTaskList()
	{
		if (!this.TaskWindow.TaskManager.Initialized)
		{
			this.TaskWindow.TaskManager.Start();
		}
		if (this.Tutorials)
		{
			for (int i = 1; i < this.TaskNameLabels.Length; i++)
			{
				this.TaskNameLabels[i].text = this.TutorialNames[i + this.ListPosition];
			}
			return;
		}
		for (int j = 1; j < this.TaskNameLabels.Length; j++)
		{
			if (this.TaskWindow.TaskManager.TaskStatus[j + this.ListPosition] == 0)
			{
				this.TaskNameLabels[j].text = "Undiscovered Task #" + (j + this.ListPosition).ToString();
			}
			else
			{
				this.TaskNameLabels[j].text = this.JSON.Students[j + this.ListPosition].Name + "'s Task";
			}
			this.Checkmarks[j].enabled = (this.TaskWindow.TaskManager.TaskStatus[j + this.ListPosition] == 3);
		}
	}

	// Token: 0x06001E70 RID: 7792 RVA: 0x001AA7D5 File Offset: 0x001A89D5
	public IEnumerator UpdateTaskInfo()
	{
		this.Highlight.localPosition = new Vector3(this.Highlight.localPosition.x, 200f - 25f * (float)this.ID, this.Highlight.localPosition.z);
		if (this.Tutorials)
		{
			this.TaskIcon.mainTexture = this.TutorialTextures[this.ID + this.ListPosition];
			this.TaskDesc.text = "This tutorial will teach you about " + this.TutorialNames[this.ID + this.ListPosition];
		}
		else
		{
			string text = "";
			if (GameGlobals.Eighties)
			{
				text = "1989";
			}
			if (this.TaskWindow.TaskManager.TaskStatus[this.ID + this.ListPosition] == 0)
			{
				this.StudentIcon.mainTexture = this.Silhouette;
				this.TaskIcon.mainTexture = this.QuestionMark;
				this.TaskDesc.text = "This task has not been discovered yet.";
			}
			else
			{
				string url = string.Concat(new string[]
				{
					"file:///",
					Application.streamingAssetsPath,
					"/Portraits",
					text,
					"/Student_",
					(this.ID + this.ListPosition).ToString(),
					".png"
				});
				WWW www = new WWW(url);
				yield return www;
				this.StudentIcon.mainTexture = www.texture;
				this.TaskWindow.AltGenericCheck(this.ID + this.ListPosition);
				if (this.TaskWindow.Generic)
				{
					this.TaskIcon.mainTexture = this.TaskWindow.Icons[0];
					this.TaskDesc.text = this.TaskWindow.Descriptions[0];
				}
				else
				{
					this.TaskIcon.mainTexture = this.TaskWindow.Icons[this.ID + this.ListPosition];
					this.TaskDesc.text = this.TaskWindow.Descriptions[this.ID + this.ListPosition];
				}
				www = null;
			}
		}
		yield break;
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x001AA7E4 File Offset: 0x001A89E4
	public void Exit()
	{
		this.PauseScreen.PromptBar.ClearButtons();
		this.PauseScreen.PromptBar.Label[0].text = "Accept";
		this.PauseScreen.PromptBar.Label[1].text = "Back";
		this.PauseScreen.PromptBar.Label[4].text = "Choose";
		this.PauseScreen.PromptBar.Label[5].text = "Choose";
		this.PauseScreen.PromptBar.UpdateButtons();
		this.PauseScreen.Sideways = false;
		this.PauseScreen.PressedB = true;
		this.MainMenu.SetActive(true);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04003EC7 RID: 16071
	public TutorialWindowScript TutorialWindow;

	// Token: 0x04003EC8 RID: 16072
	public InputManagerScript InputManager;

	// Token: 0x04003EC9 RID: 16073
	public PauseScreenScript PauseScreen;

	// Token: 0x04003ECA RID: 16074
	public TaskWindowScript TaskWindow;

	// Token: 0x04003ECB RID: 16075
	public JsonScript JSON;

	// Token: 0x04003ECC RID: 16076
	public GameObject MainMenu;

	// Token: 0x04003ECD RID: 16077
	public UITexture StudentIcon;

	// Token: 0x04003ECE RID: 16078
	public UITexture TaskIcon;

	// Token: 0x04003ECF RID: 16079
	public UILabel TaskDesc;

	// Token: 0x04003ED0 RID: 16080
	public Texture QuestionMark;

	// Token: 0x04003ED1 RID: 16081
	public Transform Highlight;

	// Token: 0x04003ED2 RID: 16082
	public Texture Silhouette;

	// Token: 0x04003ED3 RID: 16083
	public UILabel[] TaskNameLabels;

	// Token: 0x04003ED4 RID: 16084
	public UISprite[] Checkmarks;

	// Token: 0x04003ED5 RID: 16085
	public Texture[] TutorialTextures;

	// Token: 0x04003ED6 RID: 16086
	public string[] TutorialDescs;

	// Token: 0x04003ED7 RID: 16087
	public string[] TutorialNames;

	// Token: 0x04003ED8 RID: 16088
	public int ListPosition;

	// Token: 0x04003ED9 RID: 16089
	public int Limit = 84;

	// Token: 0x04003EDA RID: 16090
	public int ID = 1;

	// Token: 0x04003EDB RID: 16091
	public bool Tutorials;
}
