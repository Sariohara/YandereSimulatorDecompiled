﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200044E RID: 1102
public class StudentInfoMenuScript : MonoBehaviour
{
	// Token: 0x06001D27 RID: 7463 RVA: 0x0015BA30 File Offset: 0x00159C30
	private void Start()
	{
		StudentGlobals.GetStudentPhotographed(11);
		this.BusyBlocker.position = new Vector3(0f, 0f, 0f);
		for (int i = 1; i < 101; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StudentPortrait, base.transform.position, Quaternion.identity);
			gameObject.transform.parent = this.PortraitGrid;
			gameObject.transform.localPosition = new Vector3(-300f + (float)this.Column * 150f, 80f - (float)this.Row * 160f, 0f);
			gameObject.transform.localEulerAngles = Vector3.zero;
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			this.StudentPortraits[i] = gameObject.GetComponent<StudentPortraitScript>();
			this.Column++;
			if (this.Column > 4)
			{
				this.Column = 0;
				this.Row++;
			}
		}
		if (this.PauseScreen.Eighties)
		{
			this.Headmaster = this.EightiesHeadmaster;
			this.Counselor = this.EightiesCounselor;
			this.InfoChan = this.Journalist;
		}
		this.Column = 0;
		this.Row = 0;
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x0015BB88 File Offset: 0x00159D88
	private void Update()
	{
		if (!this.GrabbedPortraits)
		{
			base.StartCoroutine(this.UpdatePortraits());
			this.GrabbedPortraits = true;
			if (this.PauseScreen.Eighties)
			{
				this.PauseScreen.BlackenAllText();
			}
		}
		if (Input.GetButtonDown("A"))
		{
			if (this.PromptBar.Label[0].text != string.Empty)
			{
				if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
				{
					if (this.UsingLifeNote)
					{
						this.PauseScreen.MainMenu.SetActive(true);
						this.PauseScreen.Sideways = false;
						this.PauseScreen.Show = false;
						base.gameObject.SetActive(false);
						this.NoteWindow.TargetStudent = this.StudentID;
						this.NoteWindow.gameObject.SetActive(true);
						this.NoteWindow.SlotLabels[1].text = this.StudentManager.Students[this.StudentID].Name;
						this.NoteWindow.SlotsFilled[1] = true;
						this.UsingLifeNote = false;
						this.PromptBar.Label[0].text = "Confirm";
						this.PromptBar.UpdateButtons();
						this.NoteWindow.CheckForCompletion();
					}
					else
					{
						this.StudentInfo.gameObject.SetActive(true);
						this.StudentInfo.UpdateInfo(this.StudentID);
						this.StudentInfo.Topics.SetActive(false);
						base.gameObject.SetActive(false);
						this.PromptBar.ClearButtons();
						if (this.Gossiping)
						{
							this.PromptBar.Label[0].text = "Gossip";
						}
						if (this.Distracting)
						{
							this.PromptBar.Label[0].text = "Distract";
						}
						if (this.CyberBullying || this.CyberStalking)
						{
							this.PromptBar.Label[0].text = "Accept";
						}
						if (this.FindingLocker)
						{
							this.PromptBar.Label[0].text = "Find Locker";
						}
						if (this.MatchMaking)
						{
							this.PromptBar.Label[0].text = "Match";
						}
						if (this.Targeting || this.UsingLifeNote)
						{
							this.PromptBar.Label[0].text = "Kill";
						}
						if (this.SendingHome)
						{
							this.PromptBar.Label[0].text = "Send Home";
						}
						if (this.FiringCouncilMember)
						{
							this.PromptBar.Label[0].text = "Fire";
						}
						if (this.GettingOpinions)
						{
							this.PromptBar.Label[0].text = "Get Opinions";
						}
						if (this.StudentManager.Students[this.StudentID] != null)
						{
							if (this.StudentManager.Students[this.StudentID].gameObject.activeInHierarchy)
							{
								if (this.StudentManager.Tag.Target == this.StudentManager.Students[this.StudentID].Head)
								{
									this.PromptBar.Label[2].text = "Untag";
								}
								else
								{
									this.PromptBar.Label[2].text = "Tag";
								}
							}
							else
							{
								this.PromptBar.Label[2].text = "";
							}
						}
						else
						{
							this.PromptBar.Label[2].text = "";
						}
						this.PromptBar.Label[1].text = "Back";
						this.PromptBar.Label[3].text = "Interests";
						this.PromptBar.Label[6].text = "Reputation";
						this.PromptBar.UpdateButtons();
					}
				}
				else
				{
					StudentGlobals.SetStudentPhotographed(this.StudentID, true);
					if (this.StudentManager.Students[this.StudentID] != null)
					{
						for (int i = 0; i < this.StudentManager.Students[this.StudentID].Outlines.Length; i++)
						{
							if (this.StudentManager.Students[this.StudentID].Outlines[i] != null)
							{
								this.StudentManager.Students[this.StudentID].Outlines[i].enabled = true;
							}
						}
					}
					this.PauseScreen.ServiceMenu.gameObject.SetActive(true);
					this.PauseScreen.ServiceMenu.UpdateList();
					this.PauseScreen.ServiceMenu.UpdateDesc();
					this.PauseScreen.ServiceMenu.Purchase();
					this.GettingInfo = false;
					base.gameObject.SetActive(false);
				}
				if (this.PauseScreen.Eighties)
				{
					this.PauseScreen.BlackenAllText();
				}
			}
		}
		else if (Input.GetButtonDown("B"))
		{
			this.BusyBlocker.position = new Vector3(0f, 0f, 0f);
			if (this.Gossiping || this.Distracting || this.MatchMaking || this.Targeting)
			{
				if (this.Targeting)
				{
					this.PauseScreen.Yandere.RPGCamera.enabled = true;
				}
				this.PauseScreen.Yandere.Interaction = YandereInteractionType.Bye;
				this.PauseScreen.Yandere.TalkTimer = 2f;
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				this.Distracting = false;
				this.MatchMaking = false;
				this.Gossiping = false;
				this.Targeting = false;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.CyberBullying || this.CyberStalking || this.FindingLocker)
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				base.gameObject.SetActive(false);
				Time.timeScale = 1f;
				if (this.FindingLocker)
				{
					this.PauseScreen.Yandere.RPGCamera.enabled = true;
				}
				this.FindingLocker = false;
				this.PromptBar.ClearButtons();
				this.PromptBar.Show = false;
			}
			else if (this.SendingHome || this.GettingInfo || this.GettingOpinions || this.FiringCouncilMember)
			{
				this.PauseScreen.ServiceMenu.gameObject.SetActive(true);
				this.PauseScreen.ServiceMenu.UpdateList();
				this.PauseScreen.ServiceMenu.UpdateDesc();
				base.gameObject.SetActive(false);
				this.FiringCouncilMember = false;
				this.GettingOpinions = false;
				this.SendingHome = false;
				this.GettingInfo = false;
			}
			else if (this.UsingLifeNote)
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.Show = false;
				base.gameObject.SetActive(false);
				this.NoteWindow.gameObject.SetActive(true);
				this.UsingLifeNote = false;
			}
			else
			{
				this.PauseScreen.MainMenu.SetActive(true);
				this.PauseScreen.Sideways = false;
				this.PauseScreen.PressedB = true;
				base.gameObject.SetActive(false);
				this.PromptBar.ClearButtons();
				this.PromptBar.Label[0].text = "Accept";
				this.PromptBar.Label[1].text = "Exit";
				this.PromptBar.Label[4].text = "Choose";
				this.PromptBar.UpdateButtons();
				this.PromptBar.Show = true;
			}
		}
		float t = Time.unscaledDeltaTime * 10f;
		float num = (float)((this.Row % 2 == 0) ? (this.Row / 2) : ((this.Row - 1) / 2));
		float b = 320f * num;
		this.PortraitGrid.localPosition = new Vector3(this.PortraitGrid.localPosition.x, Mathf.Lerp(this.PortraitGrid.localPosition.y, b, t), this.PortraitGrid.localPosition.z);
		this.Scrollbar.localPosition = new Vector3(this.Scrollbar.localPosition.x, Mathf.Lerp(this.Scrollbar.localPosition.y, 175f - 350f * (this.PortraitGrid.localPosition.y / 2880f), t), this.Scrollbar.localPosition.z);
		if (this.InputManager.TappedUp)
		{
			this.Row--;
			if (this.Row < 0)
			{
				this.Row = this.Rows - 1;
			}
			this.UpdateHighlight();
		}
		if (this.InputManager.TappedDown)
		{
			this.Row++;
			if (this.Row > this.Rows - 1)
			{
				this.Row = 0;
			}
			this.UpdateHighlight();
		}
		if (this.InputManager.TappedRight)
		{
			this.Column++;
			if (this.Column > this.Columns - 1)
			{
				this.Column = 0;
			}
			this.UpdateHighlight();
		}
		if (this.InputManager.TappedLeft)
		{
			this.Column--;
			if (this.Column < 0)
			{
				this.Column = this.Columns - 1;
			}
			this.UpdateHighlight();
		}
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x0015C55C File Offset: 0x0015A75C
	public void UpdateHighlight()
	{
		this.Highlight.localPosition = new Vector3(-300f + (float)this.Column * 150f, 80f - (float)this.Row * 160f, this.Highlight.localPosition.z);
		this.BusyBlocker.position = new Vector3(0f, 0f, 0f);
		this.StudentID = 1 + (this.Column + this.Row * this.Columns);
		if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
		{
			this.PromptBar.Label[0].text = "View Info";
			this.PromptBar.UpdateButtons();
		}
		else
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.Gossiping && (this.StudentID == 1 || this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID || this.JSON.Students[this.StudentID].Club == ClubType.Sports || this.StudentManager.Students[this.StudentID] == null || StudentGlobals.GetStudentDead(this.StudentID) || this.StudentID > 97))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.CyberBullying && (this.JSON.Students[this.StudentID].Gender == 1 || StudentGlobals.GetStudentDead(this.StudentID) || this.StudentID > 97))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.CyberStalking && (StudentGlobals.GetStudentDead(this.StudentID) || StudentGlobals.GetStudentArrested(this.StudentID) || this.StudentID > 97))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.FindingLocker && (this.StudentID == 1 || this.StudentID > 89 || (this.StudentManager.Students[this.StudentID] != null && this.StudentManager.Students[this.StudentID].Club == ClubType.Council) || StudentGlobals.GetStudentDead(this.StudentID)))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.Distracting)
		{
			this.Dead = false;
			if (this.StudentManager.Students[this.StudentID] == null)
			{
				this.Dead = true;
			}
			if (this.Dead)
			{
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
			else if (this.StudentID == 1 || !this.StudentManager.Students[this.StudentID].Alive || this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID || StudentGlobals.GetStudentKidnapped(this.StudentID) || this.StudentManager.Students[this.StudentID].Tranquil || this.StudentManager.Students[this.StudentID].Teacher || this.StudentManager.Students[this.StudentID].Slave || StudentGlobals.GetStudentDead(this.StudentID) || this.StudentManager.Students[this.StudentID].MyBento.Tampered || this.StudentID > 97)
			{
				if (this.StudentID > 1 && this.StudentManager.Students[this.StudentID] != null && this.StudentManager.Students[this.StudentID].InEvent)
				{
					this.BusyBlocker.position = this.Highlight.position;
				}
				this.PromptBar.Label[0].text = string.Empty;
				this.PromptBar.UpdateButtons();
			}
		}
		if (this.MatchMaking && (this.StudentID == this.PauseScreen.Yandere.TargetStudent.StudentID || StudentGlobals.GetStudentDead(this.StudentID) || this.StudentID > 97))
		{
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.Targeting && (this.StudentID == 1 || this.StudentID > 97 || StudentGlobals.GetStudentDead(this.StudentID) || (this.StudentManager.Students[this.StudentID] != null && !this.StudentManager.Students[this.StudentID].gameObject.activeInHierarchy) || (this.StudentManager.Students[this.StudentID] != null && this.StudentManager.Students[this.StudentID].InEvent) || (this.StudentManager.Students[this.StudentID] != null && this.StudentManager.Students[this.StudentID].Tranquil)))
		{
			if (this.StudentID > 1 && this.StudentManager.Students[this.StudentID] != null && this.StudentManager.Students[this.StudentID].InEvent)
			{
				this.BusyBlocker.position = this.Highlight.position;
			}
			this.PromptBar.Label[0].text = string.Empty;
			this.PromptBar.UpdateButtons();
		}
		if (this.SendingHome)
		{
			Debug.Log("Highlighting student number " + this.StudentID.ToString());
			if (this.StudentManager.Students[this.StudentID] != null)
			{
				StudentScript studentScript = this.StudentManager.Students[this.StudentID];
				if (this.StudentID == 1 || StudentGlobals.GetStudentDead(this.StudentID) || (this.StudentID < 98 && studentScript.SentHome) || (this.StudentID > 97 || StudentGlobals.StudentSlave == this.StudentID || (studentScript.Club == ClubType.MartialArts && studentScript.ClubAttire)) || (studentScript.Club == ClubType.Sports && studentScript.ClubAttire) || this.StudentManager.Students[this.StudentID].CameraReacting || !StudentGlobals.GetStudentPhotographed(this.StudentID) || studentScript.Wet || studentScript.Slave || studentScript.Phoneless)
				{
					this.PromptBar.Label[0].text = string.Empty;
					this.PromptBar.UpdateButtons();
				}
			}
		}
		if (this.GettingInfo)
		{
			if (StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentID > 97)
			{
				this.PromptBar.Label[0].text = string.Empty;
			}
			else
			{
				this.PromptBar.Label[0].text = "Get Info";
			}
			this.PromptBar.UpdateButtons();
		}
		if (this.GettingOpinions)
		{
			this.PromptBar.Label[0].text = "Get Opinions";
			this.PromptBar.UpdateButtons();
		}
		if (this.UsingLifeNote)
		{
			if (this.StudentID == 1 || this.StudentID > 97 || (this.StudentID > 11 && this.StudentID < 21) || this.StudentPortraits[this.StudentID].DeathShadow.activeInHierarchy || (this.StudentManager.Students[this.StudentID] != null && !this.StudentManager.Students[this.StudentID].enabled))
			{
				this.PromptBar.Label[0].text = "";
			}
			else
			{
				this.PromptBar.Label[0].text = "Kill";
			}
			this.PromptBar.UpdateButtons();
		}
		if (this.FiringCouncilMember)
		{
			if (this.StudentManager.Students[this.StudentID] != null)
			{
				if (!StudentGlobals.GetStudentPhotographed(this.StudentID) || this.StudentManager.Students[this.StudentID].Club != ClubType.Council)
				{
					this.PromptBar.Label[0].text = "";
				}
				else
				{
					this.PromptBar.Label[0].text = "Fire";
				}
			}
			this.PromptBar.UpdateButtons();
		}
		if (MissionModeGlobals.MissionMode && this.StudentID == 1)
		{
			this.PromptBar.Label[0].text = "";
		}
		if (this.PauseScreen.Eighties && this.Headmaster != this.EightiesHeadmaster)
		{
			this.Headmaster = this.EightiesHeadmaster;
			this.Counselor = this.EightiesCounselor;
			this.InfoChan = this.Journalist;
			if (this.StudentPortraits[98] != null)
			{
				this.StudentPortraits[98].Portrait.mainTexture = this.Counselor;
				this.StudentPortraits[99].Portrait.mainTexture = this.Headmaster;
				this.StudentPortraits[100].Portrait.mainTexture = this.InfoChan;
			}
		}
		this.UpdateNameLabel();
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x0015CF18 File Offset: 0x0015B118
	private void UpdateNameLabel()
	{
		if (this.StudentID > 97 || StudentGlobals.GetStudentPhotographed(this.StudentID) || this.GettingInfo)
		{
			this.NameLabel.text = this.JSON.Students[this.StudentID].Name;
			return;
		}
		this.NameLabel.text = "Unknown";
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x0015CF77 File Offset: 0x0015B177
	public IEnumerator UpdatePortraits()
	{
		if (this.Debugging)
		{
			Debug.Log("The Student Info Menu was instructed to get photos.");
		}
		string EightiesPrefix = "";
		if (this.PauseScreen.Eighties)
		{
			EightiesPrefix = "1989";
		}
		int num;
		for (int ID = 1; ID < 101; ID = num + 1)
		{
			if (ID == 0)
			{
				this.StudentPortraits[ID].Portrait.mainTexture = this.InfoChan;
			}
			else if (!this.PortraitLoaded[ID])
			{
				if (ID < 98)
				{
					if (this.PauseScreen.Eighties || (!this.PauseScreen.Eighties && ID < 12) || (!this.PauseScreen.Eighties && ID > 20))
					{
						if (StudentGlobals.GetStudentPhotographed(ID))
						{
							string url = string.Concat(new string[]
							{
								"file:///",
								Application.streamingAssetsPath,
								"/Portraits",
								EightiesPrefix,
								"/Student_",
								ID.ToString(),
								".png"
							});
							WWW www = new WWW(url);
							yield return www;
							if (www.error == null)
							{
								if (!StudentGlobals.GetStudentReplaced(ID))
								{
									this.StudentPortraits[ID].Portrait.mainTexture = www.texture;
								}
								else
								{
									this.StudentPortraits[ID].Portrait.mainTexture = this.BlankPortrait;
								}
							}
							else
							{
								this.StudentPortraits[ID].Portrait.mainTexture = this.UnknownPortrait;
							}
							this.PortraitLoaded[ID] = true;
							www = null;
						}
						else
						{
							this.StudentPortraits[ID].Portrait.mainTexture = this.UnknownPortrait;
						}
					}
					else
					{
						this.StudentPortraits[ID].Portrait.mainTexture = this.RivalPortraits[ID];
					}
				}
				else if (ID == 98)
				{
					this.StudentPortraits[ID].Portrait.mainTexture = this.Counselor;
				}
				else if (ID == 99)
				{
					this.StudentPortraits[ID].Portrait.mainTexture = this.Headmaster;
				}
				else if (ID == 100)
				{
					this.StudentPortraits[ID].Portrait.mainTexture = this.InfoChan;
				}
				else
				{
					this.StudentPortraits[ID].Portrait.mainTexture = this.RivalPortraits[ID];
				}
			}
			if (this.StudentManager.PantyShotTaken[ID] || PlayerGlobals.GetStudentPantyShot(ID))
			{
				this.StudentPortraits[ID].Panties.SetActive(true);
			}
			if (this.StudentManager.Students[ID] != null)
			{
				this.StudentPortraits[ID].Friend.SetActive(this.StudentManager.Students[ID].Friend);
			}
			if (StudentGlobals.GetStudentDying(ID) || StudentGlobals.GetStudentDead(ID))
			{
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}
			if (MissionModeGlobals.MissionMode && ID == 1)
			{
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}
			if (SceneManager.GetActiveScene().name == "SchoolScene" && this.StudentManager.Students[ID] != null && this.StudentManager.Students[ID].Tranquil)
			{
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}
			if (StudentGlobals.GetStudentArrested(ID))
			{
				this.StudentPortraits[ID].PrisonBars.SetActive(true);
				this.StudentPortraits[ID].DeathShadow.SetActive(true);
			}
			num = ID;
		}
		yield break;
	}

	// Token: 0x04003569 RID: 13673
	public StudentManagerScript StudentManager;

	// Token: 0x0400356A RID: 13674
	public InputManagerScript InputManager;

	// Token: 0x0400356B RID: 13675
	public PauseScreenScript PauseScreen;

	// Token: 0x0400356C RID: 13676
	public StudentInfoScript StudentInfo;

	// Token: 0x0400356D RID: 13677
	public NoteWindowScript NoteWindow;

	// Token: 0x0400356E RID: 13678
	public PromptBarScript PromptBar;

	// Token: 0x0400356F RID: 13679
	public JsonScript JSON;

	// Token: 0x04003570 RID: 13680
	public GameObject StudentPortrait;

	// Token: 0x04003571 RID: 13681
	public Texture UnknownPortrait;

	// Token: 0x04003572 RID: 13682
	public Texture BlankPortrait;

	// Token: 0x04003573 RID: 13683
	public Texture Headmaster;

	// Token: 0x04003574 RID: 13684
	public Texture Counselor;

	// Token: 0x04003575 RID: 13685
	public Texture InfoChan;

	// Token: 0x04003576 RID: 13686
	public Texture EightiesHeadmaster;

	// Token: 0x04003577 RID: 13687
	public Texture EightiesCounselor;

	// Token: 0x04003578 RID: 13688
	public Texture Journalist;

	// Token: 0x04003579 RID: 13689
	public Transform PortraitGrid;

	// Token: 0x0400357A RID: 13690
	public Transform BusyBlocker;

	// Token: 0x0400357B RID: 13691
	public Transform Highlight;

	// Token: 0x0400357C RID: 13692
	public Transform Scrollbar;

	// Token: 0x0400357D RID: 13693
	public StudentPortraitScript[] StudentPortraits;

	// Token: 0x0400357E RID: 13694
	public Texture[] RivalPortraits;

	// Token: 0x0400357F RID: 13695
	public bool[] PortraitLoaded;

	// Token: 0x04003580 RID: 13696
	public UISprite[] DeathShadows;

	// Token: 0x04003581 RID: 13697
	public UISprite[] Friends;

	// Token: 0x04003582 RID: 13698
	public UISprite[] Panties;

	// Token: 0x04003583 RID: 13699
	public UITexture[] PrisonBars;

	// Token: 0x04003584 RID: 13700
	public UITexture[] Portraits;

	// Token: 0x04003585 RID: 13701
	public UILabel NameLabel;

	// Token: 0x04003586 RID: 13702
	public bool FiringCouncilMember;

	// Token: 0x04003587 RID: 13703
	public bool GettingOpinions;

	// Token: 0x04003588 RID: 13704
	public bool CyberBullying;

	// Token: 0x04003589 RID: 13705
	public bool CyberStalking;

	// Token: 0x0400358A RID: 13706
	public bool FindingLocker;

	// Token: 0x0400358B RID: 13707
	public bool UsingLifeNote;

	// Token: 0x0400358C RID: 13708
	public bool GettingInfo;

	// Token: 0x0400358D RID: 13709
	public bool MatchMaking;

	// Token: 0x0400358E RID: 13710
	public bool Distracting;

	// Token: 0x0400358F RID: 13711
	public bool SendingHome;

	// Token: 0x04003590 RID: 13712
	public bool Gossiping;

	// Token: 0x04003591 RID: 13713
	public bool Targeting;

	// Token: 0x04003592 RID: 13714
	public bool Dead;

	// Token: 0x04003593 RID: 13715
	public int[] SetSizes;

	// Token: 0x04003594 RID: 13716
	public int StudentID;

	// Token: 0x04003595 RID: 13717
	public int Column;

	// Token: 0x04003596 RID: 13718
	public int Row;

	// Token: 0x04003597 RID: 13719
	public int Set;

	// Token: 0x04003598 RID: 13720
	public int Columns;

	// Token: 0x04003599 RID: 13721
	public int Rows;

	// Token: 0x0400359A RID: 13722
	public bool GrabbedPortraits;

	// Token: 0x0400359B RID: 13723
	public bool Debugging;
}
