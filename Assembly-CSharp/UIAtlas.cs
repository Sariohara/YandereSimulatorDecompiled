﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class UIAtlas : MonoBehaviour, INGUIAtlas
{
	// Token: 0x17000117 RID: 279
	// (get) Token: 0x0600069F RID: 1695 RVA: 0x00038980 File Offset: 0x00036B80
	// (set) Token: 0x060006A0 RID: 1696 RVA: 0x000389A4 File Offset: 0x00036BA4
	public Material spriteMaterial
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement == null)
			{
				return this.material;
			}
			return replacement.spriteMaterial;
		}
		set
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				replacement.spriteMaterial = value;
				return;
			}
			if (this.material == null)
			{
				this.mPMA = 0;
				this.material = value;
				return;
			}
			this.MarkAsChanged();
			this.mPMA = -1;
			this.material = value;
			this.MarkAsChanged();
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x060006A1 RID: 1697 RVA: 0x000389FC File Offset: 0x00036BFC
	public bool premultipliedAlpha
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.premultipliedAlpha;
			}
			if (this.mPMA == -1)
			{
				Material spriteMaterial = this.spriteMaterial;
				this.mPMA = ((spriteMaterial != null && spriteMaterial.shader != null && spriteMaterial.shader.name.Contains("Premultiplied")) ? 1 : 0);
			}
			return this.mPMA == 1;
		}
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00038A6C File Offset: 0x00036C6C
	// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00038AA4 File Offset: 0x00036CA4
	public List<UISpriteData> spriteList
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.spriteList;
			}
			if (this.mSprites.Count == 0)
			{
				this.Upgrade();
			}
			return this.mSprites;
		}
		set
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				replacement.spriteList = value;
				return;
			}
			this.mSprites = value;
		}
	}

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00038ACC File Offset: 0x00036CCC
	public Texture texture
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				return replacement.texture;
			}
			if (!(this.material != null))
			{
				return null;
			}
			return this.material.mainTexture;
		}
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00038B08 File Offset: 0x00036D08
	// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00038B2C File Offset: 0x00036D2C
	public float pixelSize
	{
		get
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement == null)
			{
				return this.mPixelSize;
			}
			return replacement.pixelSize;
		}
		set
		{
			INGUIAtlas replacement = this.replacement;
			if (replacement != null)
			{
				replacement.pixelSize = value;
				return;
			}
			float num = Mathf.Clamp(value, 0.25f, 4f);
			if (this.mPixelSize != num)
			{
				this.mPixelSize = num;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00038B72 File Offset: 0x00036D72
	// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00038B80 File Offset: 0x00036D80
	public INGUIAtlas replacement
	{
		get
		{
			return this.mReplacement as INGUIAtlas;
		}
		set
		{
			INGUIAtlas inguiatlas = value;
			if (inguiatlas == this)
			{
				inguiatlas = null;
			}
			if (this.mReplacement as INGUIAtlas != inguiatlas)
			{
				if (inguiatlas != null && inguiatlas.replacement == this)
				{
					inguiatlas.replacement = null;
				}
				if (this.mReplacement != null)
				{
					this.MarkAsChanged();
				}
				this.mReplacement = (inguiatlas as UnityEngine.Object);
				if (inguiatlas != null)
				{
					this.material = null;
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00038BE8 File Offset: 0x00036DE8
	public UISpriteData GetSprite(string name)
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			return replacement.GetSprite(name);
		}
		if (!string.IsNullOrEmpty(name))
		{
			if (this.mSprites.Count == 0)
			{
				this.Upgrade();
			}
			if (this.mSprites.Count == 0)
			{
				return null;
			}
			if (this.mSpriteIndices.Count != this.mSprites.Count)
			{
				this.MarkSpriteListAsChanged();
			}
			int num;
			if (this.mSpriteIndices.TryGetValue(name, out num))
			{
				if (num > -1 && num < this.mSprites.Count)
				{
					return this.mSprites[num];
				}
				this.MarkSpriteListAsChanged();
				if (!this.mSpriteIndices.TryGetValue(name, out num))
				{
					return null;
				}
				return this.mSprites[num];
			}
			else
			{
				int i = 0;
				int count = this.mSprites.Count;
				while (i < count)
				{
					UISpriteData uispriteData = this.mSprites[i];
					if (!string.IsNullOrEmpty(uispriteData.name) && name == uispriteData.name)
					{
						this.MarkSpriteListAsChanged();
						return uispriteData;
					}
					i++;
				}
			}
		}
		return null;
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x00038CF4 File Offset: 0x00036EF4
	public void MarkSpriteListAsChanged()
	{
		this.mSpriteIndices.Clear();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			this.mSpriteIndices[this.mSprites[i].name] = i;
			i++;
		}
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00038D41 File Offset: 0x00036F41
	public void SortAlphabetically()
	{
		this.mSprites.Sort((UISpriteData s1, UISpriteData s2) => s1.name.CompareTo(s2.name));
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x00038D70 File Offset: 0x00036F70
	public BetterList<string> GetListOfSprites()
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			return replacement.GetListOfSprites();
		}
		if (this.mSprites.Count == 0)
		{
			this.Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			UISpriteData uispriteData = this.mSprites[i];
			if (uispriteData != null && !string.IsNullOrEmpty(uispriteData.name))
			{
				betterList.Add(uispriteData.name);
			}
			i++;
		}
		return betterList;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00038DF0 File Offset: 0x00036FF0
	public BetterList<string> GetListOfSprites(string match)
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			return replacement.GetListOfSprites(match);
		}
		if (string.IsNullOrEmpty(match))
		{
			return this.GetListOfSprites();
		}
		if (this.mSprites.Count == 0)
		{
			this.Upgrade();
		}
		BetterList<string> betterList = new BetterList<string>();
		int i = 0;
		int count = this.mSprites.Count;
		while (i < count)
		{
			UISpriteData uispriteData = this.mSprites[i];
			if (uispriteData != null && !string.IsNullOrEmpty(uispriteData.name) && string.Equals(match, uispriteData.name, StringComparison.OrdinalIgnoreCase))
			{
				betterList.Add(uispriteData.name);
				return betterList;
			}
			i++;
		}
		string[] array = match.Split(new char[]
		{
			' '
		}, StringSplitOptions.RemoveEmptyEntries);
		for (int j = 0; j < array.Length; j++)
		{
			array[j] = array[j].ToLower();
		}
		int k = 0;
		int count2 = this.mSprites.Count;
		while (k < count2)
		{
			UISpriteData uispriteData2 = this.mSprites[k];
			if (uispriteData2 != null && !string.IsNullOrEmpty(uispriteData2.name))
			{
				string text = uispriteData2.name.ToLower();
				int num = 0;
				for (int l = 0; l < array.Length; l++)
				{
					if (text.Contains(array[l]))
					{
						num++;
					}
				}
				if (num == array.Length)
				{
					betterList.Add(uispriteData2.name);
				}
			}
			k++;
		}
		return betterList;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00038F50 File Offset: 0x00037150
	public bool References(INGUIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (atlas == this)
		{
			return true;
		}
		INGUIAtlas replacement = this.replacement;
		return replacement != null && replacement.References(atlas);
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00038F7C File Offset: 0x0003717C
	public void MarkAsChanged()
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			replacement.MarkAsChanged();
		}
		UISprite[] array = NGUITools.FindActive<UISprite>();
		int i = 0;
		int num = array.Length;
		while (i < num)
		{
			UISprite uisprite = array[i];
			if (NGUITools.CheckIfRelated(this, uisprite.atlas))
			{
				INGUIAtlas atlas = uisprite.atlas;
				uisprite.atlas = null;
				uisprite.atlas = atlas;
			}
			i++;
		}
		NGUIFont[] array2 = Resources.FindObjectsOfTypeAll<NGUIFont>();
		int j = 0;
		int num2 = array2.Length;
		while (j < num2)
		{
			NGUIFont nguifont = array2[j];
			if (nguifont.atlas != null && NGUITools.CheckIfRelated(this, nguifont.atlas))
			{
				INGUIAtlas atlas2 = nguifont.atlas;
				nguifont.atlas = null;
				nguifont.atlas = atlas2;
			}
			j++;
		}
		UIFont[] array3 = Resources.FindObjectsOfTypeAll<UIFont>();
		int k = 0;
		int num3 = array3.Length;
		while (k < num3)
		{
			UIFont uifont = array3[k];
			if (NGUITools.CheckIfRelated(this, uifont.atlas))
			{
				INGUIAtlas atlas3 = uifont.atlas;
				uifont.atlas = null;
				uifont.atlas = atlas3;
			}
			k++;
		}
		UILabel[] array4 = NGUITools.FindActive<UILabel>();
		int l = 0;
		int num4 = array4.Length;
		while (l < num4)
		{
			UILabel uilabel = array4[l];
			if (uilabel.atlas != null && NGUITools.CheckIfRelated(this, uilabel.atlas))
			{
				INGUIAtlas atlas4 = uilabel.atlas;
				INGUIFont bitmapFont = uilabel.bitmapFont;
				uilabel.bitmapFont = null;
				uilabel.bitmapFont = bitmapFont;
			}
			l++;
		}
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x000390E4 File Offset: 0x000372E4
	private bool Upgrade()
	{
		INGUIAtlas replacement = this.replacement;
		if (replacement != null)
		{
			UIAtlas uiatlas = replacement as UIAtlas;
			if (uiatlas != null)
			{
				return uiatlas.Upgrade();
			}
		}
		if (this.mSprites.Count == 0 && this.sprites.Count > 0 && this.material)
		{
			Texture mainTexture = this.material.mainTexture;
			int width = (mainTexture != null) ? mainTexture.width : 512;
			int height = (mainTexture != null) ? mainTexture.height : 512;
			for (int i = 0; i < this.sprites.Count; i++)
			{
				UIAtlas.Sprite sprite = this.sprites[i];
				Rect outer = sprite.outer;
				Rect inner = sprite.inner;
				if (this.mCoordinates == UIAtlas.Coordinates.TexCoords)
				{
					NGUIMath.ConvertToPixels(outer, width, height, true);
					NGUIMath.ConvertToPixels(inner, width, height, true);
				}
				UISpriteData uispriteData = new UISpriteData();
				uispriteData.name = sprite.name;
				uispriteData.x = Mathf.RoundToInt(outer.xMin);
				uispriteData.y = Mathf.RoundToInt(outer.yMin);
				uispriteData.width = Mathf.RoundToInt(outer.width);
				uispriteData.height = Mathf.RoundToInt(outer.height);
				uispriteData.paddingLeft = Mathf.RoundToInt(sprite.paddingLeft * outer.width);
				uispriteData.paddingRight = Mathf.RoundToInt(sprite.paddingRight * outer.width);
				uispriteData.paddingBottom = Mathf.RoundToInt(sprite.paddingBottom * outer.height);
				uispriteData.paddingTop = Mathf.RoundToInt(sprite.paddingTop * outer.height);
				uispriteData.borderLeft = Mathf.RoundToInt(inner.xMin - outer.xMin);
				uispriteData.borderRight = Mathf.RoundToInt(outer.xMax - inner.xMax);
				uispriteData.borderBottom = Mathf.RoundToInt(outer.yMax - inner.yMax);
				uispriteData.borderTop = Mathf.RoundToInt(inner.yMin - outer.yMin);
				this.mSprites.Add(uispriteData);
			}
			this.sprites.Clear();
			return true;
		}
		return false;
	}

	// Token: 0x04000629 RID: 1577
	[HideInInspector]
	[SerializeField]
	private Material material;

	// Token: 0x0400062A RID: 1578
	[HideInInspector]
	[SerializeField]
	private List<UISpriteData> mSprites = new List<UISpriteData>();

	// Token: 0x0400062B RID: 1579
	[HideInInspector]
	[SerializeField]
	private float mPixelSize = 1f;

	// Token: 0x0400062C RID: 1580
	[HideInInspector]
	[SerializeField]
	private UnityEngine.Object mReplacement;

	// Token: 0x0400062D RID: 1581
	[HideInInspector]
	[SerializeField]
	private UIAtlas.Coordinates mCoordinates;

	// Token: 0x0400062E RID: 1582
	[HideInInspector]
	[SerializeField]
	private List<UIAtlas.Sprite> sprites = new List<UIAtlas.Sprite>();

	// Token: 0x0400062F RID: 1583
	[NonSerialized]
	private int mPMA = -1;

	// Token: 0x04000630 RID: 1584
	[NonSerialized]
	private Dictionary<string, int> mSpriteIndices = new Dictionary<string, int>();

	// Token: 0x0200060E RID: 1550
	[Serializable]
	private class Sprite
	{
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x001F8E6C File Offset: 0x001F706C
		public bool hasPadding
		{
			get
			{
				return this.paddingLeft != 0f || this.paddingRight != 0f || this.paddingTop != 0f || this.paddingBottom != 0f;
			}
		}

		// Token: 0x04004DAD RID: 19885
		public string name = "Unity Bug";

		// Token: 0x04004DAE RID: 19886
		public Rect outer = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04004DAF RID: 19887
		public Rect inner = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04004DB0 RID: 19888
		public bool rotated;

		// Token: 0x04004DB1 RID: 19889
		public float paddingLeft;

		// Token: 0x04004DB2 RID: 19890
		public float paddingRight;

		// Token: 0x04004DB3 RID: 19891
		public float paddingTop;

		// Token: 0x04004DB4 RID: 19892
		public float paddingBottom;
	}

	// Token: 0x0200060F RID: 1551
	private enum Coordinates
	{
		// Token: 0x04004DB6 RID: 19894
		Pixels,
		// Token: 0x04004DB7 RID: 19895
		TexCoords
	}
}