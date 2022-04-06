﻿using System;
using UnityEngine;

// Token: 0x020002ED RID: 749
public static class ClassGlobals
{
	// Token: 0x17000370 RID: 880
	// (get) Token: 0x0600153C RID: 5436 RVA: 0x000D969C File Offset: 0x000D789C
	// (set) Token: 0x0600153D RID: 5437 RVA: 0x000D96CC File Offset: 0x000D78CC
	public static int Biology
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_Biology");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_Biology", value);
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x0600153E RID: 5438 RVA: 0x000D96FC File Offset: 0x000D78FC
	// (set) Token: 0x0600153F RID: 5439 RVA: 0x000D972C File Offset: 0x000D792C
	public static int BiologyBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_BiologyBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_BiologyBonus", value);
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x06001540 RID: 5440 RVA: 0x000D975C File Offset: 0x000D795C
	// (set) Token: 0x06001541 RID: 5441 RVA: 0x000D978C File Offset: 0x000D798C
	public static int BiologyGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_BiologyGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_BiologyGrade", value);
		}
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06001542 RID: 5442 RVA: 0x000D97BC File Offset: 0x000D79BC
	// (set) Token: 0x06001543 RID: 5443 RVA: 0x000D97EC File Offset: 0x000D79EC
	public static int Chemistry
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_Chemistry");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_Chemistry", value);
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06001544 RID: 5444 RVA: 0x000D981C File Offset: 0x000D7A1C
	// (set) Token: 0x06001545 RID: 5445 RVA: 0x000D984C File Offset: 0x000D7A4C
	public static int ChemistryBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_ChemistryBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_ChemistryBonus", value);
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06001546 RID: 5446 RVA: 0x000D987C File Offset: 0x000D7A7C
	// (set) Token: 0x06001547 RID: 5447 RVA: 0x000D98AC File Offset: 0x000D7AAC
	public static int ChemistryGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_ChemistryGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_ChemistryGrade", value);
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06001548 RID: 5448 RVA: 0x000D98DC File Offset: 0x000D7ADC
	// (set) Token: 0x06001549 RID: 5449 RVA: 0x000D990C File Offset: 0x000D7B0C
	public static int Language
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_Language");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_Language", value);
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x0600154A RID: 5450 RVA: 0x000D993C File Offset: 0x000D7B3C
	// (set) Token: 0x0600154B RID: 5451 RVA: 0x000D996C File Offset: 0x000D7B6C
	public static int LanguageBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_LanguageBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_LanguageBonus", value);
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x0600154C RID: 5452 RVA: 0x000D999C File Offset: 0x000D7B9C
	// (set) Token: 0x0600154D RID: 5453 RVA: 0x000D99CC File Offset: 0x000D7BCC
	public static int LanguageGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_LanguageGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_LanguageGrade", value);
		}
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x0600154E RID: 5454 RVA: 0x000D99FC File Offset: 0x000D7BFC
	// (set) Token: 0x0600154F RID: 5455 RVA: 0x000D9A2C File Offset: 0x000D7C2C
	public static int Physical
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_Physical");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_Physical", value);
		}
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06001550 RID: 5456 RVA: 0x000D9A5C File Offset: 0x000D7C5C
	// (set) Token: 0x06001551 RID: 5457 RVA: 0x000D9A8C File Offset: 0x000D7C8C
	public static int PhysicalBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_PhysicalBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_PhysicalBonus", value);
		}
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06001552 RID: 5458 RVA: 0x000D9ABC File Offset: 0x000D7CBC
	// (set) Token: 0x06001553 RID: 5459 RVA: 0x000D9AEC File Offset: 0x000D7CEC
	public static int PhysicalGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_PhysicalGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_PhysicalGrade", value);
		}
	}

	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06001554 RID: 5460 RVA: 0x000D9B1C File Offset: 0x000D7D1C
	// (set) Token: 0x06001555 RID: 5461 RVA: 0x000D9B4C File Offset: 0x000D7D4C
	public static int Psychology
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_Psychology");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_Psychology", value);
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06001556 RID: 5462 RVA: 0x000D9B7C File Offset: 0x000D7D7C
	// (set) Token: 0x06001557 RID: 5463 RVA: 0x000D9BAC File Offset: 0x000D7DAC
	public static int PsychologyBonus
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_PsychologyBonus");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_PsychologyBonus", value);
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06001558 RID: 5464 RVA: 0x000D9BDC File Offset: 0x000D7DDC
	// (set) Token: 0x06001559 RID: 5465 RVA: 0x000D9C0C File Offset: 0x000D7E0C
	public static int PsychologyGrade
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_PsychologyGrade");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_PsychologyGrade", value);
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x0600155A RID: 5466 RVA: 0x000D9C3C File Offset: 0x000D7E3C
	// (set) Token: 0x0600155B RID: 5467 RVA: 0x000D9C6C File Offset: 0x000D7E6C
	public static int BonusStudyPoints
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_BonusStudyPoints");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_BonusStudyPoints", value);
		}
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x000D9C9C File Offset: 0x000D7E9C
	public static void DeleteAll()
	{
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_Biology");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_BiologyBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_BiologyGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_Chemistry");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_ChemistryBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_ChemistryGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_Language");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_LanguageBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_LanguageGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_Physical");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_PhysicalBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_PhysicalGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_Psychology");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_PsychologyBonus");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_PsychologyGrade");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_BonusStudyPoints");
	}

	// Token: 0x040021D9 RID: 8665
	private const string Str_Biology = "Biology";

	// Token: 0x040021DA RID: 8666
	private const string Str_BiologyBonus = "BiologyBonus";

	// Token: 0x040021DB RID: 8667
	private const string Str_BiologyGrade = "BiologyGrade";

	// Token: 0x040021DC RID: 8668
	private const string Str_Chemistry = "Chemistry";

	// Token: 0x040021DD RID: 8669
	private const string Str_ChemistryBonus = "ChemistryBonus";

	// Token: 0x040021DE RID: 8670
	private const string Str_ChemistryGrade = "ChemistryGrade";

	// Token: 0x040021DF RID: 8671
	private const string Str_Language = "Language";

	// Token: 0x040021E0 RID: 8672
	private const string Str_LanguageBonus = "LanguageBonus";

	// Token: 0x040021E1 RID: 8673
	private const string Str_LanguageGrade = "LanguageGrade";

	// Token: 0x040021E2 RID: 8674
	private const string Str_Physical = "Physical";

	// Token: 0x040021E3 RID: 8675
	private const string Str_PhysicalBonus = "PhysicalBonus";

	// Token: 0x040021E4 RID: 8676
	private const string Str_PhysicalGrade = "PhysicalGrade";

	// Token: 0x040021E5 RID: 8677
	private const string Str_Psychology = "Psychology";

	// Token: 0x040021E6 RID: 8678
	private const string Str_PsychologyBonus = "PsychologyBonus";

	// Token: 0x040021E7 RID: 8679
	private const string Str_PsychologyGrade = "PsychologyGrade";

	// Token: 0x040021E8 RID: 8680
	private const string Str_BonusStudyPoints = "BonusStudyPoints";
}
