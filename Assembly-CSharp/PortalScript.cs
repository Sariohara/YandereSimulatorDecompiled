﻿using System;
using UnityEngine;

// Token: 0x020003AC RID: 940
public class PortalScript : MonoBehaviour
{
	// Token: 0x06001AB2 RID: 6834 RVA: 0x00122DD8 File Offset: 0x00120FD8
	private void Start()
	{
		this.EvidenceWarning.SetActive(false);
		this.ClassDarkness.enabled = false;
		if (GameGlobals.EightiesTutorial)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x00122E08 File Offset: 0x00121008
	private void Update()
	{
		if (this.Clock.HourTime > 8.52f && this.Clock.HourTime < 8.53f && !this.Yandere.InClass && !this.LateReport1)
		{
			this.LateReport1 = true;
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Late);
		}
		if (this.Clock.HourTime > 13.52f && this.Clock.HourTime < 13.53f && !this.Yandere.InClass && !this.LateReport2)
		{
			this.LateReport2 = true;
			this.Yandere.NotificationManager.DisplayNotification(NotificationType.Late);
		}
		bool flag = false;
		if (this.EvidenceWarning.activeInHierarchy)
		{
			if (Input.GetButtonDown("A"))
			{
				this.EvidenceWarning.SetActive(false);
				this.BypassWarning = true;
				flag = true;
			}
			if (Input.GetButtonDown("B"))
			{
				this.EvidenceWarning.SetActive(false);
				this.Yandere.CanMove = true;
			}
		}
		if (this.Prompt.Circle[0].fillAmount == 0f || flag)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if ((!this.BypassWarning && this.Police.Corpses - this.Police.HiddenCorpses > 0) || (!this.BypassWarning && this.Police.LimbParent.childCount > 0) || (!this.BypassWarning && this.Police.BloodParent.childCount > 0) || (!this.BypassWarning && this.Police.BloodyClothing > 0) || (!this.BypassWarning && this.Police.BloodyWeapons > 0))
			{
				this.CorpsesLabel.text = "Corpses: " + (this.Police.Corpses - this.Police.HiddenCorpses).ToString();
				this.BodyPartsLabel.text = "Body Parts: " + this.Police.LimbParent.childCount.ToString();
				this.BloodStainsLabel.text = "Blood Stains: " + this.Police.BloodParent.childCount.ToString();
				this.BloodyClothingLabel.text = "Bloody Clothing: " + this.Police.BloodyClothing.ToString();
				this.BloodyWeaponsLabel.text = "Bloody Weapons: " + this.Police.BloodyWeapons.ToString();
				if (this.Clock.HourTime > 13.5f)
				{
					this.BottomLabel.text = "If you try to leave school right now, the police will be called.";
					this.AttendClassLabel.text = "Leave School";
				}
				this.EvidenceWarning.SetActive(true);
				this.Yandere.CanMove = false;
			}
			else
			{
				this.CheckForLateness();
				this.Reputation.UpdateRep();
				bool flag2 = false;
				if (this.Clock.HourTime > 13f)
				{
					this.CheckForPoison();
				}
				if (this.Police.PoisonScene || (this.Police.SuicideScene && this.Police.Corpses - this.Police.HiddenCorpses > 0) || this.Police.Corpses - this.Police.HiddenCorpses > 0 || this.Police.BloodParent.childCount > 0 || this.Reputation.Reputation <= -100f)
				{
					this.EndDay();
				}
				else if (this.Clock.HourTime < 15.5f)
				{
					if (!this.Police.Show)
					{
						bool flag3 = false;
						if (this.StudentManager.Teachers[21] != null && this.StudentManager.Teachers[21].DistanceToDestination < 1f)
						{
							flag3 = true;
						}
						if (this.StudentManager.Eighties && this.StudentManager.Students[this.StudentManager.RivalID] != null)
						{
							this.StudentManager.Students[this.StudentManager.RivalID].PlaceBag();
						}
						if (this.Late > 0 && flag3)
						{
							this.Yandere.Subtitle.UpdateLabel(SubtitleType.TeacherLateReaction, this.Late, 5.5f);
							this.Yandere.RPGCamera.enabled = false;
							this.Yandere.ShoulderCamera.Scolding = true;
							this.Yandere.ShoulderCamera.Teacher = this.Teacher;
						}
						else
						{
							this.ClassDarkness.enabled = true;
							this.Transition = true;
							this.FadeOut = true;
						}
						this.Clock.StopTime = true;
					}
					else
					{
						this.EndDay();
					}
				}
				else if (this.Yandere.Inventory.RivalPhone && !this.StudentManager.RivalEliminated)
				{
					this.Yandere.NotificationManager.CustomText = "Put the stolen phone on the owner's desk!";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
					this.Yandere.NotificationManager.CustomText = "You are carrying a stolen phone!";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
					flag2 = true;
				}
				else
				{
					this.EndFinalEvents();
					this.EndDay();
				}
				if (!flag2)
				{
					this.Yandere.Character.GetComponent<Animation>().CrossFade(this.Yandere.IdleAnim);
					this.Yandere.YandereVision = false;
					this.Yandere.CanMove = false;
					if (this.Clock.HourTime < 15.5f)
					{
						this.Yandere.InClass = true;
						if (this.Clock.HourTime < 8.5f)
						{
							this.EndEvents();
						}
						else
						{
							this.StudentManager.CheckBentos();
							if (this.StudentManager.Students[11] != null && this.StudentManager.Students[11].Alive && this.OsanaMondayLunchEvent.Bento[2].Poison == 2)
							{
								Debug.Log("The player poisoned Osana, so we're killing her automatically.");
								this.StudentManager.Students[11].DeathType = DeathType.Poison;
								this.StudentManager.Students[11].BecomeRagdoll();
								this.ClassDarkness.enabled = false;
								this.Yandere.InClass = false;
								this.Transition = false;
								this.FadeOut = false;
								this.EndDay();
							}
							else if (this.StudentManager.Students[10] != null && this.StudentManager.Students[10].Alive && this.OsanaMondayLunchEvent.Bento[3].Poison == 2)
							{
								Debug.Log("The player poisoned Raibaru, so we're killing her automatically.");
								this.StudentManager.Students[10].DeathType = DeathType.Poison;
								this.StudentManager.Students[10].BecomeRagdoll();
								this.ClassDarkness.enabled = false;
								this.Yandere.InClass = false;
								this.Transition = false;
								this.FadeOut = false;
								this.EndDay();
							}
							else
							{
								this.EndLaterEvents();
							}
						}
					}
				}
			}
		}
		if (this.Transition)
		{
			if (this.FadeOut)
			{
				if (this.LoveManager.HoldingHands)
				{
					this.LoveManager.Rival.transform.position = new Vector3(0f, 0f, -50f);
				}
				this.ClassDarkness.color = new Color(this.ClassDarkness.color.r, this.ClassDarkness.color.g, this.ClassDarkness.color.b, this.ClassDarkness.color.a + Time.deltaTime);
				if (this.ClassDarkness.color.a >= 1f)
				{
					if (this.Yandere.Resting)
					{
						this.Yandere.IdleAnim = "f02_idleShort_00";
						this.Yandere.WalkAnim = "f02_newWalk_00";
						this.Yandere.OriginalIdleAnim = this.Yandere.IdleAnim;
						this.Yandere.OriginalWalkAnim = this.Yandere.WalkAnim;
						this.Yandere.CharacterAnimation.CrossFade(this.Yandere.IdleAnim);
						this.Yandere.MyRenderer.materials[2].SetFloat("_BlendAmount1", 0f);
						this.Yandere.Resting = false;
						this.Yandere.Health = 10;
						this.FadeOut = false;
						this.Proceed = true;
					}
					else
					{
						if (this.Yandere.Armed)
						{
							this.Yandere.Unequip();
						}
						this.HeartbeatCamera.SetActive(false);
						this.ClassDarkness.color = new Color(this.ClassDarkness.color.r, this.ClassDarkness.color.g, this.ClassDarkness.color.b, 1f);
						this.FadeOut = false;
						this.Proceed = false;
						this.Yandere.RPGCamera.enabled = false;
						this.PromptBar.ClearButtons();
						this.PromptBar.Label[4].text = "Choose";
						this.PromptBar.Label[5].text = "Allocate";
						this.PromptBar.UpdateButtons();
						this.PromptBar.Show = true;
						this.Class.StudyPoints += ((PlayerGlobals.PantiesEquipped == 7) ? 10 : 5);
						this.Class.StudyPoints += this.Class.BonusPoints;
						this.Class.BonusPoints = 0;
						this.Class.StudyPoints -= this.Late;
						this.Class.UpdateLabel();
						this.Class.gameObject.SetActive(true);
						this.Class.Show = true;
						if (this.Police.Show)
						{
							this.Police.Timer = 1E-06f;
						}
					}
				}
			}
			else if (this.Proceed)
			{
				if (this.ClassDarkness.color.a >= 1f)
				{
					Debug.Log("The PortalScript is now changing the time of day.");
					this.HeartbeatCamera.SetActive(true);
					this.Clock.enabled = true;
					this.Yandere.FixCamera();
					this.Yandere.RPGCamera.enabled = false;
					if (this.Clock.HourTime < 13f)
					{
						if (this.StudentManager.Bullies > 0)
						{
							this.StudentManager.UpdateGraffiti();
						}
						this.Yandere.Incinerator.Timer -= 780f - this.Clock.PresentTime;
						this.DelinquentManager.CheckTime();
						this.Clock.PresentTime = 780f;
					}
					else
					{
						this.Yandere.Incinerator.Timer -= 930f - this.Clock.PresentTime;
						this.DelinquentManager.CheckTime();
						this.Clock.PresentTime = 930f;
					}
					this.Clock.HourTime = this.Clock.PresentTime / 60f;
					this.StudentManager.AttendClass();
				}
				this.ClassDarkness.color = new Color(this.ClassDarkness.color.r, this.ClassDarkness.color.g, this.ClassDarkness.color.b, this.ClassDarkness.color.a - Time.deltaTime);
				if (this.ClassDarkness.color.a <= 0f)
				{
					this.ClassDarkness.enabled = false;
					this.ClassDarkness.color = new Color(this.ClassDarkness.color.r, this.ClassDarkness.color.g, this.ClassDarkness.color.b, 0f);
					this.Clock.StopTime = false;
					this.Transition = false;
					this.Proceed = false;
					this.Yandere.RPGCamera.enabled = true;
					this.Yandere.InClass = false;
					this.Yandere.CanMove = true;
					this.StudentManager.ResumeMovement();
					if (this.Clock.HourTime > 15f)
					{
						this.StudentManager.TakeOutTheTrash();
					}
					if (!MissionModeGlobals.MissionMode)
					{
						if (this.Headmaster.activeInHierarchy)
						{
							this.Headmaster.SetActive(false);
						}
						else
						{
							this.Headmaster.SetActive(true);
						}
					}
				}
			}
		}
		if (this.Clock.HourTime > 15.5f)
		{
			if (base.transform.position.z < 0f)
			{
				this.StudentManager.RemovePapersFromDesks();
				if (!MissionModeGlobals.MissionMode)
				{
					this.MapMarker.material.mainTexture = this.HomeMapMarker;
					base.transform.position = new Vector3(0f, 1f, -75f);
					this.Prompt.Label[0].text = "     Go Home";
					this.Prompt.enabled = true;
					return;
				}
				base.transform.position = new Vector3(0f, -10f, 0f);
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				return;
			}
		}
		else if (!this.Yandere.Police.FadeOut && Vector3.Distance(this.Yandere.transform.position, base.transform.position) < 1.4f)
		{
			this.CanAttendClass = true;
			this.CheckForProblems();
			if (!this.CanAttendClass)
			{
				if (this.Timer == 0f)
				{
					if (this.Yandere.Armed)
					{
						this.Yandere.NotificationManager.CustomText = "Carrying Weapon";
					}
					else if (this.Yandere.Bloodiness > 0f)
					{
						this.Yandere.NotificationManager.CustomText = "Bloody";
					}
					else if (this.Yandere.Sanity < 33.333f)
					{
						this.Yandere.NotificationManager.CustomText = "Visibly Insane";
					}
					else if (this.Yandere.Attacking)
					{
						this.Yandere.NotificationManager.CustomText = "In Combat";
					}
					else if (this.Yandere.Dragging || this.Yandere.Carrying)
					{
						this.Yandere.NotificationManager.CustomText = "Holding Corpse";
					}
					else if (this.Yandere.PickUp != null)
					{
						this.Yandere.NotificationManager.CustomText = "Carrying Item";
					}
					else if (this.Yandere.Chased || this.Yandere.Chasers > 0)
					{
						this.Yandere.NotificationManager.CustomText = "Chased";
					}
					else if (this.StudentManager.Reporter && !this.Police.Show)
					{
						this.Yandere.NotificationManager.CustomText = "Murder being reported";
					}
					else if (this.StudentManager.MurderTakingPlace)
					{
						this.Yandere.NotificationManager.CustomText = "Murder taking place";
					}
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
					this.Yandere.NotificationManager.CustomText = "Cannot attend class. Reason:";
					this.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				}
				this.Prompt.Hide();
				this.Prompt.enabled = false;
				this.Timer += Time.deltaTime;
				if (this.Timer > 5f)
				{
					this.Timer = 0f;
					return;
				}
			}
			else
			{
				this.Prompt.enabled = true;
			}
		}
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x00123E14 File Offset: 0x00122014
	public void CheckForProblems()
	{
		if (this.Yandere.Armed || this.Yandere.Bloodiness > 0f || this.Yandere.Sanity < 33.333f || this.Yandere.Attacking || this.Yandere.Dragging || this.Yandere.Carrying || this.Yandere.PickUp != null || this.Yandere.Chased || this.Yandere.Chasers > 0 || (this.StudentManager.Reporter != null && !this.Police.Show) || this.StudentManager.MurderTakingPlace)
		{
			this.CanAttendClass = false;
		}
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x00123EE4 File Offset: 0x001220E4
	public void EndDay()
	{
		this.StudentManager.StopMoving();
		this.Yandere.StopLaughing();
		this.Yandere.EmptyHands();
		this.Clock.StopTime = true;
		this.Police.Darkness.enabled = true;
		this.Police.FadeOut = true;
		this.Police.DayOver = true;
		if (SchemeGlobals.GetSchemeStage(6) == 8)
		{
			SchemeGlobals.SetSchemeStage(6, 9);
			this.Yandere.PauseScreen.Schemes.UpdateInstructions();
		}
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x00123F70 File Offset: 0x00122170
	private void CheckForLateness()
	{
		this.Late = 0;
		if (this.Clock.HourTime < 13f)
		{
			if (this.Clock.HourTime < 8.52f)
			{
				this.Late = 0;
			}
			else if (this.Clock.HourTime < 10f)
			{
				this.Late = 1;
			}
			else if (this.Clock.HourTime < 11f)
			{
				this.Late = 2;
			}
			else if (this.Clock.HourTime < 12f)
			{
				this.Late = 3;
			}
			else if (this.Clock.HourTime < 13f)
			{
				this.Late = 4;
			}
		}
		else if (this.Clock.HourTime < 13.52f)
		{
			this.Late = 0;
		}
		else if (this.Clock.HourTime < 14f)
		{
			this.Late = 1;
		}
		else if (this.Clock.HourTime < 14.5f)
		{
			this.Late = 2;
		}
		else if (this.Clock.HourTime < 15f)
		{
			this.Late = 3;
		}
		else if (this.Clock.HourTime < 15.5f)
		{
			this.Late = 4;
		}
		this.Reputation.PendingRep -= (float)(5 * this.Late);
		int late = this.Late;
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x001240DC File Offset: 0x001222DC
	public void EndEvents()
	{
		for (int i = 0; i < this.MorningEvents.Length; i++)
		{
			if (this.MorningEvents[i].enabled)
			{
				this.MorningEvents[i].EndEvent();
			}
		}
		for (int i = 0; i < this.FriendEvents.Length; i++)
		{
			if (this.FriendEvents[i].enabled)
			{
				this.FriendEvents[i].EndEvent();
			}
		}
		if (this.OsanaEvent.enabled)
		{
			this.OsanaEvent.EndEvent();
		}
		if (this.OsanaClubEvent.enabled)
		{
			this.OsanaClubEvent.EndEvent();
		}
		if (!this.OsanaClubEvent.enabled && !this.OsanaClubEvent.ReachedTheEnd && !this.StudentManager.Eighties)
		{
			Debug.Log("The Portal is checking the Osana Club Event.");
			this.OsanaClubEvent.CheckForRooftopConvo();
		}
		if (this.OsanaFridayEvent1.enabled)
		{
			this.OsanaFridayEvent1.EndEvent();
		}
		if (this.OsanaFridayEvent2.enabled)
		{
			this.OsanaFridayEvent2.EndEvent();
		}
		for (int i = 1; i < this.MorningGenericEvents.Length; i++)
		{
			if (this.MorningGenericEvents[i].enabled)
			{
				this.MorningGenericEvents[i].NaturalEnd = true;
				this.MorningGenericEvents[i].EndEvent();
			}
		}
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x00124220 File Offset: 0x00122420
	public void EndLaterEvents()
	{
		if (this.OsanaMondayLunchEvent.enabled && this.OsanaMondayLunchEvent.Phase > 0 && this.OsanaMondayLunchEvent.Bento[1].Poison > 0)
		{
			Debug.Log("We're skipping past Osana's Monday lunchtime event, but it was was sabotaged, so we're incrementing the Sabotage count.");
			this.StudentManager.SabotageProgress++;
		}
		if (this.OsanaFridayLunchEvent.enabled && this.OsanaFridayLunchEvent.Rival != null && this.OsanaFridayLunchEvent.Rival.InEvent && this.OsanaFridayLunchEvent.AudioSoftware.AudioDoctored)
		{
			Debug.Log("We're skipping past Osana's Friday lunchtime event, but it was was sabotaged, so we're incrementing the Sabotage count.");
			this.StudentManager.SabotageProgress++;
		}
		if (this.OsanaPoolEvent.Phase > 0)
		{
			this.OsanaPoolEvent.EndEvent();
		}
		if (this.OsanaFridayLunchEvent.enabled)
		{
			this.OsanaFridayLunchEvent.EndEvent();
		}
		for (int i = 1; i < this.LunchGenericEvents.Length; i++)
		{
			if (this.LunchGenericEvents[i].enabled)
			{
				if (this.LunchGenericEvents[i].Sabotaged)
				{
					Debug.Log("We're skipping past a generic rival lunchtime event, but it was was sabotaged, so we're incrementing the Sabotage count.");
					this.StudentManager.SabotageProgress++;
				}
				this.LunchGenericEvents[i].EndEvent();
			}
		}
		Debug.Log("Sabotage Progress is currently: " + this.StudentManager.SabotageProgress.ToString());
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x00124388 File Offset: 0x00122588
	public void EndFinalEvents()
	{
		if (this.OsanaTuesdayAfterClassEvent.enabled && this.OsanaTuesdayAfterClassEvent.Sabotaged)
		{
			Debug.Log("We're skipping past Osana's Tuesday talk-to-Senpai-outside-of-school event, but it was was sabotaged, so we're incrementing the Sabotage count.");
			this.StudentManager.SabotageProgress++;
		}
		if (this.OsanaWednesdayAfterClassEvent.enabled && this.OsanaWednesdayAfterClassEvent.Rival != null && this.OsanaWednesdayAfterClassEvent.Rival.LewdPhotos)
		{
			Debug.Log("We're skipping past Osana's Wednesday talk-to-Senpai-outside-of-school event, but it was was sabotaged, so we're incrementing the Sabotage count.");
			this.StudentManager.SabotageProgress++;
		}
		if (this.OsanaThursdayEvent.enabled && this.OsanaThursdayEvent.Phase > 0 && this.OsanaThursdayEvent.Sabotaged)
		{
			Debug.Log("We're skipping past Osana's Thursday rooftop event, but it was was sabotaged, so we're incrementing the Sabotage count.");
			this.StudentManager.SabotageProgress++;
		}
		for (int i = 1; i < this.FinalGenericEvents.Length; i++)
		{
			if (this.FinalGenericEvents[i].enabled)
			{
				if (this.FinalGenericEvents[i].Sabotaged || (i == 3 && this.StudentManager.Students[this.StudentManager.RivalID].Sleepy))
				{
					Debug.Log("We're skipping past a generic rival after-class event, but it was was sabotaged, so we're incrementing the Sabotage count.");
					this.StudentManager.SabotageProgress++;
				}
				this.FinalGenericEvents[i].EndEvent();
			}
		}
		Debug.Log("It is the end of the day, and Sabotage Progress is currently " + this.StudentManager.SabotageProgress.ToString() + " out of 5.");
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x00124504 File Offset: 0x00122704
	public void CheckForPoison()
	{
		for (int i = 0; i < this.StudentManager.Students.Length; i++)
		{
			if (this.StudentManager.Students[i] != null && this.StudentManager.Students[i].Alive && this.StudentManager.Students[i].MyBento.Lethal)
			{
				this.Police.SkippingPastPoison = true;
				this.StudentManager.Students[i].Ragdoll.Poisoned = true;
				this.StudentManager.Students[i].BecomeRagdoll();
				this.StudentManager.Students[i].DeathType = DeathType.Poison;
			}
		}
	}

	// Token: 0x04002CA7 RID: 11431
	public RivalMorningEventManagerScript[] MorningEvents;

	// Token: 0x04002CA8 RID: 11432
	public OsanaMorningFriendEventScript[] FriendEvents;

	// Token: 0x04002CA9 RID: 11433
	public OsanaMondayBeforeClassEventScript OsanaEvent;

	// Token: 0x04002CAA RID: 11434
	public RivalAfterClassEventManagerScript OsanaWednesdayAfterClassEvent;

	// Token: 0x04002CAB RID: 11435
	public RivalAfterClassEventManagerScript OsanaTuesdayAfterClassEvent;

	// Token: 0x04002CAC RID: 11436
	public OsanaThursdayAfterClassEventScript OsanaThursdayEvent;

	// Token: 0x04002CAD RID: 11437
	public OsanaFridayBeforeClassEvent1Script OsanaFridayEvent1;

	// Token: 0x04002CAE RID: 11438
	public OsanaFridayBeforeClassEvent2Script OsanaFridayEvent2;

	// Token: 0x04002CAF RID: 11439
	public OsanaTuesdayLunchEventScript OsanaTuesdayLunchEvent;

	// Token: 0x04002CB0 RID: 11440
	public OsanaMondayLunchEventScript OsanaMondayLunchEvent;

	// Token: 0x04002CB1 RID: 11441
	public OsanaFridayLunchEventScript OsanaFridayLunchEvent;

	// Token: 0x04002CB2 RID: 11442
	public OsanaClubEventScript OsanaClubEvent;

	// Token: 0x04002CB3 RID: 11443
	public OsanaPoolEventScript OsanaPoolEvent;

	// Token: 0x04002CB4 RID: 11444
	public DelinquentManagerScript DelinquentManager;

	// Token: 0x04002CB5 RID: 11445
	public StudentManagerScript StudentManager;

	// Token: 0x04002CB6 RID: 11446
	public WeaponManagerScript WeaponManager;

	// Token: 0x04002CB7 RID: 11447
	public LoveManagerScript LoveManager;

	// Token: 0x04002CB8 RID: 11448
	public ReputationScript Reputation;

	// Token: 0x04002CB9 RID: 11449
	public PromptBarScript PromptBar;

	// Token: 0x04002CBA RID: 11450
	public YandereScript Yandere;

	// Token: 0x04002CBB RID: 11451
	public PoliceScript Police;

	// Token: 0x04002CBC RID: 11452
	public PromptScript Prompt;

	// Token: 0x04002CBD RID: 11453
	public ClassScript Class;

	// Token: 0x04002CBE RID: 11454
	public ClockScript Clock;

	// Token: 0x04002CBF RID: 11455
	public GameObject EvidenceWarning;

	// Token: 0x04002CC0 RID: 11456
	public GameObject HeartbeatCamera;

	// Token: 0x04002CC1 RID: 11457
	public GameObject Headmaster;

	// Token: 0x04002CC2 RID: 11458
	public UISprite ClassDarkness;

	// Token: 0x04002CC3 RID: 11459
	public Texture HomeMapMarker;

	// Token: 0x04002CC4 RID: 11460
	public Renderer MapMarker;

	// Token: 0x04002CC5 RID: 11461
	public Transform Teacher;

	// Token: 0x04002CC6 RID: 11462
	public bool CanAttendClass;

	// Token: 0x04002CC7 RID: 11463
	public bool BypassWarning;

	// Token: 0x04002CC8 RID: 11464
	public bool LateReport1;

	// Token: 0x04002CC9 RID: 11465
	public bool LateReport2;

	// Token: 0x04002CCA RID: 11466
	public bool Transition;

	// Token: 0x04002CCB RID: 11467
	public bool FadeOut;

	// Token: 0x04002CCC RID: 11468
	public bool Proceed;

	// Token: 0x04002CCD RID: 11469
	public float Timer;

	// Token: 0x04002CCE RID: 11470
	public int Late;

	// Token: 0x04002CCF RID: 11471
	public UILabel BottomLabel;

	// Token: 0x04002CD0 RID: 11472
	public UILabel AttendClassLabel;

	// Token: 0x04002CD1 RID: 11473
	public UILabel CorpsesLabel;

	// Token: 0x04002CD2 RID: 11474
	public UILabel BodyPartsLabel;

	// Token: 0x04002CD3 RID: 11475
	public UILabel BloodStainsLabel;

	// Token: 0x04002CD4 RID: 11476
	public UILabel BloodyClothingLabel;

	// Token: 0x04002CD5 RID: 11477
	public UILabel BloodyWeaponsLabel;

	// Token: 0x04002CD6 RID: 11478
	public GenericRivalEventScript[] MorningGenericEvents;

	// Token: 0x04002CD7 RID: 11479
	public GenericRivalEventScript[] LunchGenericEvents;

	// Token: 0x04002CD8 RID: 11480
	public GenericRivalEventScript[] FinalGenericEvents;
}