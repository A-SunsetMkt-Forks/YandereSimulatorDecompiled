﻿using System;

// Token: 0x020003FD RID: 1021
[Serializable]
public class ConversationSaveData
{
	// Token: 0x06001C1D RID: 7197 RVA: 0x00148A24 File Offset: 0x00146C24
	public static ConversationSaveData ReadFromGlobals()
	{
		ConversationSaveData conversationSaveData = new ConversationSaveData();
		foreach (int num in ConversationGlobals.KeysOfTopicDiscovered())
		{
			if (ConversationGlobals.GetTopicDiscovered(num))
			{
				conversationSaveData.topicDiscovered.Add(num);
			}
		}
		foreach (IntAndIntPair intAndIntPair in ConversationGlobals.KeysOfTopicLearnedByStudent())
		{
			if (ConversationGlobals.GetTopicLearnedByStudent(intAndIntPair.first, intAndIntPair.second))
			{
				conversationSaveData.topicLearnedByStudent.Add(intAndIntPair);
			}
		}
		return conversationSaveData;
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x00148AA4 File Offset: 0x00146CA4
	public static void WriteToGlobals(ConversationSaveData data)
	{
		foreach (int topicID in data.topicDiscovered)
		{
			ConversationGlobals.SetTopicDiscovered(topicID, true);
		}
		foreach (IntAndIntPair intAndIntPair in data.topicLearnedByStudent)
		{
			ConversationGlobals.SetTopicLearnedByStudent(intAndIntPair.first, intAndIntPair.second, true);
		}
	}

	// Token: 0x0400317F RID: 12671
	public IntHashSet topicDiscovered = new IntHashSet();

	// Token: 0x04003180 RID: 12672
	public IntAndIntPairHashSet topicLearnedByStudent = new IntAndIntPairHashSet();
}
