﻿// Decompiled with JetBrains decompiler
// Type: AchievementPopUpScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12831466-57D6-4F5A-B867-CD140BE439C0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AchievementPopUpScript : MonoBehaviour
{
  public string[] AchievementFancyNames;
  public string[] AchievementNames;
  public Texture[] AchievementIcons;
  public int[] PreviousNumber;
  public float ShowTimer;
  public float Timer;
  public Camera MyCamera;
  public UITexture Icon;
  public UILabel Label;
  public bool Show;
  public int DebugID;

  private void Awake() => Object.DontDestroyOnLoad((Object) this.transform.parent.parent.gameObject);

  private void Start()
  {
    this.transform.localPosition = new Vector3(637f, -613f, 0.0f);
    for (int index = 1; index < this.AchievementNames.Length; ++index)
      this.PreviousNumber[index] = PlayerPrefs.GetInt(this.AchievementNames[index]);
  }

  private void Update()
  {
    if (this.Show)
    {
      this.ShowTimer += Time.deltaTime;
      if ((double) this.ShowTimer < 5.0)
      {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(this.transform.localPosition.x, -413f, 0.0f), Time.unscaledDeltaTime * 10f);
      }
      else
      {
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, new Vector3(this.transform.localPosition.x, -613f, 0.0f), Time.unscaledDeltaTime * 100f);
        if (this.transform.localPosition == new Vector3(this.transform.localPosition.x, -613f, 0.0f))
        {
          this.MyCamera.enabled = false;
          this.ShowTimer = 0.0f;
          this.Show = false;
        }
      }
    }
    else
    {
      this.Timer = Mathf.MoveTowards(this.Timer, 1f, Time.deltaTime);
      if ((double) this.Timer == 1.0)
      {
        this.Timer = 0.0f;
        if (PlayerPrefs.GetInt("a") == 1)
        {
          PlayerPrefs.SetInt("a", 0);
          for (int index = 1; index < this.AchievementNames.Length; ++index)
          {
            if (PlayerPrefs.GetInt(this.AchievementNames[index]) != this.PreviousNumber[index])
            {
              Debug.Log((object) ("ID is : " + index.ToString() + "."));
              Debug.Log((object) ("AchievementNames[ID] is : " + this.AchievementNames[index] + "."));
              Debug.Log((object) ("PlayerPrefs.GetInt(AchievementNames[ID]) is : " + PlayerPrefs.GetInt(this.AchievementNames[index]).ToString() + "."));
              Debug.Log((object) ("PreviousNumber[ID] is : " + this.PreviousNumber[index].ToString() + "."));
              Debug.Log((object) ("Achievement #" + index.ToString() + " was obtained. Attempting to update label and icon."));
              this.PreviousNumber[index] = PlayerPrefs.GetInt(this.AchievementNames[index]);
              this.Label.text = this.AchievementFancyNames[index] ?? "";
              this.Icon.mainTexture = this.AchievementIcons[index];
              this.MyCamera.enabled = true;
              this.Show = true;
              index = 99;
            }
          }
        }
      }
    }
    if (!Input.GetKeyDown(KeyCode.LeftControl))
      return;
    PlayerPrefs.SetInt("a", 1);
    this.PreviousNumber[31] = 0;
    PlayerPrefs.SetInt("LifeNote", 1);
  }
}