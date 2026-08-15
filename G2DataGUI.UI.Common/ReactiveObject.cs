namespace G2DataGUI.UI.Common;

public class ReactiveObject<T>(T defaultValue)
{
	private readonly ReaderWriterLock _readerWriterLock = new();
	private bool _isInitialized = false;

	public event EventHandler<ValueChangedEventArgs>? ValueChanged;

	public T Value
	{
		get
		{
			_readerWriterLock.AcquireReaderLock(Timeout.Infinite);
			T value = field;
			_readerWriterLock.ReleaseReaderLock();

			return value;
		}
		set
		{
			_readerWriterLock.AcquireWriterLock(Timeout.Infinite);

			T oldValue = field;

			bool oldIsInitialized = _isInitialized;

			_isInitialized = true;
			field = value;

			_readerWriterLock.ReleaseWriterLock();

			if (!oldIsInitialized || oldValue == null || !oldValue.Equals(field))
			{
				ValueChanged?.Invoke(this, new ValueChangedEventArgs(oldValue, value));
			}
		}
	} = defaultValue;

	public static implicit operator T(ReactiveObject<T> obj)
	{
		return obj.Value;
	}

	public class ValueChangedEventArgs(T oldValue, T newValue)
	{
		public T OldValue { get; } = oldValue;
		public T NewValue { get; } = newValue;
	}
}
