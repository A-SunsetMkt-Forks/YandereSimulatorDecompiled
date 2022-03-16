﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[AddComponentMenu("NGUI/UI/Input Field")]
public class UIInput : MonoBehaviour
{
	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000736 RID: 1846 RVA: 0x0003E33E File Offset: 0x0003C53E
	// (set) Token: 0x06000737 RID: 1847 RVA: 0x0003E354 File Offset: 0x0003C554
	public string defaultText
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mDefaultText;
		}
		set
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			this.mDefaultText = value;
			this.UpdateLabel();
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000738 RID: 1848 RVA: 0x0003E371 File Offset: 0x0003C571
	// (set) Token: 0x06000739 RID: 1849 RVA: 0x0003E387 File Offset: 0x0003C587
	public Color defaultColor
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mDefaultColor;
		}
		set
		{
			this.mDefaultColor = value;
			if (!this.isSelected)
			{
				this.label.color = value;
			}
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x0600073A RID: 1850 RVA: 0x0003E3A4 File Offset: 0x0003C5A4
	public bool inputShouldBeHidden
	{
		get
		{
			return this.hideInput && this.label != null && !this.label.multiLine && this.inputType != UIInput.InputType.Password;
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x0600073B RID: 1851 RVA: 0x0003E3D7 File Offset: 0x0003C5D7
	// (set) Token: 0x0600073C RID: 1852 RVA: 0x0003E3DF File Offset: 0x0003C5DF
	[Obsolete("Use UIInput.value instead")]
	public string text
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x0600073D RID: 1853 RVA: 0x0003E3E8 File Offset: 0x0003C5E8
	// (set) Token: 0x0600073E RID: 1854 RVA: 0x0003E3FE File Offset: 0x0003C5FE
	public string value
	{
		get
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			return this.mValue;
		}
		set
		{
			this.Set(value, true);
		}
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x0003E408 File Offset: 0x0003C608
	public void Set(string value, bool notify = true)
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (value == this.value)
		{
			return;
		}
		UIInput.mDrawStart = 0;
		value = this.Validate(value);
		if (this.mValue != value)
		{
			this.mValue = value;
			this.mLoadSavedValue = false;
			if (this.isSelected)
			{
				if (string.IsNullOrEmpty(value))
				{
					this.mSelectionStart = 0;
					this.mSelectionEnd = 0;
				}
				else
				{
					this.mSelectionStart = value.Length;
					this.mSelectionEnd = this.mSelectionStart;
				}
			}
			else if (this.mStarted)
			{
				this.SaveToPlayerPrefs(value);
			}
			this.UpdateLabel();
			if (notify)
			{
				this.ExecuteOnChange();
			}
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000740 RID: 1856 RVA: 0x0003E4B5 File Offset: 0x0003C6B5
	// (set) Token: 0x06000741 RID: 1857 RVA: 0x0003E4BD File Offset: 0x0003C6BD
	[Obsolete("Use UIInput.isSelected instead")]
	public bool selected
	{
		get
		{
			return this.isSelected;
		}
		set
		{
			this.isSelected = value;
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06000742 RID: 1858 RVA: 0x0003E4C6 File Offset: 0x0003C6C6
	// (set) Token: 0x06000743 RID: 1859 RVA: 0x0003E4D3 File Offset: 0x0003C6D3
	public bool isSelected
	{
		get
		{
			return UIInput.selection == this;
		}
		set
		{
			if (!value)
			{
				if (this.isSelected)
				{
					UICamera.selectedObject = null;
					return;
				}
			}
			else
			{
				UICamera.selectedObject = base.gameObject;
			}
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06000744 RID: 1860 RVA: 0x0003E4F2 File Offset: 0x0003C6F2
	// (set) Token: 0x06000745 RID: 1861 RVA: 0x0003E50E File Offset: 0x0003C70E
	public int cursorPosition
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06000746 RID: 1862 RVA: 0x0003E525 File Offset: 0x0003C725
	// (set) Token: 0x06000747 RID: 1863 RVA: 0x0003E541 File Offset: 0x0003C741
	public int selectionStart
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionStart;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionStart = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06000748 RID: 1864 RVA: 0x0003E558 File Offset: 0x0003C758
	// (set) Token: 0x06000749 RID: 1865 RVA: 0x0003E574 File Offset: 0x0003C774
	public int selectionEnd
	{
		get
		{
			if (!this.isSelected)
			{
				return this.value.Length;
			}
			return this.mSelectionEnd;
		}
		set
		{
			if (this.isSelected)
			{
				this.mSelectionEnd = value;
				this.UpdateLabel();
			}
		}
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x0600074A RID: 1866 RVA: 0x0003E58B File Offset: 0x0003C78B
	public UITexture caret
	{
		get
		{
			return this.mCaret;
		}
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0003E594 File Offset: 0x0003C794
	public string Validate(string val)
	{
		if (string.IsNullOrEmpty(val))
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(val.Length);
		foreach (char c in val)
		{
			if (this.onValidate != null)
			{
				c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
		}
		if (this.characterLimit > 0 && stringBuilder.Length > this.characterLimit)
		{
			return stringBuilder.ToString(0, this.characterLimit);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0003E64C File Offset: 0x0003C84C
	public void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		if (this.selectOnTab != null)
		{
			if (base.GetComponent<UIKeyNavigation>() == null)
			{
				base.gameObject.AddComponent<UIKeyNavigation>().onDown = this.selectOnTab;
			}
			this.selectOnTab = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.mLoadSavedValue && !string.IsNullOrEmpty(this.savedAs))
		{
			this.LoadValue();
		}
		else
		{
			this.value = this.mValue.Replace("\\n", "\n");
		}
		this.mStarted = true;
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0003E6E8 File Offset: 0x0003C8E8
	protected void Init()
	{
		if (this.mDoInit && this.label != null)
		{
			this.mDoInit = false;
			this.mDefaultText = this.label.text;
			this.mDefaultColor = this.label.color;
			this.mEllipsis = this.label.overflowEllipsis;
			if (this.label.alignment == NGUIText.Alignment.Justified)
			{
				this.label.alignment = NGUIText.Alignment.Left;
				Debug.LogWarning("Input fields using labels with justified alignment are not supported at this time", this);
			}
			this.mAlignment = this.label.alignment;
			this.mPosition = this.label.cachedTransform.localPosition.x;
			this.UpdateLabel();
		}
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0003E7A2 File Offset: 0x0003C9A2
	protected void SaveToPlayerPrefs(string val)
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			if (string.IsNullOrEmpty(val))
			{
				PlayerPrefs.DeleteKey(this.savedAs);
				return;
			}
			PlayerPrefs.SetString(this.savedAs, val);
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0003E7D4 File Offset: 0x0003C9D4
	protected virtual void OnSelect(bool isSelected)
	{
		if (isSelected)
		{
			if (this.label != null)
			{
				this.label.supportEncoding = false;
			}
			if (this.mOnGUI == null)
			{
				this.mOnGUI = base.gameObject.AddComponent<UIInputOnGUI>();
			}
			this.OnSelectEvent();
			return;
		}
		if (this.mOnGUI != null)
		{
			UnityEngine.Object.Destroy(this.mOnGUI);
			this.mOnGUI = null;
		}
		this.OnDeselectEvent();
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0003E84C File Offset: 0x0003CA4C
	protected void OnSelectEvent()
	{
		this.mSelectTime = Time.frameCount;
		UIInput.selection = this;
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null)
		{
			this.mEllipsis = this.label.overflowEllipsis;
			this.label.overflowEllipsis = false;
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mSelectMe = Time.frameCount;
		}
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0003E8C4 File Offset: 0x0003CAC4
	protected void OnDeselectEvent()
	{
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.label != null)
		{
			this.label.overflowEllipsis = this.mEllipsis;
		}
		if (this.label != null && NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (string.IsNullOrEmpty(this.mValue))
			{
				this.label.text = this.mDefaultText;
				this.label.color = this.mDefaultColor;
			}
			else
			{
				this.label.text = this.mValue;
			}
			Input.imeCompositionMode = IMECompositionMode.Auto;
			this.label.alignment = this.mAlignment;
		}
		UIInput.selection = null;
		this.UpdateLabel();
		if (this.submitOnUnselect)
		{
			this.Submit();
		}
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0003E994 File Offset: 0x0003CB94
	protected virtual void Update()
	{
		if (!this.isSelected || this.mSelectTime == Time.frameCount)
		{
			return;
		}
		if (this.mDoInit)
		{
			this.Init();
		}
		if (this.mSelectMe != -1 && this.mSelectMe != Time.frameCount)
		{
			this.mSelectMe = -1;
			this.mSelectionEnd = (string.IsNullOrEmpty(this.mValue) ? 0 : this.mValue.Length);
			UIInput.mDrawStart = 0;
			this.mSelectionStart = (this.selectAllTextOnFocus ? 0 : this.mSelectionEnd);
			this.label.color = this.activeTextColor;
			Vector2 vector = (UICamera.current != null && UICamera.current.cachedCamera != null) ? UICamera.current.cachedCamera.WorldToScreenPoint(this.label.worldCorners[0]) : this.label.worldCorners[0];
			vector.y = (float)Screen.height - vector.y;
			Input.imeCompositionMode = IMECompositionMode.On;
			Input.compositionCursorPos = vector;
			this.UpdateLabel();
			if (string.IsNullOrEmpty(Input.inputString))
			{
				return;
			}
		}
		string compositionString = Input.compositionString;
		if (string.IsNullOrEmpty(compositionString) && !string.IsNullOrEmpty(Input.inputString))
		{
			foreach (char c in Input.inputString)
			{
				if (c >= ' ' && c != '' && c != '' && c != '' && c != '' && c != '')
				{
					this.Insert(c.ToString());
				}
			}
		}
		if (UIInput.mLastIME != compositionString)
		{
			this.mSelectionEnd = (string.IsNullOrEmpty(compositionString) ? this.mSelectionStart : (this.mValue.Length + compositionString.Length));
			UIInput.mLastIME = compositionString;
			this.UpdateLabel();
			this.ExecuteOnChange();
		}
		if (this.mCaret != null && this.mNextBlink < RealTime.time)
		{
			this.mNextBlink = RealTime.time + 0.5f;
			this.mCaret.enabled = !this.mCaret.enabled;
		}
		if (this.isSelected && this.mLastAlpha != this.label.finalAlpha)
		{
			this.UpdateLabel();
		}
		if (this.mCam == null)
		{
			this.mCam = UICamera.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mCam != null)
		{
			bool flag = false;
			if (this.label.multiLine)
			{
				bool flag2 = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
				if (this.onReturnKey == UIInput.OnReturnKey.Submit)
				{
					flag = flag2;
				}
				else
				{
					flag = !flag2;
				}
			}
			if (UICamera.GetKeyDown(this.mCam.submitKey0) || (this.mCam.submitKey0 == KeyCode.Return && UICamera.GetKeyDown(KeyCode.KeypadEnter)))
			{
				if (flag)
				{
					this.Insert("\n");
				}
				else
				{
					if (UICamera.controller.current != null)
					{
						UICamera.controller.clickNotification = UICamera.ClickNotification.None;
					}
					UICamera.currentKey = this.mCam.submitKey0;
					this.Submit();
				}
			}
			if (UICamera.GetKeyDown(this.mCam.submitKey1) || (this.mCam.submitKey1 == KeyCode.Return && UICamera.GetKeyDown(KeyCode.KeypadEnter)))
			{
				if (flag)
				{
					this.Insert("\n");
				}
				else
				{
					if (UICamera.controller.current != null)
					{
						UICamera.controller.clickNotification = UICamera.ClickNotification.None;
					}
					UICamera.currentKey = this.mCam.submitKey1;
					this.Submit();
				}
			}
			if (!this.mCam.useKeyboard && UICamera.GetKeyUp(KeyCode.Tab))
			{
				this.OnKey(KeyCode.Tab);
			}
		}
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0003ED78 File Offset: 0x0003CF78
	private void OnKey(KeyCode key)
	{
		int frameCount = Time.frameCount;
		if (UIInput.mIgnoreKey == frameCount)
		{
			return;
		}
		if (this.mCam != null && (key == this.mCam.cancelKey0 || key == this.mCam.cancelKey1))
		{
			UIInput.mIgnoreKey = frameCount;
			this.isSelected = false;
			return;
		}
		if (key == KeyCode.Tab)
		{
			UIInput.mIgnoreKey = frameCount;
			this.isSelected = false;
			UIKeyNavigation component = base.GetComponent<UIKeyNavigation>();
			if (component != null)
			{
				component.OnKey(KeyCode.Tab);
			}
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0003EDF6 File Offset: 0x0003CFF6
	protected void DoBackspace()
	{
		if (!string.IsNullOrEmpty(this.mValue))
		{
			if (this.mSelectionStart == this.mSelectionEnd)
			{
				if (this.mSelectionStart < 1)
				{
					return;
				}
				this.mSelectionEnd--;
			}
			this.Insert("");
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0003EE38 File Offset: 0x0003D038
	public virtual bool ProcessEvent(Event ev)
	{
		if (this.label == null)
		{
			return false;
		}
		RuntimePlatform platform = Application.platform;
		bool flag = (platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer) ? ((ev.modifiers & EventModifiers.Command) > EventModifiers.None) : ((ev.modifiers & EventModifiers.Control) > EventModifiers.None);
		if ((ev.modifiers & EventModifiers.Alt) != EventModifiers.None)
		{
			flag = false;
		}
		bool flag2 = (ev.modifiers & EventModifiers.Shift) > EventModifiers.None;
		KeyCode keyCode = ev.keyCode;
		if (keyCode <= KeyCode.C)
		{
			if (keyCode == KeyCode.Backspace)
			{
				ev.Use();
				this.DoBackspace();
				return true;
			}
			if (keyCode == KeyCode.A)
			{
				if (flag)
				{
					ev.Use();
					this.mSelectionStart = 0;
					this.mSelectionEnd = this.mValue.Length;
					this.UpdateLabel();
				}
				return true;
			}
			if (keyCode == KeyCode.C)
			{
				if (flag)
				{
					ev.Use();
					NGUITools.clipboard = this.GetSelection();
				}
				return true;
			}
		}
		else if (keyCode <= KeyCode.X)
		{
			if (keyCode == KeyCode.V)
			{
				if (flag)
				{
					ev.Use();
					this.Insert(NGUITools.clipboard);
				}
				return true;
			}
			if (keyCode == KeyCode.X)
			{
				if (flag)
				{
					ev.Use();
					NGUITools.clipboard = this.GetSelection();
					this.Insert("");
				}
				return true;
			}
		}
		else
		{
			if (keyCode == KeyCode.Delete)
			{
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.mSelectionStart == this.mSelectionEnd)
					{
						if (this.mSelectionStart >= this.mValue.Length)
						{
							return true;
						}
						this.mSelectionEnd++;
					}
					this.Insert("");
				}
				return true;
			}
			switch (keyCode)
			{
			case KeyCode.UpArrow:
				ev.Use();
				if (this.onUpArrow != null)
				{
					this.onUpArrow();
				}
				else if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.UpArrow);
					if (this.mSelectionEnd != 0)
					{
						this.mSelectionEnd += UIInput.mDrawStart;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.DownArrow:
				ev.Use();
				if (this.onDownArrow != null)
				{
					this.onDownArrow();
				}
				else if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.DownArrow);
					if (this.mSelectionEnd != this.label.processedText.Length)
					{
						this.mSelectionEnd += UIInput.mDrawStart;
					}
					else
					{
						this.mSelectionEnd = this.mValue.Length;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.RightArrow:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = Mathf.Min(this.mSelectionEnd + 1, this.mValue.Length);
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.LeftArrow:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = Mathf.Max(this.mSelectionEnd - 1, 0);
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.Home:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.label.multiLine)
					{
						this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.Home);
					}
					else
					{
						this.mSelectionEnd = 0;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.End:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					if (this.label.multiLine)
					{
						this.mSelectionEnd = this.label.GetCharacterIndex(this.mSelectionEnd, KeyCode.End);
					}
					else
					{
						this.mSelectionEnd = this.mValue.Length;
					}
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.PageUp:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = 0;
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			case KeyCode.PageDown:
				ev.Use();
				if (!string.IsNullOrEmpty(this.mValue))
				{
					this.mSelectionEnd = this.mValue.Length;
					if (!flag2)
					{
						this.mSelectionStart = this.mSelectionEnd;
					}
					this.UpdateLabel();
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0003F2A8 File Offset: 0x0003D4A8
	protected virtual void Insert(string text)
	{
		string leftText = this.GetLeftText();
		string rightText = this.GetRightText();
		int length = rightText.Length;
		StringBuilder stringBuilder = new StringBuilder(leftText.Length + rightText.Length + text.Length);
		stringBuilder.Append(leftText);
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			char c = text[i];
			if (c == '\b')
			{
				this.DoBackspace();
			}
			else
			{
				if (this.characterLimit > 0 && stringBuilder.Length + length >= this.characterLimit)
				{
					break;
				}
				if (this.onValidate != null)
				{
					c = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				else if (this.validation != UIInput.Validation.None)
				{
					c = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c);
				}
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
			}
			i++;
		}
		this.mSelectionStart = stringBuilder.Length;
		this.mSelectionEnd = this.mSelectionStart;
		int j = 0;
		int length3 = rightText.Length;
		while (j < length3)
		{
			char c2 = rightText[j];
			if (this.onValidate != null)
			{
				c2 = this.onValidate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			else if (this.validation != UIInput.Validation.None)
			{
				c2 = this.Validate(stringBuilder.ToString(), stringBuilder.Length, c2);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			j++;
		}
		this.mValue = stringBuilder.ToString();
		this.UpdateLabel();
		this.ExecuteOnChange();
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0003F430 File Offset: 0x0003D630
	protected string GetLeftText()
	{
		int num = Mathf.Min(new int[]
		{
			this.mSelectionStart,
			this.mSelectionEnd,
			this.mValue.Length
		});
		if (!string.IsNullOrEmpty(this.mValue) && num >= 0)
		{
			return this.mValue.Substring(0, num);
		}
		return "";
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0003F490 File Offset: 0x0003D690
	protected string GetRightText()
	{
		int num = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		if (!string.IsNullOrEmpty(this.mValue) && num < this.mValue.Length)
		{
			return this.mValue.Substring(num);
		}
		return "";
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0003F4DC File Offset: 0x0003D6DC
	protected string GetSelection()
	{
		if (string.IsNullOrEmpty(this.mValue) || this.mSelectionStart == this.mSelectionEnd)
		{
			return "";
		}
		int num = Mathf.Min(this.mSelectionStart, this.mSelectionEnd);
		int num2 = Mathf.Max(this.mSelectionStart, this.mSelectionEnd);
		return this.mValue.Substring(num, num2 - num);
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0003F540 File Offset: 0x0003D740
	protected int GetCharUnderMouse()
	{
		Vector3[] worldCorners = this.label.worldCorners;
		Ray currentRay = UICamera.currentRay;
		Plane plane = new Plane(worldCorners[0], worldCorners[1], worldCorners[2]);
		float distance;
		if (!plane.Raycast(currentRay, out distance))
		{
			return 0;
		}
		return UIInput.mDrawStart + this.label.GetCharacterIndexAtPosition(currentRay.GetPoint(distance), false);
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
	protected virtual void OnPress(bool isPressed)
	{
		if (isPressed && this.isSelected && this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				this.selectionStart = this.mSelectionEnd;
			}
		}
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0003F609 File Offset: 0x0003D809
	protected virtual void OnDrag(Vector2 delta)
	{
		if (this.label != null && (UICamera.currentScheme == UICamera.ControlScheme.Mouse || UICamera.currentScheme == UICamera.ControlScheme.Touch))
		{
			this.selectionEnd = this.GetCharUnderMouse();
		}
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0003F634 File Offset: 0x0003D834
	private void OnDisable()
	{
		this.Cleanup();
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0003F63C File Offset: 0x0003D83C
	protected virtual void Cleanup()
	{
		if (this.mHighlight)
		{
			this.mHighlight.enabled = false;
		}
		if (this.mCaret)
		{
			this.mCaret.enabled = false;
		}
		if (this.mBlankTex)
		{
			NGUITools.Destroy(this.mBlankTex);
			this.mBlankTex = null;
		}
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0003F69C File Offset: 0x0003D89C
	public void Submit()
	{
		if (NGUITools.GetActive(this))
		{
			this.mValue = this.value;
			if (UIInput.current == null)
			{
				UIInput.current = this;
				EventDelegate.Execute(this.onSubmit);
				UIInput.current = null;
			}
			this.SaveToPlayerPrefs(this.mValue);
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
	public void UpdateLabel()
	{
		if (this.label != null)
		{
			if (this.mDoInit)
			{
				this.Init();
			}
			bool isSelected = this.isSelected;
			string value = this.value;
			bool flag = string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Input.compositionString);
			this.label.color = ((flag && !isSelected) ? this.mDefaultColor : this.activeTextColor);
			string text;
			if (flag)
			{
				text = (isSelected ? "" : this.mDefaultText);
				this.label.alignment = this.mAlignment;
			}
			else
			{
				if (this.inputType == UIInput.InputType.Password)
				{
					text = "";
					string str = "*";
					INGUIFont bitmapFont = this.label.bitmapFont;
					if (bitmapFont != null && bitmapFont.bmFont != null && bitmapFont.bmFont.GetGlyph(42) == null)
					{
						str = "x";
					}
					int i = 0;
					int length = value.Length;
					while (i < length)
					{
						text += str;
						i++;
					}
				}
				else
				{
					text = value;
				}
				int num = isSelected ? Mathf.Min(text.Length, this.cursorPosition) : 0;
				string str2 = text.Substring(0, num);
				if (isSelected)
				{
					str2 += Input.compositionString;
				}
				text = str2 + text.Substring(num, text.Length - num);
				if (isSelected && this.label.overflowMethod == UILabel.Overflow.ClampContent && this.label.maxLineCount == 1)
				{
					int num2 = this.label.CalculateOffsetToFit(text);
					if (num2 == 0)
					{
						UIInput.mDrawStart = 0;
						this.label.alignment = this.mAlignment;
					}
					else if (num < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num;
						this.label.alignment = NGUIText.Alignment.Left;
					}
					else if (num2 < UIInput.mDrawStart)
					{
						UIInput.mDrawStart = num2;
						this.label.alignment = NGUIText.Alignment.Left;
					}
					else
					{
						num2 = this.label.CalculateOffsetToFit(text.Substring(0, num));
						if (num2 > UIInput.mDrawStart)
						{
							UIInput.mDrawStart = num2;
							this.label.alignment = NGUIText.Alignment.Right;
						}
					}
					if (UIInput.mDrawStart != 0)
					{
						text = text.Substring(UIInput.mDrawStart, text.Length - UIInput.mDrawStart);
					}
				}
				else
				{
					UIInput.mDrawStart = 0;
					this.label.alignment = this.mAlignment;
				}
			}
			this.label.text = text;
			if (isSelected)
			{
				int num3 = this.mSelectionStart - UIInput.mDrawStart;
				int num4 = this.mSelectionEnd - UIInput.mDrawStart;
				if (this.mBlankTex == null)
				{
					this.mBlankTex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 2; k++)
						{
							this.mBlankTex.SetPixel(k, j, Color.white);
						}
					}
					this.mBlankTex.Apply();
				}
				if (num3 != num4)
				{
					if (this.mHighlight == null)
					{
						this.mHighlight = this.label.cachedGameObject.AddWidget(int.MaxValue);
						this.mHighlight.name = "Input Highlight";
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.fillGeometry = false;
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.SetAnchor(this.label.cachedTransform);
					}
					else
					{
						this.mHighlight.pivot = this.label.pivot;
						this.mHighlight.mainTexture = this.mBlankTex;
						this.mHighlight.MarkAsChanged();
						this.mHighlight.enabled = true;
					}
				}
				if (this.mCaret == null)
				{
					this.mCaret = this.label.cachedGameObject.AddWidget(int.MaxValue);
					this.mCaret.name = "Input Caret";
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.fillGeometry = false;
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.SetAnchor(this.label.cachedTransform);
				}
				else
				{
					this.mCaret.pivot = this.label.pivot;
					this.mCaret.mainTexture = this.mBlankTex;
					this.mCaret.MarkAsChanged();
					this.mCaret.enabled = true;
				}
				if (num3 != num4)
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, this.mHighlight.geometry, this.caretColor, this.selectionColor);
					this.mHighlight.enabled = this.mHighlight.geometry.hasVertices;
				}
				else
				{
					this.label.PrintOverlay(num3, num4, this.mCaret.geometry, null, this.caretColor, this.selectionColor);
					if (this.mHighlight != null)
					{
						this.mHighlight.enabled = false;
					}
				}
				this.mNextBlink = RealTime.time + 0.5f;
				this.mLastAlpha = this.label.finalAlpha;
				return;
			}
			this.Cleanup();
		}
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x0003FC14 File Offset: 0x0003DE14
	protected char Validate(string text, int pos, char ch)
	{
		if (this.validation == UIInput.Validation.None || !base.enabled)
		{
			return ch;
		}
		if (this.validation == UIInput.Validation.Integer)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Float)
		{
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
			if (ch == '-' && pos == 0 && !text.Contains("-"))
			{
				return ch;
			}
			if (ch == '.' && !text.Contains("."))
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Alphanumeric)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch;
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Username)
		{
			if (ch >= 'A' && ch <= 'Z')
			{
				return ch - 'A' + 'a';
			}
			if (ch >= 'a' && ch <= 'z')
			{
				return ch;
			}
			if (ch >= '0' && ch <= '9')
			{
				return ch;
			}
		}
		else if (this.validation == UIInput.Validation.Filename)
		{
			if (ch == ':')
			{
				return '\0';
			}
			if (ch == '/')
			{
				return '\0';
			}
			if (ch == '\\')
			{
				return '\0';
			}
			if (ch == '<')
			{
				return '\0';
			}
			if (ch == '>')
			{
				return '\0';
			}
			if (ch == '|')
			{
				return '\0';
			}
			if (ch == '^')
			{
				return '\0';
			}
			if (ch == '*')
			{
				return '\0';
			}
			if (ch == ';')
			{
				return '\0';
			}
			if (ch == '"')
			{
				return '\0';
			}
			if (ch == '`')
			{
				return '\0';
			}
			if (ch == '\t')
			{
				return '\0';
			}
			if (ch == '\n')
			{
				return '\0';
			}
			return ch;
		}
		else if (this.validation == UIInput.Validation.Name)
		{
			char c = (text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ';
			char c2 = (text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n';
			if (ch >= 'a' && ch <= 'z')
			{
				if (c == ' ')
				{
					return ch - 'a' + 'A';
				}
				return ch;
			}
			else if (ch >= 'A' && ch <= 'Z')
			{
				if (c != ' ' && c != '\'')
				{
					return ch - 'A' + 'a';
				}
				return ch;
			}
			else if (ch == '\'')
			{
				if (c != ' ' && c != '\'' && c2 != '\'' && !text.Contains("'"))
				{
					return ch;
				}
			}
			else if (ch == ' ' && c != ' ' && c != '\'' && c2 != ' ' && c2 != '\'')
			{
				return ch;
			}
		}
		return '\0';
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x0003FE4D File Offset: 0x0003E04D
	protected void ExecuteOnChange()
	{
		if (UIInput.current == null && EventDelegate.IsValid(this.onChange))
		{
			UIInput.current = this;
			EventDelegate.Execute(this.onChange);
			UIInput.current = null;
		}
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0003FE80 File Offset: 0x0003E080
	public void RemoveFocus()
	{
		this.isSelected = false;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0003FE89 File Offset: 0x0003E089
	public void SaveValue()
	{
		this.SaveToPlayerPrefs(this.mValue);
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0003FE98 File Offset: 0x0003E098
	public void LoadValue()
	{
		if (!string.IsNullOrEmpty(this.savedAs))
		{
			string text = this.mValue.Replace("\\n", "\n");
			this.mValue = "";
			this.value = (PlayerPrefs.HasKey(this.savedAs) ? PlayerPrefs.GetString(this.savedAs) : text);
		}
	}

	// Token: 0x040006C0 RID: 1728
	public static UIInput current;

	// Token: 0x040006C1 RID: 1729
	public static UIInput selection;

	// Token: 0x040006C2 RID: 1730
	public UILabel label;

	// Token: 0x040006C3 RID: 1731
	public UIInput.InputType inputType;

	// Token: 0x040006C4 RID: 1732
	public UIInput.OnReturnKey onReturnKey;

	// Token: 0x040006C5 RID: 1733
	public UIInput.KeyboardType keyboardType;

	// Token: 0x040006C6 RID: 1734
	public bool hideInput;

	// Token: 0x040006C7 RID: 1735
	[NonSerialized]
	public bool selectAllTextOnFocus = true;

	// Token: 0x040006C8 RID: 1736
	public bool submitOnUnselect;

	// Token: 0x040006C9 RID: 1737
	public UIInput.Validation validation;

	// Token: 0x040006CA RID: 1738
	public int characterLimit;

	// Token: 0x040006CB RID: 1739
	public string savedAs;

	// Token: 0x040006CC RID: 1740
	[HideInInspector]
	[SerializeField]
	private GameObject selectOnTab;

	// Token: 0x040006CD RID: 1741
	public Color activeTextColor = Color.white;

	// Token: 0x040006CE RID: 1742
	public Color caretColor = new Color(1f, 1f, 1f, 0.8f);

	// Token: 0x040006CF RID: 1743
	public Color selectionColor = new Color(1f, 0.8745098f, 0.5529412f, 0.5f);

	// Token: 0x040006D0 RID: 1744
	public List<EventDelegate> onSubmit = new List<EventDelegate>();

	// Token: 0x040006D1 RID: 1745
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x040006D2 RID: 1746
	public UIInput.OnValidate onValidate;

	// Token: 0x040006D3 RID: 1747
	[SerializeField]
	[HideInInspector]
	protected string mValue;

	// Token: 0x040006D4 RID: 1748
	[NonSerialized]
	protected string mDefaultText = "";

	// Token: 0x040006D5 RID: 1749
	[NonSerialized]
	protected Color mDefaultColor = Color.white;

	// Token: 0x040006D6 RID: 1750
	[NonSerialized]
	protected float mPosition;

	// Token: 0x040006D7 RID: 1751
	[NonSerialized]
	protected bool mDoInit = true;

	// Token: 0x040006D8 RID: 1752
	[NonSerialized]
	protected NGUIText.Alignment mAlignment = NGUIText.Alignment.Left;

	// Token: 0x040006D9 RID: 1753
	[NonSerialized]
	protected bool mLoadSavedValue = true;

	// Token: 0x040006DA RID: 1754
	protected static int mDrawStart = 0;

	// Token: 0x040006DB RID: 1755
	protected static string mLastIME = "";

	// Token: 0x040006DC RID: 1756
	[NonSerialized]
	protected int mSelectionStart;

	// Token: 0x040006DD RID: 1757
	[NonSerialized]
	protected int mSelectionEnd;

	// Token: 0x040006DE RID: 1758
	[NonSerialized]
	protected UITexture mHighlight;

	// Token: 0x040006DF RID: 1759
	[NonSerialized]
	protected UITexture mCaret;

	// Token: 0x040006E0 RID: 1760
	[NonSerialized]
	protected Texture2D mBlankTex;

	// Token: 0x040006E1 RID: 1761
	[NonSerialized]
	protected float mNextBlink;

	// Token: 0x040006E2 RID: 1762
	[NonSerialized]
	protected float mLastAlpha;

	// Token: 0x040006E3 RID: 1763
	[NonSerialized]
	protected string mCached = "";

	// Token: 0x040006E4 RID: 1764
	[NonSerialized]
	protected int mSelectMe = -1;

	// Token: 0x040006E5 RID: 1765
	[NonSerialized]
	protected int mSelectTime = -1;

	// Token: 0x040006E6 RID: 1766
	[NonSerialized]
	protected bool mStarted;

	// Token: 0x040006E7 RID: 1767
	[NonSerialized]
	private UIInputOnGUI mOnGUI;

	// Token: 0x040006E8 RID: 1768
	[NonSerialized]
	private UICamera mCam;

	// Token: 0x040006E9 RID: 1769
	[NonSerialized]
	private bool mEllipsis;

	// Token: 0x040006EA RID: 1770
	private static int mIgnoreKey = 0;

	// Token: 0x040006EB RID: 1771
	[NonSerialized]
	public Action onUpArrow;

	// Token: 0x040006EC RID: 1772
	[NonSerialized]
	public Action onDownArrow;

	// Token: 0x02000631 RID: 1585
	[DoNotObfuscateNGUI]
	public enum InputType
	{
		// Token: 0x04004EC6 RID: 20166
		Standard,
		// Token: 0x04004EC7 RID: 20167
		AutoCorrect,
		// Token: 0x04004EC8 RID: 20168
		Password
	}

	// Token: 0x02000632 RID: 1586
	[DoNotObfuscateNGUI]
	public enum Validation
	{
		// Token: 0x04004ECA RID: 20170
		None,
		// Token: 0x04004ECB RID: 20171
		Integer,
		// Token: 0x04004ECC RID: 20172
		Float,
		// Token: 0x04004ECD RID: 20173
		Alphanumeric,
		// Token: 0x04004ECE RID: 20174
		Username,
		// Token: 0x04004ECF RID: 20175
		Name,
		// Token: 0x04004ED0 RID: 20176
		Filename
	}

	// Token: 0x02000633 RID: 1587
	[DoNotObfuscateNGUI]
	public enum KeyboardType
	{
		// Token: 0x04004ED2 RID: 20178
		Default,
		// Token: 0x04004ED3 RID: 20179
		ASCIICapable,
		// Token: 0x04004ED4 RID: 20180
		NumbersAndPunctuation,
		// Token: 0x04004ED5 RID: 20181
		URL,
		// Token: 0x04004ED6 RID: 20182
		NumberPad,
		// Token: 0x04004ED7 RID: 20183
		PhonePad,
		// Token: 0x04004ED8 RID: 20184
		NamePhonePad,
		// Token: 0x04004ED9 RID: 20185
		EmailAddress
	}

	// Token: 0x02000634 RID: 1588
	[DoNotObfuscateNGUI]
	public enum OnReturnKey
	{
		// Token: 0x04004EDB RID: 20187
		Default,
		// Token: 0x04004EDC RID: 20188
		Submit,
		// Token: 0x04004EDD RID: 20189
		NewLine
	}

	// Token: 0x02000635 RID: 1589
	// (Invoke) Token: 0x06002615 RID: 9749
	public delegate char OnValidate(string text, int charIndex, char addedChar);
}
