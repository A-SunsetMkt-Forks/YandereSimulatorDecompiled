using UnityEngine;

public class ReputationSetterScript : MonoBehaviour
{
	public StudentManagerScript StudentManager;

	public float[] Likes;

	public float[] Respects;

	public float[] Fears;

	public void InitializeReputations()
	{
		if (!GameGlobals.Eighties)
		{
			StudentGlobals.SetReputationTriangle(1, new Vector3(0f, 0f, 0f));
			StudentGlobals.SetReputationTriangle(2, new Vector3(70f, -10f, 10f));
			StudentGlobals.SetReputationTriangle(3, new Vector3(50f, -10f, 30f));
			StudentGlobals.SetReputationTriangle(4, new Vector3(0f, 10f, 0f));
			StudentGlobals.SetReputationTriangle(5, new Vector3(-50f, -30f, 10f));
			StudentGlobals.SetReputationTriangle(6, new Vector3(30f, 0f, 0f));
			StudentGlobals.SetReputationTriangle(7, new Vector3(-10f, -10f, -10f));
			StudentGlobals.SetReputationTriangle(8, new Vector3(0f, 10f, -30f));
			StudentGlobals.SetReputationTriangle(9, new Vector3(0f, 0f, 0f));
			StudentGlobals.SetReputationTriangle(10, new Vector3(30f, 15f, 5f));
			StudentGlobals.SetReputationTriangle(11, new Vector3(60f, 30f, 10f));
			StudentGlobals.SetReputationTriangle(12, new Vector3(100f, 100f, -10f));
			StudentGlobals.SetReputationTriangle(13, new Vector3(-10f, 100f, 100f));
			StudentGlobals.SetReputationTriangle(14, new Vector3(0f, 100f, -10f));
			StudentGlobals.SetReputationTriangle(15, new Vector3(100f, 100f, 0f));
			StudentGlobals.SetReputationTriangle(16, new Vector3(0f, -10f, 0f));
			StudentGlobals.SetReputationTriangle(17, new Vector3(-10f, -10f, 50f));
			StudentGlobals.SetReputationTriangle(18, new Vector3(-100f, -100f, 100f));
			StudentGlobals.SetReputationTriangle(19, new Vector3(10f, 0f, 0f));
			StudentGlobals.SetReputationTriangle(20, new Vector3(100f, 100f, 100f));
			StudentGlobals.SetReputationTriangle(21, new Vector3(50f, 100f, 0f));
			StudentGlobals.SetReputationTriangle(22, new Vector3(30f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(23, new Vector3(50f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(24, new Vector3(30f, 50f, 10f));
			StudentGlobals.SetReputationTriangle(25, new Vector3(70f, 50f, -30f));
			StudentGlobals.SetReputationTriangle(26, new Vector3(-10f, 100f, 0f));
			StudentGlobals.SetReputationTriangle(27, new Vector3(0f, 70f, 0f));
			StudentGlobals.SetReputationTriangle(28, new Vector3(0f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(29, new Vector3(-10f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(30, new Vector3(30f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(31, new Vector3(-70f, 100f, 10f));
			StudentGlobals.SetReputationTriangle(32, new Vector3(-70f, -10f, 10f));
			StudentGlobals.SetReputationTriangle(33, new Vector3(-70f, -10f, 10f));
			StudentGlobals.SetReputationTriangle(34, new Vector3(-70f, -10f, 10f));
			StudentGlobals.SetReputationTriangle(35, new Vector3(-70f, -10f, 10f));
			StudentGlobals.SetReputationTriangle(36, new Vector3(-70f, 100f, 0f));
			StudentGlobals.SetReputationTriangle(37, new Vector3(0f, -10f, 0f));
			StudentGlobals.SetReputationTriangle(38, new Vector3(50f, 0f, 0f));
			StudentGlobals.SetReputationTriangle(39, new Vector3(-50f, -10f, 0f));
			StudentGlobals.SetReputationTriangle(40, new Vector3(70f, -30f, 10f));
			StudentGlobals.SetReputationTriangle(41, new Vector3(0f, 100f, 0f));
			StudentGlobals.SetReputationTriangle(42, new Vector3(-50f, -30f, 30f));
			StudentGlobals.SetReputationTriangle(43, new Vector3(-10f, -10f, 0f));
			StudentGlobals.SetReputationTriangle(44, new Vector3(-10f, 0f, 0f));
			StudentGlobals.SetReputationTriangle(45, new Vector3(0f, -10f, 0f));
			StudentGlobals.SetReputationTriangle(46, new Vector3(100f, 100f, 100f));
			StudentGlobals.SetReputationTriangle(47, new Vector3(10f, 30f, 10f));
			StudentGlobals.SetReputationTriangle(48, new Vector3(30f, 10f, 10f));
			StudentGlobals.SetReputationTriangle(49, new Vector3(30f, 30f, 10f));
			StudentGlobals.SetReputationTriangle(50, new Vector3(30f, 10f, 10f));
			StudentGlobals.SetReputationTriangle(51, new Vector3(10f, 100f, 0f));
			StudentGlobals.SetReputationTriangle(52, new Vector3(30f, 70f, 0f));
			StudentGlobals.SetReputationTriangle(53, new Vector3(50f, 10f, 0f));
			StudentGlobals.SetReputationTriangle(54, new Vector3(50f, 50f, -10f));
			StudentGlobals.SetReputationTriangle(55, new Vector3(30f, 30f, 0f));
			StudentGlobals.SetReputationTriangle(56, new Vector3(70f, 100f, 0f));
			StudentGlobals.SetReputationTriangle(57, new Vector3(70f, -30f, 0f));
			StudentGlobals.SetReputationTriangle(58, new Vector3(70f, -30f, 0f));
			StudentGlobals.SetReputationTriangle(59, new Vector3(50f, -10f, 0f));
			StudentGlobals.SetReputationTriangle(60, new Vector3(-10f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(61, new Vector3(-50f, 100f, 100f));
			StudentGlobals.SetReputationTriangle(62, new Vector3(0f, 70f, 10f));
			StudentGlobals.SetReputationTriangle(63, new Vector3(0f, 30f, 50f));
			StudentGlobals.SetReputationTriangle(64, new Vector3(-10f, 30f, 50f));
			StudentGlobals.SetReputationTriangle(65, new Vector3(-10f, 30f, 50f));
			StudentGlobals.SetReputationTriangle(66, new Vector3(-50f, 100f, 50f));
			StudentGlobals.SetReputationTriangle(67, new Vector3(30f, 70f, 0f));
			StudentGlobals.SetReputationTriangle(68, new Vector3(-50f, -50f, 50f));
			StudentGlobals.SetReputationTriangle(69, new Vector3(30f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(70, new Vector3(50f, 30f, 0f));
			StudentGlobals.SetReputationTriangle(71, new Vector3(100f, 100f, -100f));
			StudentGlobals.SetReputationTriangle(72, new Vector3(50f, 30f, 0f));
			StudentGlobals.SetReputationTriangle(73, new Vector3(100f, 100f, -100f));
			StudentGlobals.SetReputationTriangle(74, new Vector3(70f, 50f, -50f));
			StudentGlobals.SetReputationTriangle(75, new Vector3(10f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(76, new Vector3(-100f, -100f, 100f));
			StudentGlobals.SetReputationTriangle(77, new Vector3(-100f, -100f, 100f));
			StudentGlobals.SetReputationTriangle(78, new Vector3(-100f, -100f, 100f));
			StudentGlobals.SetReputationTriangle(79, new Vector3(-100f, -100f, 100f));
			StudentGlobals.SetReputationTriangle(80, new Vector3(-100f, -100f, 100f));
			StudentGlobals.SetReputationTriangle(81, new Vector3(50f, -10f, 50f));
			StudentGlobals.SetReputationTriangle(82, new Vector3(50f, -10f, 50f));
			StudentGlobals.SetReputationTriangle(83, new Vector3(50f, -10f, 50f));
			StudentGlobals.SetReputationTriangle(84, new Vector3(50f, -10f, 50f));
			StudentGlobals.SetReputationTriangle(85, new Vector3(50f, -10f, 50f));
			StudentGlobals.SetReputationTriangle(86, new Vector3(30f, 100f, 70f));
			StudentGlobals.SetReputationTriangle(87, new Vector3(30f, -10f, 100f));
			StudentGlobals.SetReputationTriangle(88, new Vector3(100f, 30f, 50f));
			StudentGlobals.SetReputationTriangle(89, new Vector3(-10f, 30f, 100f));
			StudentGlobals.SetReputationTriangle(90, new Vector3(10f, 100f, 10f));
			StudentGlobals.SetReputationTriangle(91, new Vector3(0f, 50f, 100f));
			StudentGlobals.SetReputationTriangle(92, new Vector3(0f, 70f, 50f));
			StudentGlobals.SetReputationTriangle(93, new Vector3(0f, 100f, 50f));
			StudentGlobals.SetReputationTriangle(94, new Vector3(0f, 70f, 100f));
			StudentGlobals.SetReputationTriangle(95, new Vector3(0f, 50f, 70f));
			StudentGlobals.SetReputationTriangle(96, new Vector3(0f, 100f, 50f));
			StudentGlobals.SetReputationTriangle(97, new Vector3(50f, 100f, 30f));
			StudentGlobals.SetReputationTriangle(98, new Vector3(0f, 100f, 100f));
			StudentGlobals.SetReputationTriangle(99, new Vector3(-50f, 50f, 100f));
			StudentGlobals.SetReputationTriangle(99, new Vector3(-100f, -100f, 100f));
		}
		else if (GameGlobals.CustomMode)
		{
			for (int i = 1; i < 101; i++)
			{
				StudentGlobals.SetReputationTriangle(i, new Vector3(StudentManager.JSON.Misc.Likes[i], StudentManager.JSON.Misc.Respects[i], StudentManager.JSON.Misc.Fears[i]));
			}
		}
		else
		{
			StudentGlobals.SetReputationTriangle(1, new Vector3(100f, 100f, 0f));
			StudentGlobals.SetReputationTriangle(2, new Vector3(-20f, 10f, 60f));
			StudentGlobals.SetReputationTriangle(3, new Vector3(80f, 0f, -20f));
			StudentGlobals.SetReputationTriangle(4, new Vector3(100f, 20f, 0f));
			StudentGlobals.SetReputationTriangle(5, new Vector3(20f, 80f, 10f));
			StudentGlobals.SetReputationTriangle(6, new Vector3(10f, -10f, 30f));
			StudentGlobals.SetReputationTriangle(7, new Vector3(60f, -20f, 15f));
			StudentGlobals.SetReputationTriangle(8, new Vector3(80f, 10f, -10f));
			StudentGlobals.SetReputationTriangle(9, new Vector3(20f, 50f, 0f));
			StudentGlobals.SetReputationTriangle(10, new Vector3(50f, 75f, -20f));
			StudentGlobals.SetReputationTriangle(11, new Vector3(30f, 0f, 0f));
			StudentGlobals.SetReputationTriangle(12, new Vector3(30f, 0f, 30f));
			StudentGlobals.SetReputationTriangle(13, new Vector3(0f, 45f, 45f));
			StudentGlobals.SetReputationTriangle(14, new Vector3(60f, 60f, 0f));
			StudentGlobals.SetReputationTriangle(15, new Vector3(0f, 75f, 75f));
			StudentGlobals.SetReputationTriangle(16, new Vector3(90f, 90f, 0f));
			StudentGlobals.SetReputationTriangle(17, new Vector3(100f, 100f, 10f));
			StudentGlobals.SetReputationTriangle(18, new Vector3(100f, 100f, 40f));
			StudentGlobals.SetReputationTriangle(19, new Vector3(100f, 100f, 70f));
			StudentGlobals.SetReputationTriangle(20, new Vector3(100f, 100f, 100f));
			StudentGlobals.SetReputationTriangle(21, new Vector3(45f, 20f, 0f));
			StudentGlobals.SetReputationTriangle(22, new Vector3(40f, 20f, 0f));
			StudentGlobals.SetReputationTriangle(23, new Vector3(60f, -20f, -10f));
			StudentGlobals.SetReputationTriangle(24, new Vector3(75f, 30f, 0f));
			StudentGlobals.SetReputationTriangle(25, new Vector3(-10f, -10f, 30f));
			StudentGlobals.SetReputationTriangle(26, new Vector3(50f, 80f, 20f));
			StudentGlobals.SetReputationTriangle(27, new Vector3(40f, 10f, 60f));
			StudentGlobals.SetReputationTriangle(28, new Vector3(45f, -10f, -10f));
			StudentGlobals.SetReputationTriangle(29, new Vector3(50f, -5f, 50f));
			StudentGlobals.SetReputationTriangle(30, new Vector3(-25f, 50f, 50f));
			StudentGlobals.SetReputationTriangle(31, new Vector3(-60f, -60f, 50f));
			StudentGlobals.SetReputationTriangle(32, new Vector3(-10f, -30f, 20f));
			StudentGlobals.SetReputationTriangle(33, new Vector3(-20f, -70f, 40f));
			StudentGlobals.SetReputationTriangle(34, new Vector3(-30f, -80f, 50f));
			StudentGlobals.SetReputationTriangle(35, new Vector3(-10f, -60f, 30f));
			StudentGlobals.SetReputationTriangle(36, new Vector3(40f, 50f, 10f));
			StudentGlobals.SetReputationTriangle(37, new Vector3(50f, 30f, -15f));
			StudentGlobals.SetReputationTriangle(38, new Vector3(80f, 30f, -20f));
			StudentGlobals.SetReputationTriangle(39, new Vector3(5f, 15f, 50f));
			StudentGlobals.SetReputationTriangle(40, new Vector3(40f, 55f, 0f));
			StudentGlobals.SetReputationTriangle(41, new Vector3(40f, 85f, 0f));
			StudentGlobals.SetReputationTriangle(42, new Vector3(30f, 25f, 5f));
			StudentGlobals.SetReputationTriangle(43, new Vector3(50f, -5f, -10f));
			StudentGlobals.SetReputationTriangle(44, new Vector3(15f, 0f, -10f));
			StudentGlobals.SetReputationTriangle(45, new Vector3(50f, 30f, 0f));
			StudentGlobals.SetReputationTriangle(46, new Vector3(60f, 40f, 40f));
			StudentGlobals.SetReputationTriangle(47, new Vector3(60f, 20f, 40f));
			StudentGlobals.SetReputationTriangle(48, new Vector3(-50f, 0f, 75f));
			StudentGlobals.SetReputationTriangle(49, new Vector3(20f, 20f, 30f));
			StudentGlobals.SetReputationTriangle(50, new Vector3(25f, 35f, 25f));
			StudentGlobals.SetReputationTriangle(51, new Vector3(10f, 40f, 10f));
			StudentGlobals.SetReputationTriangle(52, new Vector3(30f, 55f, 0f));
			StudentGlobals.SetReputationTriangle(53, new Vector3(45f, 10f, 0f));
			StudentGlobals.SetReputationTriangle(54, new Vector3(55f, 20f, -10f));
			StudentGlobals.SetReputationTriangle(55, new Vector3(20f, 60f, 20f));
			StudentGlobals.SetReputationTriangle(56, new Vector3(30f, 25f, 0f));
			StudentGlobals.SetReputationTriangle(57, new Vector3(20f, 70f, -10f));
			StudentGlobals.SetReputationTriangle(58, new Vector3(-5f, -10f, 0f));
			StudentGlobals.SetReputationTriangle(59, new Vector3(15f, -10f, 0f));
			StudentGlobals.SetReputationTriangle(60, new Vector3(20f, 15f, 0f));
			StudentGlobals.SetReputationTriangle(61, new Vector3(-20f, 90f, 20f));
			StudentGlobals.SetReputationTriangle(62, new Vector3(30f, 80f, -20f));
			StudentGlobals.SetReputationTriangle(63, new Vector3(25f, 60f, -10f));
			StudentGlobals.SetReputationTriangle(64, new Vector3(5f, 20f, -10f));
			StudentGlobals.SetReputationTriangle(65, new Vector3(15f, 30f, 0f));
			StudentGlobals.SetReputationTriangle(66, new Vector3(30f, 30f, 50f));
			StudentGlobals.SetReputationTriangle(67, new Vector3(100f, -10f, 15f));
			StudentGlobals.SetReputationTriangle(68, new Vector3(30f, 20f, 0f));
			StudentGlobals.SetReputationTriangle(69, new Vector3(15f, 20f, 20f));
			StudentGlobals.SetReputationTriangle(70, new Vector3(20f, 10f, 10f));
			StudentGlobals.SetReputationTriangle(71, new Vector3(45f, 85f, 0f));
			StudentGlobals.SetReputationTriangle(72, new Vector3(80f, 80f, 10f));
			StudentGlobals.SetReputationTriangle(73, new Vector3(20f, 20f, -10f));
			StudentGlobals.SetReputationTriangle(74, new Vector3(40f, -10f, -10f));
			StudentGlobals.SetReputationTriangle(75, new Vector3(100f, 100f, -20f));
			StudentGlobals.SetReputationTriangle(76, new Vector3(-100f, -100f, 100f));
			StudentGlobals.SetReputationTriangle(77, new Vector3(-80f, -100f, 70f));
			StudentGlobals.SetReputationTriangle(78, new Vector3(-90f, -90f, 70f));
			StudentGlobals.SetReputationTriangle(79, new Vector3(-100f, -100f, 80f));
			StudentGlobals.SetReputationTriangle(80, new Vector3(-90f, -90f, 80f));
			StudentGlobals.SetReputationTriangle(81, new Vector3(-90f, -90f, 100f));
			StudentGlobals.SetReputationTriangle(82, new Vector3(-100f, -80f, 90f));
			StudentGlobals.SetReputationTriangle(83, new Vector3(-70f, -100f, 70f));
			StudentGlobals.SetReputationTriangle(84, new Vector3(-50f, -100f, 60f));
			StudentGlobals.SetReputationTriangle(85, new Vector3(-100f, -80f, 80f));
			StudentGlobals.SetReputationTriangle(86, new Vector3(80f, 100f, 80f));
			StudentGlobals.SetReputationTriangle(87, new Vector3(75f, 100f, 75f));
			StudentGlobals.SetReputationTriangle(88, new Vector3(100f, 80f, 50f));
			StudentGlobals.SetReputationTriangle(89, new Vector3(60f, 60f, 100f));
			StudentGlobals.SetReputationTriangle(90, new Vector3(100f, 75f, 50f));
			StudentGlobals.SetReputationTriangle(91, new Vector3(80f, 80f, 50f));
			StudentGlobals.SetReputationTriangle(92, new Vector3(100f, 80f, 60f));
			StudentGlobals.SetReputationTriangle(93, new Vector3(40f, 100f, 90f));
			StudentGlobals.SetReputationTriangle(94, new Vector3(80f, 60f, 70f));
			StudentGlobals.SetReputationTriangle(95, new Vector3(20f, 100f, 100f));
			StudentGlobals.SetReputationTriangle(96, new Vector3(50f, 90f, 80f));
			StudentGlobals.SetReputationTriangle(97, new Vector3(100f, 80f, 70f));
			StudentGlobals.SetReputationTriangle(98, new Vector3(50f, 100f, 100f));
			StudentGlobals.SetReputationTriangle(99, new Vector3(100f, 100f, 60f));
			StudentGlobals.SetReputationTriangle(100, new Vector3(100f, 100f, 100f));
		}
	}
}
