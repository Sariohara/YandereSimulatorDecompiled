﻿// Decompiled with JetBrains decompiler
// Type: RetroMinigameScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12831466-57D6-4F5A-B867-CD140BE439C0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RetroMinigameScript : MonoBehaviour
{
  public GameObject GameOverGraphic;
  public GameObject MinigameCamera;
  public GameObject Heart;
  public GameObject[] PipeSet;
  public UILabel ScoreLabel;
  public float Momentum;
  public bool GameOver;
  public float Speed;
  public int Score;
  public bool Show;

  private void Start()
  {
    this.Heart.transform.localPosition = new Vector3(-3.125f, 0.0f, 1.5f);
    this.PipeSet[1].transform.localPosition = new Vector3(8f, Random.RandomRange(-1.52f, 1.52f), 1f);
    this.PipeSet[2].transform.localPosition = new Vector3(13f, Random.RandomRange(-1.52f, 1.52f), 1f);
    this.PipeSet[3].transform.localPosition = new Vector3(18f, Random.RandomRange(-1.52f, 1.52f), 1f);
    this.PipeSet[4].transform.localPosition = new Vector3(23f, Random.RandomRange(-1.52f, 1.52f), 1f);
    this.PipeSet[5].transform.localPosition = new Vector3(28f, Random.RandomRange(-1.52f, 1.52f), 1f);
    this.GameOverGraphic.SetActive(false);
    this.ScoreLabel.text = "0";
    this.GameOver = false;
    this.Momentum = 1f;
    this.Score = 0;
    this.Speed = 2f;
  }

  private void Update()
  {
    if (this.Show)
    {
      if ((double) this.transform.localPosition.y < -1.0)
      {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(0.0f, 0.0f, 0.0f), Time.unscaledDeltaTime * 10f);
        if ((double) this.transform.localPosition.y <= -1.0)
          return;
        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        this.MinigameCamera.SetActive(true);
      }
      else if (!this.GameOver)
      {
        this.Speed += Time.unscaledDeltaTime * 0.1f;
        for (int index = 1; index < 6; ++index)
        {
          this.PipeSet[index].transform.localPosition -= new Vector3(Time.unscaledDeltaTime * this.Speed, 0.0f, 0.0f);
          if ((double) this.PipeSet[index].transform.localPosition.x < -8.0)
          {
            this.PipeSet[index].transform.localPosition = new Vector3(17f, Random.RandomRange(-1.52f, 1.52f), 1f);
            ++this.Score;
            this.ScoreLabel.text = this.Score.ToString() ?? "";
          }
        }
        this.Heart.transform.localPosition += new Vector3(0.0f, (float) ((double) this.Momentum * (double) Time.unscaledDeltaTime * 5.0), 0.0f);
        this.Momentum -= Time.unscaledDeltaTime * 5f;
        if (Input.GetButtonDown("A"))
          this.Momentum = 1f;
        if ((double) this.Heart.transform.localPosition.y > 4.5999999046325684)
        {
          this.Heart.transform.localPosition = new Vector3(-3.125f, 4.6f, 1.5f);
          this.Momentum = 0.0f;
        }
        if ((double) this.Heart.transform.localPosition.y > -4.5999999046325684)
          return;
        this.Heart.transform.localPosition = new Vector3(-3.125f, -4.6f, 1.5f);
        this.GetGameOver();
      }
      else
      {
        if (!Input.GetButtonDown("A"))
          return;
        this.Start();
      }
    }
    else
    {
      if ((double) this.transform.localPosition.y <= -1154.0)
        return;
      this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(0.0f, -1155f, 0.0f), Time.unscaledDeltaTime * 10f);
      if ((double) this.transform.localPosition.y >= -1154.0)
        return;
      this.transform.localPosition = new Vector3(0.0f, -1155f, 0.0f);
      this.gameObject.SetActive(false);
    }
  }

  private void GetGameOver()
  {
    this.GameOverGraphic.SetActive(true);
    this.GameOver = true;
  }
}