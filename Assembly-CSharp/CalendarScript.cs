using System;
using System.IO;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class CalendarScript : MonoBehaviour
{
	public PostProcessingProfile NewTitleScreenProfile;

	public SelectiveGrayscale GrayscaleEffect;

	public ChallengeScript Challenge;

	public Vignetting Vignette;

	public GameObject SkipConfirmationWindow;

	public GameObject CongratulationsWindow;

	public GameObject ResetWeekErrorWindow;

	public GameObject ConfirmationWindow;

	public GameObject ResetWeekWindow;

	public GameObject ClubKickWindow;

	public GameObject AmaiWindow;

	public GameObject DeadlineLabel;

	public GameObject ResetButton;

	public GameObject StatsButton;

	public GameObject AmaiButton;

	public GameObject SkipButton;

	public UILabel AtmosphereLabel;

	public UIPanel ChallengePanel;

	public UIPanel CalendarPanel;

	public UILabel ErrorLabel;

	public UISprite Darkness;

	public UITexture Cloud;

	public UITexture Sun;

	public Transform Highlight;

	public Transform Continue;

	public UILabel[] DayNumber;

	public UILabel[] DayLabel;

	public UILabel MonthLabel;

	public UILabel WeekNumber;

	public UILabel YearLabel;

	public UILabel SkipLabel;

	public bool CameFromTitleScreen;

	public bool ViewingStats;

	public bool Incremented;

	public bool ResetWeek;

	public bool Eighties;

	public bool LoveSick;

	public bool Skipping;

	public bool FadeOut;

	public bool Switch;

	public bool Reset;

	public float Timer;

	public float Target;

	public float Offset = 66.66666f;

	public int Adjustment;

	public int Phase = 1;

	public AudioClip EightiesJingle;

	public UILabel CongratsConfirmLabel;

	public UILabel CongratsLabel;

	public UISprite CongratsBorder;

	public UISprite CongratsBG;

	public UILabel[] Labels;

	public GameObject SundayLabel;

	public GameObject EndingLabel;

	public UISprite[] Borders;

	public UISprite[] BGs;

	public Font VCR;

	public int Corpses;

	private void Start()
	{
		GameGlobals.AlphabetMode = false;
		NewTitleScreenProfile.colorGrading.enabled = false;
		SetVignettePink();
		PlayerGlobals.BringingItem = 0;
		if (GameGlobals.RivalEliminationID == 0 && StudentGlobals.GetStudentDead(10 + DateGlobals.Week))
		{
			Debug.Log("Upon entering the Calendar screen, the rival was dead, but RivalEliminationID was 0. Setting it to 1.");
			GameGlobals.RivalEliminationID = 1;
		}
		if (DateGlobals.Week == 0)
		{
			Debug.Log("Save file had to be deleted because Week was '0''.");
			ResetSaveFile();
		}
		else if (GameGlobals.Eighties)
		{
			if (DateGlobals.Week == 1)
			{
				Debug.Log("Rival is cute.");
			}
			else if (DateGlobals.Week == 2)
			{
				Debug.Log("Rival is pryomaniac.");
			}
			else if (DateGlobals.Week == 3)
			{
				Debug.Log("Rival is bookworm.");
			}
			else if (DateGlobals.Week == 4)
			{
				Debug.Log("Rival is sporty.");
			}
			else if (DateGlobals.Week == 5)
			{
				Debug.Log("Rival is rich.");
			}
			else if (DateGlobals.Week == 6)
			{
				Debug.Log("Rival is idol.");
			}
			else if (DateGlobals.Week == 7)
			{
				Debug.Log("Rival is prodigy.");
			}
			else if (DateGlobals.Week == 8)
			{
				Debug.Log("Rival is traditional.");
			}
			else if (DateGlobals.Week == 9)
			{
				Debug.Log("Rival is gravure.");
			}
			else if (DateGlobals.Week == 10)
			{
				Debug.Log("Rival is investigator.");
			}
			for (int i = 1; i < 11; i++)
			{
				PlayerGlobals.SetShrineCollectible(i, value: true);
			}
		}
		else if (DateGlobals.Week > 2)
		{
			Debug.Log("Save file had to be deleted because 80s and 202X got mixed up.");
			ResetSaveFile();
		}
		if (!ConversationGlobals.GetTopicDiscovered(1))
		{
			for (int j = 1; j < 26; j++)
			{
				ConversationGlobals.SetTopicDiscovered(j, value: true);
			}
		}
		if (!SchoolGlobals.SchoolAtmosphereSet)
		{
			SchoolGlobals.SchoolAtmosphereSet = true;
			SchoolGlobals.SchoolAtmosphere = 1f;
			PlayerGlobals.Money = 10f;
			PlayerGlobals.SetCannotBringItem(4, value: true);
			PlayerGlobals.SetCannotBringItem(5, value: true);
			PlayerGlobals.SetCannotBringItem(6, value: true);
			PlayerGlobals.SetCannotBringItem(7, value: true);
		}
		LoveSickCheck();
		if (DateGlobals.PassDays > 0 && !SchoolGlobals.HighSecurity && SchoolGlobals.SchoolAtmosphere >= SchoolGlobals.PreviousSchoolAtmosphere)
		{
			SchoolGlobals.SchoolAtmosphere += 0.05f;
			GetNumberOfCorpses();
			ReducePrisonerHealth();
		}
		ImproveSchoolAtmosphere();
		DateGlobals.DayPassed = true;
		Continue.transform.localPosition = new Vector3(Continue.transform.localPosition.x, -610f, Continue.transform.localPosition.z);
		ChallengePanel.alpha = 0f;
		CalendarPanel.alpha = 1f;
		Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 1f);
		Time.timeScale = 1f;
		if (GameGlobals.RivalEliminationID > 0)
		{
			DeadlineLabel.SetActive(value: false);
		}
		WeekNumber.text = "WEEK " + DateGlobals.Week;
		LoveSickCheck();
		ChangeDayColor();
		if (GameGlobals.Eighties)
		{
			BecomeEighties();
		}
		else
		{
			AmaiButton.SetActive(value: false);
			StatsButton.SetActive(value: false);
			SkipButton.transform.localPosition = new Vector3(-120f, -500f, 0f);
			if (DateGlobals.Week == 1)
			{
				DayNumber[1].text = "6";
				DayNumber[2].text = "7";
				DayNumber[3].text = "8";
				DayNumber[4].text = "9";
				DayNumber[5].text = "10";
				DayNumber[6].text = "11";
				DayNumber[7].text = "12";
				Adjustment = -50;
			}
			else if (DateGlobals.Week == 2)
			{
				DayNumber[1].text = "13";
				DayNumber[2].text = "14";
				DayNumber[3].text = "15";
				DayNumber[4].text = "16";
				DayNumber[5].text = "17";
				DayNumber[6].text = "18";
				DayNumber[7].text = "19";
				Adjustment = -50;
				AmaiButton.SetActive(value: true);
			}
		}
		Highlight.localPosition = new Vector3(-750f + Offset + 250f * (float)DateGlobals.Weekday + (float)Adjustment, Highlight.localPosition.y, Highlight.localPosition.z);
		if (DateGlobals.Weekday == DayOfWeek.Saturday)
		{
			Highlight.localPosition = new Vector3(-1150f, Highlight.localPosition.y, Highlight.localPosition.z);
		}
		if (GameGlobals.CameFromTitleScreen)
		{
			GameGlobals.CameFromTitleScreen = false;
			CameFromTitleScreen = true;
		}
		SkipConfirmationWindow.SetActive(value: false);
		if (DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			YanSave.SaveData("Profile_" + GameGlobals.Profile + "_Slot_" + 11);
		}
	}

	private void Update()
	{
		Timer += Time.deltaTime;
		if (!FadeOut)
		{
			Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, Darkness.color.a - Time.deltaTime);
			if (Darkness.color.a < 0f)
			{
				Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, 0f);
			}
			if (Timer > 1f)
			{
				if (!Incremented)
				{
					if (DateGlobals.PassDays > 0)
					{
						if (Eighties)
						{
							GetComponent<AudioSource>().clip = EightiesJingle;
							GetComponent<AudioSource>().volume = 0.25f;
						}
						GetComponent<AudioSource>().pitch = 1f - (0.8f - SchoolGlobals.SchoolAtmosphere * 0.8f);
						GetComponent<AudioSource>().Play();
					}
					if (DateGlobals.Weekday != DayOfWeek.Friday || Skipping)
					{
						bool flag = false;
						if ((DateGlobals.Week == 1 && DateGlobals.Weekday == DayOfWeek.Sunday) || DateGlobals.ForceSkip)
						{
							if (DateGlobals.ForceSkip)
							{
								Debug.Log("DateGlobals.ForceSkip was true.");
							}
							DateGlobals.ForceSkip = false;
							SundayLabel.SetActive(value: false);
							flag = true;
						}
						while (flag || (DateGlobals.PassDays > 0 && DateGlobals.Weekday != DayOfWeek.Saturday && DateGlobals.Weekday != 0))
						{
							DateGlobals.GameplayDay++;
							DateGlobals.PassDays--;
							DateGlobals.Weekday++;
							ChangeDayColor();
							flag = false;
						}
						Skipping = false;
					}
					else if (!CameFromTitleScreen)
					{
						DateGlobals.GameplayDay++;
						DateGlobals.PassDays--;
						DateGlobals.Weekday++;
						ChangeDayColor();
						Debug.Log("It is Friday, and CameFromTitleScreen is apparently false. Passed one day. " + DateGlobals.PassDays + " days remaining.");
					}
					Target = 250f * (float)DateGlobals.Weekday + (float)Adjustment;
					if (DateGlobals.Weekday > DayOfWeek.Saturday)
					{
						Darkness.color = new Color(0f, 0f, 0f, 0f);
						DateGlobals.Weekday = DayOfWeek.Sunday;
						Target = Adjustment;
					}
					if (DateGlobals.Weekday != 0 && DateGlobals.Weekday < DayOfWeek.Saturday && GameGlobals.RivalEliminationID > 0 && !GameGlobals.InformedAboutSkipping && DateGlobals.Week < 2)
					{
						GameGlobals.InformedAboutSkipping = true;
						CongratulationsWindow.SetActive(value: true);
					}
					Incremented = true;
					ChangeDayColor();
					if (DateGlobals.Weekday == DayOfWeek.Saturday)
					{
						ResetButton.SetActive(value: false);
					}
				}
				Continue.localPosition = new Vector3(Continue.localPosition.x, Mathf.Lerp(Continue.localPosition.y, -500f, Time.deltaTime * 10f), Continue.localPosition.z);
				if (!Switch)
				{
					if (ViewingStats)
					{
						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							Switch = true;
						}
					}
					else if (CongratulationsWindow.activeInHierarchy)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							CongratulationsWindow.SetActive(value: false);
						}
					}
					else if (ResetWeekWindow.activeInHierarchy)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							int num = GameGlobals.Profile;
							int num2 = 11;
							Debug.Log("We've been instructed to reset the week. We're currently on Profile #" + num);
							if (Eighties && num < 11)
							{
								Debug.Log("...but we're in the 80s! Let's adjust that!");
								num += 10;
							}
							if (File.Exists(Application.streamingAssetsPath + "/SaveFiles/Profile_" + num + "_Slot_" + num2 + ".yansave"))
							{
								FadeOut = true;
								ResetWeek = true;
							}
							else
							{
								ErrorLabel.text = "[c][ff0000]COULD NOT LOAD RESET WEEK SAVE DATA.[-][/c]\n\nThe game searched for this file: \n\n" + Application.streamingAssetsPath + "/SaveFiles/Profile_" + num + "_Slot_" + num2 + ".yansave\n\nHowever, the file was not found.\n\nThis may be happening because you recently updated to a new build of Yandere Sim.\n\nTo retrieve your ''Reset Week'' save data, you'll have to look in the StreamingAssets directory of the previous version of Yandere Sim that you played.\n\nYou must find the file named above, and put that file into the StreamingAssets/SaveFiles directory of the version of Yandere Sim that you are currently playing.";
								ResetWeekErrorWindow.SetActive(value: true);
								ResetWeekWindow.SetActive(value: false);
								Debug.Log("An error message must be displayed.");
							}
						}
						else if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							ResetWeekWindow.SetActive(value: false);
						}
						else if (Input.GetButtonDown(InputNames.Xbox_X))
						{
							ConfirmationWindow.SetActive(value: true);
							ResetWeekWindow.SetActive(value: false);
						}
					}
					else if (ConfirmationWindow.activeInHierarchy)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							FadeOut = true;
							Reset = true;
						}
						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							ConfirmationWindow.SetActive(value: false);
						}
					}
					else if (SkipConfirmationWindow.activeInHierarchy)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							SundayLabel.SetActive(value: false);
							if (StudentGlobals.MemorialStudents > 0)
							{
								StudentGlobals.MemorialStudents = 0;
								StudentGlobals.MemorialStudent1 = 0;
								StudentGlobals.MemorialStudent2 = 0;
								StudentGlobals.MemorialStudent3 = 0;
								StudentGlobals.MemorialStudent4 = 0;
								StudentGlobals.MemorialStudent5 = 0;
								StudentGlobals.MemorialStudent6 = 0;
								StudentGlobals.MemorialStudent7 = 0;
								StudentGlobals.MemorialStudent8 = 0;
								StudentGlobals.MemorialStudent9 = 0;
							}
							SkipConfirmationWindow.SetActive(value: false);
							if (DateGlobals.Weekday != 0 && DateGlobals.Weekday != DayOfWeek.Saturday)
							{
								Debug.Log("Skipping day. Not Saturday or Sunday. Awarding 10 bonus study points.");
								ClassGlobals.BonusStudyPoints += 10;
							}
							GameGlobals.SenpaiMourning = false;
							GameGlobals.ShowAbduction = false;
							DateGlobals.Weekday++;
							Incremented = false;
							Skipping = true;
							if (!SchoolGlobals.HighSecurity && SchoolGlobals.SchoolAtmosphere >= SchoolGlobals.PreviousSchoolAtmosphere)
							{
								SchoolGlobals.SchoolAtmosphere += 0.05f;
							}
							ReducePrisonerHealth();
							ImproveSchoolAtmosphere();
							if ((GameGlobals.RivalEliminationID == 0 && DateGlobals.Weekday == DayOfWeek.Friday) || DateGlobals.Weekday > DayOfWeek.Friday)
							{
								SkipButton.SetActive(value: false);
								if (Eighties && DateGlobals.Weekday == DayOfWeek.Sunday)
								{
									SkipButton.SetActive(value: true);
								}
							}
						}
						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							SkipConfirmationWindow.SetActive(value: false);
						}
					}
					else if (AmaiWindow.activeInHierarchy)
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							AmaiButton.SetActive(value: false);
							AmaiWindow.SetActive(value: false);
							DateGlobals.Weekday++;
							Incremented = false;
							if (!SchoolGlobals.HighSecurity)
							{
								SchoolGlobals.SchoolAtmosphere += 0.05f;
							}
							ReducePrisonerHealth();
							ImproveSchoolAtmosphere();
							AmaiWindow.SetActive(value: false);
						}
						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							AmaiWindow.SetActive(value: false);
						}
					}
					else if (ResetWeekErrorWindow.activeInHierarchy)
					{
						if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							ResetWeekErrorWindow.SetActive(value: false);
						}
					}
					else
					{
						if (Input.GetButtonDown(InputNames.Xbox_A))
						{
							FadeOut = true;
						}
						else if (Input.GetButtonDown(InputNames.Xbox_B))
						{
							if (ResetButton.activeInHierarchy)
							{
								ResetWeekWindow.SetActive(value: true);
							}
						}
						else if (Input.GetButtonDown(InputNames.Xbox_X) && Eighties)
						{
							Switch = true;
						}
						if (SkipButton.activeInHierarchy)
						{
							if (Input.GetButtonDown(InputNames.Xbox_Y))
							{
								SkipConfirmationWindow.SetActive(value: true);
								if (DateGlobals.Weekday > DayOfWeek.Sunday && ClubGlobals.Club != 0 && ClubGlobals.ActivitiesAttended == 0)
								{
									ClubKickWindow.SetActive(value: true);
								}
							}
						}
						else if (AmaiButton.activeInHierarchy && Input.GetButtonDown(InputNames.Xbox_Y))
						{
							AmaiWindow.SetActive(value: true);
						}
					}
				}
			}
		}
		else
		{
			Continue.localPosition = new Vector3(Continue.localPosition.x, Mathf.Lerp(Continue.localPosition.y, -610f, Time.deltaTime * 10f), Continue.localPosition.z);
			Darkness.color = new Color(Darkness.color.r, Darkness.color.g, Darkness.color.b, Darkness.color.a + Time.deltaTime);
			if (Darkness.color.a >= 1f)
			{
				if (ResetWeek)
				{
					int num3 = GameGlobals.Profile;
					int num4 = 11;
					int femaleUniform = StudentGlobals.FemaleUniform;
					int maleUniform = StudentGlobals.MaleUniform;
					Debug.Log("We've been instructed to reset the week.");
					Debug.Log("We're currently on Profile #" + num3);
					if (Eighties && num3 < 11)
					{
						Debug.Log("...but we're in the 80s! Let's adjust that!");
						num3 += 10;
					}
					if (File.Exists(Application.streamingAssetsPath + "/SaveFiles/Profile_" + num3 + "_Slot_" + num4 + ".yansave"))
					{
						YanSave.LoadData("Profile_" + num3 + "_Slot_" + num4);
						YanSave.LoadPrefs("Profile_" + num3 + "_Slot_" + num4);
						Debug.Log("Successfully loaded the save in Slot #" + num4);
					}
					else
					{
						Debug.Log("Attempted to load a save from Slot #" + num4 + ", but apparently it didn't exist.");
					}
					StudentGlobals.FemaleUniform = femaleUniform;
					StudentGlobals.MaleUniform = maleUniform;
					GameGlobals.CorkboardScene = true;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
				else if (Reset)
				{
					ResetSaveFile();
				}
				else
				{
					if (DateGlobals.Weekday == DayOfWeek.Saturday && ClubGlobals.Club != 0 && ClubGlobals.ActivitiesAttended == 0)
					{
						Debug.Log("Kicking player out of club.");
						ClubGlobals.SetClubKicked(ClubGlobals.Club, value: true);
						ClubGlobals.Club = ClubType.None;
					}
					if (HomeGlobals.Night)
					{
						HomeGlobals.Night = false;
					}
					if (DateGlobals.Weekday == DayOfWeek.Saturday)
					{
						if (!Eighties)
						{
							SceneManager.LoadScene("BusStopScene");
						}
						else
						{
							GameGlobals.EightiesCutsceneID = DateGlobals.Week + 1;
							SceneManager.LoadScene("EightiesCutsceneScene");
						}
					}
					else
					{
						if (DateGlobals.Weekday == DayOfWeek.Sunday && !Eighties)
						{
							HomeGlobals.Night = true;
						}
						SceneManager.LoadScene("HomeScene");
					}
				}
			}
		}
		if (Incremented)
		{
			Highlight.localPosition = new Vector3(Mathf.Lerp(Highlight.localPosition.x, -750f + Offset + Target, Time.deltaTime * 10f), Highlight.localPosition.y, Highlight.localPosition.z);
		}
		if (!Switch)
		{
			return;
		}
		if (!ViewingStats)
		{
			if (Phase == 1)
			{
				CalendarPanel.alpha = Mathf.MoveTowards(CalendarPanel.alpha, 0f, Time.deltaTime * 5f);
				if (CalendarPanel.alpha <= 0f)
				{
					Phase++;
				}
				return;
			}
			ChallengePanel.alpha = Mathf.MoveTowards(ChallengePanel.alpha, 1f, Time.deltaTime * 5f);
			if (ChallengePanel.alpha >= 1f)
			{
				ViewingStats = true;
				Switch = false;
				Phase = 1;
			}
		}
		else if (Phase == 1)
		{
			ChallengePanel.alpha = Mathf.MoveTowards(ChallengePanel.alpha, 0f, Time.deltaTime * 5f);
			if (ChallengePanel.alpha <= 0f)
			{
				Phase++;
			}
		}
		else
		{
			CalendarPanel.alpha = Mathf.MoveTowards(CalendarPanel.alpha, 1f, Time.deltaTime * 5f);
			if (CalendarPanel.alpha >= 1f)
			{
				ViewingStats = false;
				Switch = false;
				Phase = 1;
			}
		}
	}

	public void ChangeDayColor()
	{
		UILabel[] dayLabel = DayLabel;
		for (int i = 0; i < dayLabel.Length; i++)
		{
			_ = dayLabel[i];
		}
		dayLabel = DayNumber;
		for (int i = 0; i < dayLabel.Length; i++)
		{
			_ = dayLabel[i] != null;
		}
		if ((GameGlobals.RivalEliminationID == 0 && DateGlobals.Weekday < DayOfWeek.Friday) || (GameGlobals.RivalEliminationID > 0 && DateGlobals.Weekday < DayOfWeek.Saturday))
		{
			SkipButton.SetActive(value: true);
		}
		else
		{
			SkipButton.SetActive(value: false);
			if (Eighties && DateGlobals.Weekday == DayOfWeek.Sunday)
			{
				SkipButton.SetActive(value: true);
			}
		}
		if (!Eighties && DateGlobals.Week == 2)
		{
			SkipButton.SetActive(value: false);
		}
	}

	public void LoveSickCheck()
	{
		if (!GameGlobals.LoveSick)
		{
			return;
		}
		SchoolGlobals.SchoolAtmosphereSet = true;
		SchoolGlobals.SchoolAtmosphere = 0f;
		LoveSick = true;
		Camera.main.backgroundColor = new Color(0f, 0f, 0f, 1f);
		GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in array)
		{
			UISprite component = obj.GetComponent<UISprite>();
			if (component != null)
			{
				component.color = new Color(1f, 0f, 0f, component.color.a);
			}
			UITexture component2 = obj.GetComponent<UITexture>();
			if (component2 != null)
			{
				component2.color = new Color(1f, 0f, 0f, component2.color.a);
			}
			UILabel component3 = obj.GetComponent<UILabel>();
			if (component3 != null)
			{
				if (component3.color != Color.black)
				{
					component3.color = new Color(1f, 0f, 0f, component3.color.a);
				}
				if (component3.text == "?")
				{
					component3.color = new Color(1f, 0f, 0f, component3.color.a);
				}
			}
		}
		Darkness.color = Color.black;
		AtmosphereLabel.enabled = false;
		Cloud.enabled = false;
		Sun.enabled = false;
	}

	public void SetVignettePink()
	{
		VignetteModel.Settings settings = NewTitleScreenProfile.vignette.settings;
		settings.color = new Color(1f, 0.75f, 1f, 1f);
		NewTitleScreenProfile.vignette.settings = settings;
	}

	private void ImproveSchoolAtmosphere()
	{
		if (SchoolGlobals.SchoolAtmosphere > 1f)
		{
			SchoolGlobals.SchoolAtmosphere = 1f;
		}
		Sun.color = new Color(Sun.color.r, Sun.color.g, Sun.color.b, SchoolGlobals.SchoolAtmosphere);
		Cloud.color = new Color(Cloud.color.r, Cloud.color.g, Cloud.color.b, 1f - SchoolGlobals.SchoolAtmosphere);
		AtmosphereLabel.text = (SchoolGlobals.SchoolAtmosphere * 100f).ToString("f0") + "%";
		float num = 1f - SchoolGlobals.SchoolAtmosphere;
		GrayscaleEffect.desaturation = num;
		Vignette.intensity = num * 5f;
		Vignette.blur = num;
		Vignette.chromaticAberration = num;
		SchoolGlobals.PreviousSchoolAtmosphere = SchoolGlobals.SchoolAtmosphere;
	}

	private void BecomeEighties()
	{
		Vignette.enabled = false;
		StudentGlobals.FemaleUniform = 6;
		StudentGlobals.MaleUniform = 1;
		if (DateGlobals.Week > 1 && DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			SundayLabel.SetActive(value: true);
			SkipButton.SetActive(value: true);
		}
		if (DateGlobals.ForceSkip)
		{
			SundayLabel.SetActive(value: false);
		}
		Eighties = true;
		EightiesifyAllLabels();
		for (int i = 1; i < DayNumber.Length; i++)
		{
			DayNumber[i].fontSize = 150;
			DayNumber[i].effectDistance = new Vector2(10f, 10f);
		}
		for (int i = 0; i < DayLabel.Length; i++)
		{
			DayLabel[i].pivot = UIWidget.Pivot.Center;
			DayLabel[i].transform.localPosition = new Vector3(0f, 120f, 0f);
			DayLabel[i].transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
		}
		Camera.main.backgroundColor = new Color(0f, 0f, 0.75f, 1f);
		AtmosphereLabel.color = new Color(0f, 0f, 0f, 1f);
		YearLabel.text = "1989";
		Offset = 0f;
		Highlight.localScale = new Vector3(1f, 1.25f, 1f);
		DeadlineLabel.transform.localPosition = new Vector3(500f, -200f, 0f);
		Continue.localPosition = new Vector3(650f, Continue.localPosition.y, Continue.localPosition.z);
		if (DateGlobals.Week == 1)
		{
			DayNumber[1].text = "2";
			DayNumber[2].text = "3";
			DayNumber[3].text = "4";
			DayNumber[4].text = "5";
			DayNumber[5].text = "6";
			DayNumber[6].text = "7";
			DayNumber[7].text = "8";
		}
		else if (DateGlobals.Week == 2)
		{
			DayNumber[1].text = "9";
			DayNumber[2].text = "10";
			DayNumber[3].text = "11";
			DayNumber[4].text = "12";
			DayNumber[5].text = "13";
			DayNumber[6].text = "14";
			DayNumber[7].text = "15";
		}
		else if (DateGlobals.Week == 3)
		{
			DayNumber[1].text = "16";
			DayNumber[2].text = "17";
			DayNumber[3].text = "18";
			DayNumber[4].text = "19";
			DayNumber[5].text = "20";
			DayNumber[6].text = "21";
			DayNumber[7].text = "22";
		}
		else if (DateGlobals.Week == 4)
		{
			DayNumber[1].text = "23";
			DayNumber[2].text = "24";
			DayNumber[3].text = "25";
			DayNumber[4].text = "26";
			DayNumber[5].text = "27";
			DayNumber[6].text = "28";
			DayNumber[7].text = "29";
		}
		else if (DateGlobals.Week == 5)
		{
			DayNumber[1].text = "30";
			DayNumber[2].text = "1";
			DayNumber[3].text = "2";
			DayNumber[4].text = "3";
			DayNumber[5].text = "4";
			DayNumber[6].text = "5";
			DayNumber[7].text = "6";
		}
		else if (DateGlobals.Week == 6)
		{
			DayNumber[1].text = "7";
			DayNumber[2].text = "8";
			DayNumber[3].text = "9";
			DayNumber[4].text = "10";
			DayNumber[5].text = "11";
			DayNumber[6].text = "12";
			DayNumber[7].text = "13";
		}
		else if (DateGlobals.Week == 7)
		{
			DayNumber[1].text = "14";
			DayNumber[2].text = "15";
			DayNumber[3].text = "16";
			DayNumber[4].text = "17";
			DayNumber[5].text = "18";
			DayNumber[6].text = "19";
			DayNumber[7].text = "20";
		}
		else if (DateGlobals.Week == 8)
		{
			DayNumber[1].text = "21";
			DayNumber[2].text = "22";
			DayNumber[3].text = "23";
			DayNumber[4].text = "24";
			DayNumber[5].text = "25";
			DayNumber[6].text = "26";
			DayNumber[7].text = "27";
		}
		else if (DateGlobals.Week == 9)
		{
			DayNumber[1].text = "28";
			DayNumber[2].text = "29";
			DayNumber[3].text = "30";
			DayNumber[4].text = "31";
			DayNumber[5].text = "1";
			DayNumber[6].text = "2";
			DayNumber[7].text = "3";
		}
		else if (DateGlobals.Week == 10)
		{
			DayNumber[1].text = "4";
			DayNumber[2].text = "5";
			DayNumber[3].text = "6";
			DayNumber[4].text = "7";
			DayNumber[5].text = "8";
			DayNumber[6].text = "9";
			DayNumber[7].text = "10";
		}
		else if (DateGlobals.Week == 11)
		{
			GameGlobals.RivalEliminationID = 1;
			EndingLabel.SetActive(value: true);
			DayNumber[1].text = "11";
			DayNumber[2].text = "12";
			DayNumber[3].text = "13";
			DayNumber[4].text = "14";
			DayNumber[5].text = "15";
			DayNumber[6].text = "16";
			DayNumber[7].text = "17";
		}
		if ((DateGlobals.Week == 9 && DateGlobals.Weekday > DayOfWeek.Wednesday) || DateGlobals.Week > 9)
		{
			MonthLabel.text = "JUNE";
		}
		else if ((DateGlobals.Week == 5 && DateGlobals.Weekday > DayOfWeek.Sunday) || DateGlobals.Week > 5)
		{
			MonthLabel.text = "MAY";
		}
		if (SchoolGlobals.SchoolAtmosphere <= 0.5f)
		{
			AtmosphereLabel.color = new Color(1f, 1f, 1f);
		}
	}

	public void EightiesifyAllLabels()
	{
		ResetWeekWindow.SetActive(value: true);
		ConfirmationWindow.SetActive(value: true);
		SkipConfirmationWindow.SetActive(value: true);
		CongratulationsWindow.SetActive(value: true);
		ClubKickWindow.SetActive(value: true);
		AmaiWindow.SetActive(value: true);
		Labels = UnityEngine.Object.FindSceneObjectsOfType(typeof(UILabel)) as UILabel[];
		for (int i = 0; i < Labels.Length; i++)
		{
			if (Labels[i].gameObject.layer != 25)
			{
				EightiesifyLabel(Labels[i]);
			}
		}
		EightiesifyLabel(SkipLabel);
		UISprite[] borders = Borders;
		for (int j = 0; j < borders.Length; j++)
		{
			borders[j].color = new Color(1f, 1f, 1f, 1f);
		}
		borders = BGs;
		for (int j = 0; j < borders.Length; j++)
		{
			borders[j].color = new Color(0f, 0f, 0.75f, 1f);
		}
		CongratsBorder.color = new Color(1f, 1f, 1f, 1f);
		CongratsBG.color = new Color(0f, 0f, 0.75f, 1f);
		EightiesifyLabel(CongratsConfirmLabel);
		EightiesifyLabel(CongratsLabel);
		ResetWeekWindow.SetActive(value: false);
		ConfirmationWindow.SetActive(value: false);
		SkipConfirmationWindow.SetActive(value: false);
		CongratulationsWindow.SetActive(value: false);
		ClubKickWindow.SetActive(value: false);
		AmaiWindow.SetActive(value: false);
	}

	public void EightiesifyLabel(UILabel Label)
	{
		Label.trueTypeFont = VCR;
		Label.applyGradient = false;
		Label.color = new Color(1f, 1f, 1f, 1f);
		Label.effectStyle = UILabel.Effect.Outline8;
		Label.effectColor = new Color(0f, 0f, 0f, 1f);
		if (Label.text.Contains("Warning") && Label.transform.localPosition.y < 0f)
		{
			Label.color = new Color(1f, 0f, 0f, 1f);
		}
	}

	public void ResetSaveFile()
	{
		int num = GameGlobals.Profile;
		bool eighties = GameGlobals.Eighties;
		bool debug = GameGlobals.Debug;
		int femaleUniform = StudentGlobals.FemaleUniform;
		int maleUniform = StudentGlobals.MaleUniform;
		Globals.DeleteAll();
		if (eighties && num < 11)
		{
			num += 10;
		}
		PlayerPrefs.SetInt("ProfileCreated_" + num, 1);
		GameGlobals.Eighties = eighties;
		GameGlobals.Profile = num;
		GameGlobals.Debug = debug;
		StudentGlobals.FemaleUniform = femaleUniform;
		StudentGlobals.MaleUniform = maleUniform;
		GameGlobals.LoveSick = LoveSick;
		DateGlobals.PassDays = 1;
		if (GameGlobals.Eighties)
		{
			for (int i = 1; i < 101; i++)
			{
				StudentGlobals.SetStudentPhotographed(i, value: true);
			}
		}
		for (int j = 1; j < 26; j++)
		{
			ConversationGlobals.SetTopicLearnedByStudent(j, 1, value: true);
		}
		GameGlobals.CorkboardScene = true;
		YanSave.SaveData("Profile_" + num + "_Slot_" + 11);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GetNumberOfCorpses()
	{
		if (StudentGlobals.Prisoner1 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner1) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner2 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner2) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner3 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner3) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner4 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner4) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner5 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner5) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner6 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner6) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner7 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner7) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner8 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner8) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner9 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner9) == 0)
		{
			Corpses++;
		}
		if (StudentGlobals.Prisoner10 != 0 && StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner10) == 0)
		{
			Corpses++;
		}
	}

	public void ReducePrisonerHealth()
	{
		int num = 10 + Corpses * 5;
		if (StudentGlobals.Prisoner1 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner1, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner1) - num);
		}
		if (StudentGlobals.Prisoner2 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner2, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner2) - num);
		}
		if (StudentGlobals.Prisoner3 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner3, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner3) - num);
		}
		if (StudentGlobals.Prisoner4 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner4, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner4) - num);
		}
		if (StudentGlobals.Prisoner5 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner5, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner5) - num);
		}
		if (StudentGlobals.Prisoner6 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner6, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner6) - num);
		}
		if (StudentGlobals.Prisoner7 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner7, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner7) - num);
		}
		if (StudentGlobals.Prisoner8 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner8, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner8) - num);
		}
		if (StudentGlobals.Prisoner9 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner9, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner9) - num);
		}
		if (StudentGlobals.Prisoner10 != 0)
		{
			StudentGlobals.SetStudentHealth(StudentGlobals.Prisoner10, StudentGlobals.GetStudentHealth(StudentGlobals.Prisoner10) - num);
		}
	}

	public void LearnIfKicked()
	{
		Debug.Log("Today is " + DateGlobals.Weekday.ToString() + ". Checking to see if we were kicked out of the Cooking Club.");
		if (ClubGlobals.GetClubKicked(ClubType.Cooking))
		{
			Debug.Log("As of right now - " + DateGlobals.Weekday.ToString() + " - we believe that we were kicked out of the Cooking Club.");
		}
		else
		{
			Debug.Log("Nope, not kicked out of the Cooking Club...");
		}
	}
}
