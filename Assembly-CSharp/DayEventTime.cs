﻿// Decompiled with JetBrains decompiler
// Type: DayEventTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03C576EE-B2A0-4A87-90DA-D90BE80DF8AE
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class DayEventTime : IScheduledEventTime
{
  [SerializeField]
  private int week;
  [SerializeField]
  private DayOfWeek weekday;

  public DayEventTime(int week, DayOfWeek weekday)
  {
    this.week = week;
    this.weekday = weekday;
  }

  public ScheduledEventTimeType ScheduleType => ScheduledEventTimeType.Day;

  public bool OccurringNow(DateAndTime currentTime) => currentTime.Week == this.week && currentTime.Weekday == this.weekday;

  public bool OccursInTheFuture(DateAndTime currentTime) => currentTime.Week == this.week ? currentTime.Weekday < this.weekday : currentTime.Week < this.week;

  public bool OccurredInThePast(DateAndTime currentTime) => currentTime.Week == this.week ? currentTime.Weekday > this.weekday : currentTime.Week > this.week;
}
