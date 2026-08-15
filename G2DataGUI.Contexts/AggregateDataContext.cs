using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Contexts;

/// <summary>
/// Base for any context that is really just "a bag of child IDataContexts
/// whose Load/Save/Reload/IsDirty all fan out the same way". Works for both
/// a bag of repositories (GlobalDataContext, GameVersionContext — T is
/// IRepository, which extends IDataContext) and a bag of contexts
/// (GameProjectContext — T is IDataContext directly).
///
/// Children comes from an overridden property, not a constructor parameter:
/// referencing `this` inside a `: base(...)` call is illegal in C# before
/// the instance exists, so a derived class can't pass `[Foo, Bar]` as a
/// ctor argument if Foo/Bar are its own properties. An overridden property
/// sidesteps that — and as a bonus it's evaluated fresh on every access, so
/// a context whose children grow at runtime (GameProjectContext adding
/// GameVersionContexts as the player switches version) needs no extra
/// plumbing beyond returning the live collection.
/// </summary>
public abstract class AggregateDataContext<T> : IDataContext
	where T : IDataContext
{
	protected abstract IEnumerable<T> Children { get; }

	public virtual bool IsDirty =>
		Children.Any(c => c.IsDirty);

	public virtual async Task LoadAsync(CancellationToken cancellationToken = default)
	{
		foreach (T child in Children)
		{
			await child.LoadAsync(cancellationToken).ConfigureAwait(false);
		}
	}

	public virtual async Task SaveAsync(CancellationToken cancellationToken = default)
	{
		foreach (T child in Children)
		{
			if (child.IsDirty)
			{
				await child.SaveAsync(cancellationToken).ConfigureAwait(false);
			}
		}
	}

	public virtual async Task ReloadAsync(CancellationToken cancellationToken = default)
	{
		foreach (T child in Children)
		{
			await child.ReloadAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}
