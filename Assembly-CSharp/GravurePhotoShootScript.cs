using UnityEngine;

public class GravurePhotoShootScript : MonoBehaviour
{
	public TaskMinigameScript TaskMinigame;

	public GameObject CameraFlashes;

	public PromptScript Prompt;

	public bool Complete;

	private void Update()
	{
		if (Prompt.Circle[0].fillAmount == 0f)
		{
			Prompt.Circle[0].fillAmount = 1f;
			bool flag = false;
			if (Prompt.Yandere.EightiesBikiniAttacher.newRenderer != null && Prompt.Yandere.EightiesBikiniAttacher.newRenderer.enabled)
			{
				flag = true;
			}
			if (!flag)
			{
				Prompt.Yandere.NotificationManager.CustomText = "Go change into a bikini first!";
				Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				return;
			}
			if (Prompt.Yandere.StudentManager.Students[19] != null && Vector3.Distance(Prompt.Yandere.StudentManager.Students[19].transform.position, Prompt.Yandere.transform.position) > 3f)
			{
				Prompt.Yandere.NotificationManager.CustomText = "Chigusa is not here!";
				Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				return;
			}
			if ((Prompt.Yandere.StudentManager.Students[19] != null && Prompt.Yandere.StudentManager.Students[19].Electrified) || (Prompt.Yandere.StudentManager.Students[19] != null && Prompt.Yandere.StudentManager.Students[19].Electrocuted) || (Prompt.Yandere.StudentManager.Students[19] != null && !Prompt.Yandere.StudentManager.Students[19].Alive))
			{
				Prompt.Yandere.NotificationManager.CustomText = "Chigusa is dead!";
				Prompt.Yandere.NotificationManager.DisplayNotification(NotificationType.Custom);
				return;
			}
			Prompt.Yandere.CharacterAnimation.CrossFade(Prompt.Yandere.IdleAnim);
			Prompt.Yandere.transform.position = new Vector3(-17f, 4f, 71f);
			Prompt.Yandere.transform.rotation = base.transform.rotation;
			Prompt.Yandere.RPGCamera.enabled = false;
			Prompt.Yandere.CanMove = false;
			Prompt.Yandere.MainCamera.transform.eulerAngles = new Vector3(0f, -90f, 0f);
			Prompt.Yandere.MainCamera.transform.position = new Vector3(-15f, 4.8f, 71.5f);
			Prompt.Yandere.Jukebox.gameObject.SetActive(value: false);
			Prompt.Yandere.SenpaiFilter.enabled = true;
			Prompt.Yandere.SenpaiFilter.FadeFX = 1f;
			CameraFlashes.SetActive(value: true);
			TaskMinigame.MyMusic.Play();
			TaskMinigame.Show = true;
			TaskMinigame.Limit = 10;
		}
	}
}
