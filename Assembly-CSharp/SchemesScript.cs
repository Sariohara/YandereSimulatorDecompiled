using System;
using UnityEngine;

public class SchemesScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;

	public SchemeManagerScript SchemeManager;

	public InputManagerScript InputManager;

	public InventoryScript Inventory;

	public PromptBarScript PromptBar;

	public GameObject NextStepInput;

	public GameObject FavorMenu;

	public Transform Highlight;

	public Transform Arrow;

	public UILabel SchemeInstructions;

	public UITexture SchemeIcon;

	public UILabel PantyCount;

	public UILabel SchemeDesc;

	public UILabel[] SchemeDeadlineLabels;

	public UILabel[] SchemeCostLabels;

	public UILabel[] SchemeNameLabels;

	public UISprite[] Exclamations;

	public Texture[] SchemeIcons;

	public int[] SchemeCosts;

	public Transform[] SchemeDestinations;

	public string[] SchemeDeadlines;

	public string[] SchemeSkills;

	public string[] SchemeDescs;

	public string[] SchemeNames;

	[Multiline]
	[SerializeField]
	public string[] SchemeSteps;

	public int ListPosition = 1;

	public int Limit = 20;

	public int ID = 1;

	public string[] Steps;

	public AudioClip InfoPurchase;

	public AudioClip InfoAfford;

	public Transform[] Scheme1Destinations;

	public Transform[] Scheme2Destinations;

	public Transform[] Scheme3Destinations;

	public Transform[] Scheme4Destinations;

	public Transform[] Scheme5Destinations;

	public bool[] DisableScheme;

	public float HeldDown;

	public float HeldUp;

	public GameObject HUDIcon;

	public UILabel HUDInstructions;

	private void Start()
	{
		for (int i = 1; i < SchemeNameLabels.Length; i++)
		{
			if (!SchemeGlobals.GetSchemeStatus(i))
			{
				SchemeDeadlineLabels[i].text = SchemeDeadlines[i];
				SchemeNameLabels[i].text = SchemeNames[i];
			}
		}
		DisableScheme[1] = true;
		DisableScheme[2] = true;
		DisableScheme[3] = true;
		DisableScheme[4] = true;
		DisableScheme[5] = true;
		DisableScheme[21] = true;
		DisableScheme[22] = true;
		DisableScheme[23] = true;
		DisableScheme[24] = true;
		DisableScheme[25] = true;
		if (DateGlobals.Weekday == DayOfWeek.Monday)
		{
			DisableScheme[1] = false;
			DisableScheme[21] = false;
		}
		if (DateGlobals.Weekday == DayOfWeek.Tuesday)
		{
			DisableScheme[2] = false;
			DisableScheme[22] = false;
			DisableScheme[27] = true;
		}
		if (DateGlobals.Weekday == DayOfWeek.Wednesday)
		{
			DisableScheme[3] = false;
			DisableScheme[23] = false;
		}
		if (DateGlobals.Weekday == DayOfWeek.Thursday)
		{
			DisableScheme[4] = false;
			DisableScheme[24] = false;
		}
		if (DateGlobals.Weekday == DayOfWeek.Friday)
		{
			DisableScheme[5] = false;
			DisableScheme[25] = false;
		}
		if (DateGlobals.Weekday != DayOfWeek.Monday)
		{
			DisableScheme[6] = true;
		}
		if (DateGlobals.Weekday != DayOfWeek.Thursday)
		{
			DisableScheme[20] = true;
		}
		if (NextStepInput != null)
		{
			NextStepInput.SetActive(value: false);
		}
		UpdateSchemeInfo();
		if (StudentManager.MissionMode)
		{
			SchemeInstructions.color = Color.white;
			SchemeDesc.color = Color.white;
		}
	}

	private void Update()
	{
		if (InputManager.DPadUp || InputManager.StickUp || Input.GetKey("w") || Input.GetKey("up"))
		{
			HeldUp += Time.unscaledDeltaTime;
		}
		else
		{
			HeldUp = 0f;
		}
		if (InputManager.DPadDown || InputManager.StickDown || Input.GetKey("s") || Input.GetKey("down"))
		{
			HeldDown += Time.unscaledDeltaTime;
		}
		else
		{
			HeldDown = 0f;
		}
		if (InputManager.TappedUp || HeldUp > 0.5f)
		{
			if (HeldUp > 0.5f)
			{
				HeldUp = 0.45f;
			}
			if (ID == 1)
			{
				ListPosition--;
				if (ListPosition < 0)
				{
					ListPosition = Limit - 15;
					ID = 15;
				}
			}
			else
			{
				ID--;
			}
			UpdateSchemeInfo();
		}
		if (InputManager.TappedDown || HeldDown > 0.5f)
		{
			if (HeldDown > 0.5f)
			{
				HeldDown = 0.45f;
			}
			if (ID == 15)
			{
				ListPosition++;
				if (ID + ListPosition > Limit)
				{
					ListPosition = 0;
					ID = 1;
				}
			}
			else
			{
				ID++;
			}
			UpdateSchemeInfo();
		}
		if (Input.GetButtonDown(InputNames.Xbox_A))
		{
			AudioSource component = GetComponent<AudioSource>();
			if (PromptBar.Label[0].text != string.Empty)
			{
				if (SchemeNameLabels[ID].color.a == 1f)
				{
					SchemeManager.enabled = true;
					SchemeManager.CurrentScheme = ID + ListPosition;
					if (ID == 5)
					{
						SchemeManager.ClockCheck = true;
					}
					Debug.Log("Before pressing the button, SchemeGlobals.GetSchemeStage(2) was " + SchemeGlobals.GetSchemeStage(2));
					Debug.Log("Selecting a scheme. Checking to see if we had already unlocked that scheme or not.");
					if (!SchemeGlobals.GetSchemeUnlocked(ID + ListPosition))
					{
						Debug.Log("Scheme hadn't been unlocked.");
						if (Inventory.PantyShots >= SchemeCosts[ID + ListPosition])
						{
							Inventory.PantyShots -= SchemeCosts[ID + ListPosition];
							SchemeGlobals.SetSchemeUnlocked(ID + ListPosition, value: true);
							SchemeGlobals.CurrentScheme = ID + ListPosition;
							if (SchemeGlobals.GetSchemeStage(ID + ListPosition) == 0)
							{
								SchemeGlobals.SetSchemeStage(ID + ListPosition, 1);
							}
							UpdateSchemeDestinations();
							UpdateInstructions();
							UpdateSchemeList();
							UpdateSchemeInfo();
							component.clip = InfoPurchase;
							component.Play();
						}
					}
					else
					{
						Debug.Log("Scheme had already been unlocked.");
						if (SchemeGlobals.CurrentScheme == ID + ListPosition)
						{
							SchemeGlobals.CurrentScheme = 0;
							SchemeManager.CurrentScheme = 0;
							SchemeManager.enabled = false;
						}
						else
						{
							SchemeGlobals.CurrentScheme = ID + ListPosition;
						}
						UpdateSchemeDestinations();
						UpdateInstructions();
						UpdateSchemeInfo();
					}
				}
			}
			else if (SchemeGlobals.GetSchemeStage(ID + ListPosition) != 100 && Inventory.PantyShots < SchemeCosts[ID + ListPosition])
			{
				StudentManager.Yandere.PauseScreen.FavorMenu.Flicker = true;
				component.clip = InfoAfford;
				component.Play();
			}
		}
		if (Input.GetButtonDown(InputNames.Xbox_B))
		{
			PromptBar.ClearButtons();
			PromptBar.Label[0].text = "Accept";
			PromptBar.Label[1].text = "Exit";
			PromptBar.Label[5].text = "Choose";
			PromptBar.UpdateButtons();
			FavorMenu.SetActive(value: true);
			base.gameObject.SetActive(value: false);
		}
	}

	public void UpdateSchemeList()
	{
		Debug.Log("Running this code now.");
		for (int i = 1; i < SchemeNameLabels.Length; i++)
		{
			if (SchemeGlobals.GetSchemeStage(i + ListPosition) == 100)
			{
				UILabel uILabel = SchemeNameLabels[i];
				uILabel.color = new Color(uILabel.color.r, uILabel.color.g, uILabel.color.b, 0.5f);
				SchemeCostLabels[i].text = string.Empty;
				continue;
			}
			if (SchemeGlobals.GetSchemeUnlocked(i))
			{
				SchemeCostLabels[i].text = SchemeCosts[i].ToString();
			}
			else
			{
				SchemeCostLabels[i].text = string.Empty;
			}
			if (SchemeGlobals.GetSchemeStage(i) > SchemeGlobals.GetSchemePreviousStage(i))
			{
				SchemeGlobals.SetSchemePreviousStage(i, SchemeGlobals.GetSchemeStage(i));
			}
		}
	}

	public void UpdateSchemeInfo()
	{
		Debug.Log("SchemeGlobals.GetSchemeStage(1) is " + SchemeGlobals.GetSchemeStage(1));
		Debug.Log("SchemeGlobals.GetSchemeStage(2) is " + SchemeGlobals.GetSchemeStage(2));
		Debug.Log("SchemeGlobals.GetSchemeStage(3) is " + SchemeGlobals.GetSchemeStage(3));
		Debug.Log("SchemeGlobals.GetSchemeStage(4) is " + SchemeGlobals.GetSchemeStage(4));
		Debug.Log("SchemeGlobals.GetSchemeStage(5) is " + SchemeGlobals.GetSchemeStage(5));
		if (SchemeGlobals.GetSchemeStage(ID + ListPosition) != 100)
		{
			if (!SchemeGlobals.GetSchemeUnlocked(ID + ListPosition))
			{
				Arrow.gameObject.SetActive(value: false);
				if (Inventory != null)
				{
					PromptBar.Label[0].text = ((Inventory.PantyShots >= SchemeCosts[ID + ListPosition]) ? "Purchase" : string.Empty);
				}
				PromptBar.UpdateButtons();
			}
			else if (SchemeGlobals.CurrentScheme == ID + ListPosition)
			{
				Arrow.gameObject.SetActive(value: true);
				Arrow.localPosition = new Vector3(Arrow.localPosition.x, -10f - 21f * (float)SchemeGlobals.GetSchemeStage(ID + ListPosition), Arrow.localPosition.z);
				PromptBar.Label[0].text = "Stop Tracking";
				PromptBar.UpdateButtons();
			}
			else
			{
				Arrow.gameObject.SetActive(value: false);
				PromptBar.Label[0].text = "Start Tracking";
				PromptBar.UpdateButtons();
			}
		}
		else
		{
			PromptBar.Label[0].text = string.Empty;
			PromptBar.UpdateButtons();
		}
		Highlight.localPosition = new Vector3(Highlight.localPosition.x, 200f - 25f * (float)ID, Highlight.localPosition.z);
		for (int i = 1; i < SchemeNameLabels.Length; i++)
		{
			SchemeNameLabels[i].text = SchemeNames[i + ListPosition];
			SchemeCostLabels[i].text = SchemeCosts[i + ListPosition].ToString() ?? "";
			SchemeDeadlineLabels[i].text = SchemeDeadlines[i + ListPosition];
			if (DisableScheme[i + ListPosition])
			{
				SchemeNameLabels[i].color = new Color(0f, 0f, 0f, 0.5f);
			}
			else
			{
				SchemeNameLabels[i].color = new Color(0f, 0f, 0f, 1f);
			}
			if (SchemeManager != null)
			{
				if (SchemeManager.CurrentScheme == i + ListPosition)
				{
					Exclamations[i].enabled = true;
				}
				else
				{
					Exclamations[i].enabled = false;
				}
			}
		}
		SchemeIcon.mainTexture = SchemeIcons[ID + ListPosition];
		SchemeDesc.text = SchemeDescs[ID + ListPosition];
		if (SchemeGlobals.GetSchemeStage(ID + ListPosition) == 100)
		{
			SchemeInstructions.text = "This scheme is no longer available.";
		}
		else
		{
			SchemeInstructions.text = ((!SchemeGlobals.GetSchemeUnlocked(ID + ListPosition)) ? ("Skills Required:\n" + SchemeSkills[ID + ListPosition]) : SchemeSteps[ID + ListPosition]);
		}
		UpdatePantyCount();
	}

	public void UpdatePantyCount()
	{
		if (Inventory != null)
		{
			PantyCount.text = Inventory.PantyShots.ToString();
		}
	}

	public void UpdateInstructions()
	{
		Debug.Log("Before running UpdateInstructions(), SchemeGlobals.GetSchemeStage(2) was: " + SchemeGlobals.GetSchemeStage(2));
		Steps = SchemeSteps[SchemeGlobals.CurrentScheme].Split('\n');
		Debug.Log("At this moment, SchemeGlobals.CurrentScheme is: " + SchemeGlobals.CurrentScheme);
		if (SchemeGlobals.CurrentScheme > 0)
		{
			if (SchemeGlobals.CurrentScheme == 4 && SchemeGlobals.GetSchemeStage(4) == 1 && ((StudentManager.Yandere.Weapon[1] != null && StudentManager.Yandere.Weapon[1].WeaponID == 6) || (StudentManager.Yandere.Weapon[2] != null && StudentManager.Yandere.Weapon[2].WeaponID == 6)))
			{
				SchemeGlobals.SetSchemeStage(4, 2);
			}
			if (SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) < 100)
			{
				Debug.Log("SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) is less than 100...");
				if (SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) < 1)
				{
					Debug.Log("SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) is less than 1...");
					SchemeGlobals.SetSchemeStage(SchemeGlobals.CurrentScheme, Steps.Length);
				}
				else if (SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) > Steps.Length)
				{
					Debug.Log("SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) is greater than the number of stea");
					SchemeGlobals.SetSchemeStage(SchemeGlobals.CurrentScheme, 1);
				}
				HUDIcon.SetActive(value: true);
				HUDInstructions.text = Steps[SchemeGlobals.GetSchemeStage(SchemeGlobals.CurrentScheme) - 1].ToString();
			}
			else
			{
				Arrow.gameObject.SetActive(value: false);
				HUDIcon.gameObject.SetActive(value: false);
				HUDInstructions.text = string.Empty;
				SchemeManager.CurrentScheme = 0;
			}
		}
		else
		{
			HUDIcon.SetActive(value: false);
			HUDInstructions.text = string.Empty;
		}
		if (SchemeGlobals.CurrentScheme < 7)
		{
			NextStepInput.SetActive(value: false);
		}
		else
		{
			NextStepInput.SetActive(value: true);
		}
		Debug.Log("After running UpdateInstructions(), SchemeGlobals.GetSchemeStage(2) is: " + SchemeGlobals.GetSchemeStage(2));
	}

	public void UpdateSchemeDestinations()
	{
		Debug.Log("Before running UpdateSchemeDestinations(), SchemeGlobals.GetSchemeStage(2) was: " + SchemeGlobals.GetSchemeStage(2));
		if (StudentManager.Students[StudentManager.RivalID] != null)
		{
			Scheme1Destinations[3] = StudentManager.Students[StudentManager.RivalID].transform;
			Scheme1Destinations[7] = StudentManager.Students[StudentManager.RivalID].transform;
			Scheme4Destinations[5] = StudentManager.Students[StudentManager.RivalID].transform;
			Scheme4Destinations[6] = StudentManager.Students[StudentManager.RivalID].transform;
		}
		if (StudentManager.Students[2] != null)
		{
			Scheme2Destinations[3] = StudentManager.Students[2].transform;
		}
		if (StudentManager.Students[97] != null)
		{
			Scheme5Destinations[3] = StudentManager.Students[97].transform;
		}
		if (SchemeGlobals.CurrentScheme == 1)
		{
			SchemeDestinations = Scheme1Destinations;
		}
		else if (SchemeGlobals.CurrentScheme == 2)
		{
			SchemeDestinations = Scheme2Destinations;
		}
		else if (SchemeGlobals.CurrentScheme == 3)
		{
			SchemeDestinations = Scheme3Destinations;
		}
		else if (SchemeGlobals.CurrentScheme == 4)
		{
			SchemeDestinations = Scheme4Destinations;
		}
		else if (SchemeGlobals.CurrentScheme == 5)
		{
			SchemeDestinations = Scheme5Destinations;
		}
		Debug.Log("After running UpdateSchemeDestinations(), SchemeGlobals.GetSchemeStage(2) is: " + SchemeGlobals.GetSchemeStage(2));
	}
}
