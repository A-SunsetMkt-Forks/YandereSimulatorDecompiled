﻿using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public static class CollectibleGlobals
{
	// Token: 0x0600154B RID: 5451 RVA: 0x000D84C8 File Offset: 0x000D66C8
	public static bool GetHeadmasterTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeCollected_" + tapeID.ToString());
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x000D8500 File Offset: 0x000D6700
	public static void SetHeadmasterTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeCollected_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeCollected_" + text, value);
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x000D855C File Offset: 0x000D675C
	public static bool GetHeadmasterTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeListened_" + tapeID.ToString());
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x000D8594 File Offset: 0x000D6794
	public static void SetHeadmasterTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeListened_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeListened_" + text, value);
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x000D85F0 File Offset: 0x000D67F0
	public static int[] KeysOfHeadmasterTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeCollected_");
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x000D8620 File Offset: 0x000D6820
	public static int[] KeysOfHeadmasterTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeListened_");
	}

	// Token: 0x06001551 RID: 5457 RVA: 0x000D8650 File Offset: 0x000D6850
	public static bool GetBasementTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeCollected_" + tapeID.ToString());
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x000D8688 File Offset: 0x000D6888
	public static void SetBasementTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeCollected_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeCollected_" + text, value);
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x000D86E4 File Offset: 0x000D68E4
	public static int[] KeysOfBasementTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeCollected_");
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x000D8714 File Offset: 0x000D6914
	public static bool GetBasementTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeListened_" + tapeID.ToString());
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x000D874C File Offset: 0x000D694C
	public static void SetBasementTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeListened_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeListened_" + text, value);
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x000D87A8 File Offset: 0x000D69A8
	public static int[] KeysOfBasementTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeListened_");
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x000D87D8 File Offset: 0x000D69D8
	public static bool GetMangaCollected(int mangaID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_MangaCollected_" + mangaID.ToString());
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x000D8810 File Offset: 0x000D6A10
	public static void SetMangaCollected(int mangaID, bool value)
	{
		string text = mangaID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_MangaCollected_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_MangaCollected_" + text, value);
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x000D886C File Offset: 0x000D6A6C
	public static bool GetGiftPurchased(int giftID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_GiftPurchased_" + giftID.ToString());
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x000D88A4 File Offset: 0x000D6AA4
	public static void SetGiftPurchased(int giftID, bool value)
	{
		string text = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_GiftPurchased_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_GiftPurchased_" + text, value);
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x000D8900 File Offset: 0x000D6B00
	public static bool GetGiftGiven(int giftID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_GiftGiven_" + giftID.ToString());
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x000D8938 File Offset: 0x000D6B38
	public static void SetGiftGiven(int giftID, bool value)
	{
		string text = giftID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_GiftGiven_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_GiftGiven_" + text, value);
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x0600155D RID: 5469 RVA: 0x000D8994 File Offset: 0x000D6B94
	// (set) Token: 0x0600155E RID: 5470 RVA: 0x000D89C4 File Offset: 0x000D6BC4
	public static int MatchmakingGifts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_MatchmakingGifts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_MatchmakingGifts", value);
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x0600155F RID: 5471 RVA: 0x000D89F4 File Offset: 0x000D6BF4
	// (set) Token: 0x06001560 RID: 5472 RVA: 0x000D8A24 File Offset: 0x000D6C24
	public static int SenpaiGifts
	{
		get
		{
			return PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_SenpaiGifts");
		}
		set
		{
			PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_SenpaiGifts", value);
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000D8A54 File Offset: 0x000D6C54
	public static bool GetPantyPurchased(int giftID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_PantyPurchased_" + giftID.ToString());
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000D8A8C File Offset: 0x000D6C8C
	public static void SetPantyPurchased(int pantyID, bool value)
	{
		string text = pantyID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_PantyPurchased_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_PantyPurchased_" + text, value);
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000D8AE8 File Offset: 0x000D6CE8
	public static bool GetAdvicePurchased(int giftID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_AdvicePurchased_" + giftID.ToString());
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000D8B20 File Offset: 0x000D6D20
	public static void SetAdvicePurchased(int adviceID, bool value)
	{
		string text = adviceID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_AdvicePurchased_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_AdvicePurchased_" + text, value);
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x000D8B7C File Offset: 0x000D6D7C
	public static int[] KeysOfMangaCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_MangaCollected_");
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x000D8BAC File Offset: 0x000D6DAC
	public static int[] KeysOfGiftPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_GiftPurchased_");
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x000D8BDC File Offset: 0x000D6DDC
	public static int[] KeysOfGiftGiven()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_GiftGiven_");
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000D8C0C File Offset: 0x000D6E0C
	public static int[] KeysOfPantyPurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_PantyPurchased_");
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x000D8C3C File Offset: 0x000D6E3C
	public static int[] KeysOfAdvicePurchased()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_AdvicePurchased_");
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x000D8C6C File Offset: 0x000D6E6C
	public static bool GetTapeCollected(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_TapeCollected_" + tapeID.ToString());
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x000D8CA4 File Offset: 0x000D6EA4
	public static void SetTapeCollected(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_TapeCollected_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_TapeCollected_" + text, value);
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x000D8D00 File Offset: 0x000D6F00
	public static int[] KeysOfTapeCollected()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_TapeCollected_");
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x000D8D30 File Offset: 0x000D6F30
	public static bool GetTapeListened(int tapeID)
	{
		return GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_TapeListened_" + tapeID.ToString());
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x000D8D68 File Offset: 0x000D6F68
	public static void SetTapeListened(int tapeID, bool value)
	{
		string text = tapeID.ToString();
		KeysHelper.AddIfMissing("Profile_" + GameGlobals.Profile.ToString() + "_TapeListened_", text);
		GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_TapeListened_" + text, value);
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x000D8DC4 File Offset: 0x000D6FC4
	public static int[] KeysOfTapeListened()
	{
		return KeysHelper.GetIntegerKeys("Profile_" + GameGlobals.Profile.ToString() + "_TapeListened_");
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x000D8DF4 File Offset: 0x000D6FF4
	public static void DeleteAll()
	{
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeCollected_", CollectibleGlobals.KeysOfHeadmasterTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_HeadmasterTapeListened_", CollectibleGlobals.KeysOfHeadmasterTapeListened());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeCollected_", CollectibleGlobals.KeysOfBasementTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_BasementTapeListened_", CollectibleGlobals.KeysOfBasementTapeListened());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_MangaCollected_", CollectibleGlobals.KeysOfMangaCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_AdvicePurchased_", CollectibleGlobals.KeysOfAdvicePurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_PantyPurchased_", CollectibleGlobals.KeysOfPantyPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_GiftPurchased_", CollectibleGlobals.KeysOfGiftPurchased());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_GiftGiven_", CollectibleGlobals.KeysOfGiftGiven());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_TapeCollected_", CollectibleGlobals.KeysOfTapeCollected());
		Globals.DeleteCollection("Profile_" + GameGlobals.Profile.ToString() + "_TapeListened_", CollectibleGlobals.KeysOfTapeListened());
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_MatchmakingGifts");
		Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_SenpaiGifts");
	}

	// Token: 0x0400219A RID: 8602
	private const string Str_HeadmasterTapeCollected = "HeadmasterTapeCollected_";

	// Token: 0x0400219B RID: 8603
	private const string Str_HeadmasterTapeListened = "HeadmasterTapeListened_";

	// Token: 0x0400219C RID: 8604
	private const string Str_BasementTapeCollected = "BasementTapeCollected_";

	// Token: 0x0400219D RID: 8605
	private const string Str_BasementTapeListened = "BasementTapeListened_";

	// Token: 0x0400219E RID: 8606
	private const string Str_MangaCollected = "MangaCollected_";

	// Token: 0x0400219F RID: 8607
	private const string Str_GiftPurchased = "GiftPurchased_";

	// Token: 0x040021A0 RID: 8608
	private const string Str_GiftGiven = "GiftGiven_";

	// Token: 0x040021A1 RID: 8609
	private const string Str_MatchmakingGifts = "MatchmakingGifts";

	// Token: 0x040021A2 RID: 8610
	private const string Str_SenpaiGifts = "SenpaiGifts";

	// Token: 0x040021A3 RID: 8611
	private const string Str_PantyPurchased = "PantyPurchased_";

	// Token: 0x040021A4 RID: 8612
	private const string Str_AdvicePurchased = "AdvicePurchased_";

	// Token: 0x040021A5 RID: 8613
	private const string Str_TapeCollected = "TapeCollected_";

	// Token: 0x040021A6 RID: 8614
	private const string Str_TapeListened = "TapeListened_";
}
