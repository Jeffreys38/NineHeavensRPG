using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class SettingsSystem : MonoBehaviour
{
	[SerializeField] private VoidEventChannelSO SaveSettingsEvent = default;

	[SerializeField] private SettingsSO _currentSettings = default;
	[SerializeField] private SaveSystem _saveSystem = default;

	[SerializeField] private FloatEventChannelSO _changeMasterVolumeEventChannel = default;
	[SerializeField] private FloatEventChannelSO _changeSFXVolumeEventChannel = default;
	[SerializeField] private FloatEventChannelSO _changeMusicVolumeEventChannel = default;

	private void Awake()
	{
		_currentSettings.LoadSavedSettings(_saveSystem.gameData);
		SetCurrentSettings();
	}
	
	private void OnEnable()
	{
		SaveSettingsEvent.OnEventRaised += SaveSettings;
	}
	
	private void OnDisable()
	{
		SaveSettingsEvent.OnEventRaised -= SaveSettings;
	}

	void SetCurrentSettings()
	{
		_changeMusicVolumeEventChannel.RaiseEvent(_currentSettings.MusicVolume);//raise event for volume change
		_changeSFXVolumeEventChannel.RaiseEvent(_currentSettings.SfxVolume); //raise event for volume change
		_changeMasterVolumeEventChannel.RaiseEvent(_currentSettings.MasterVolume); //raise event for volume change

		LocalizationSettings.SelectedLocale = _currentSettings.CurrentLocale;
	}
	
	void SaveSettings()
	{
		
	}
}

