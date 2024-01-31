using System;
using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Audio;
using G2DataGUI.UI.Common.ViewModels;
using NAudio.Vorbis;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace G2DataGUI.UI.ViewModels;

public class AudioViewerViewModel : BaseViewModel, IDisposable
{
	public ObservableCollection<AudioNode> GameDirectoryAudioFiles { get; } = AudioFiles.Instance.DirectoryAudioFiles;
	private AudioNode _selectedAudioFile = null;
	private VorbisWaveReader _waveReader = null;
	private WasapiOut _wasapiOut = null;
	public int TreeViewWidth { get; } = 200;

	public static AudioViewerViewModel Instance { get; private set; } = new();

	private AudioViewerViewModel() { }

	private MMDevice GetDefaultAudioDevice()
	{
		using var enumerator = new MMDeviceEnumerator();
		return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
	}

	private void AudioFileCollectionRefreshed(object sender, EventArgs args) { }

	private void LoadAudioFile()
	{
		StopPlayback();

		if (SelectedAudioFile != null)
		{
			_waveReader = new VorbisWaveReader(SelectedAudioFile.Path);
			_wasapiOut = new(GetDefaultAudioDevice(), AudioClientShareMode.Shared, false, 10);
			_wasapiOut.Init(_waveReader);
			_wasapiOut.Play();
		}
	}

	private void StopPlayback()
	{
		if (_wasapiOut != null && _wasapiOut.PlaybackState == PlaybackState.Playing)
		{
			_wasapiOut.Stop();
			_wasapiOut?.Dispose();
			_wasapiOut = null;
		}

		if (_waveReader != null)
		{
			_waveReader?.Dispose();
			_waveReader = null;
		}
	}

	public void Dispose() => StopPlayback();

	public AudioNode SelectedAudioFile
	{
		get => _selectedAudioFile;
		set
		{
			if (value.IsFile)
			{
				_selectedAudioFile = value;
				LoadAudioFile();
				OnPropertyChanged(nameof(SelectedAudioFile));
			}
		}
	}
}
