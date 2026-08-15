using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Enemies;

/// <summary>
/// One enemy instance. Unlike every other feature ported so far, Enemies
/// aren't one row inside a single big file — the game stores one *_0.dat
/// file per encounter, and some of those files hold a second enemy at a
/// different pointer offset. EnemyRepository (a DirectoryScanRepository,
/// not the usual Repository&lt;T,TId&gt;) is what walks the directory and
/// fills these in; see PORTING.md.
/// </summary>
public sealed class Enemy : IIdentifiable<int>
{
	/// <summary>Assigned by EnemyRepository in directory-scan order — there
	/// is no on-disk enemy id, same convention as Move.Id.</summary>
	public int Id { get; set; }

	public EnemyStats Stats { get; set; } = new();
	public EnemyAISection AISection { get; set; } = new();
	public EnemyMoveset Moveset { get; set; } = new();

	/// <summary>True if this is the second enemy packed into Filename.</summary>
	public bool IsSecond { get; set; }

	/// <summary>Full path of the *_0.dat this entry lives in — needed on
	/// save because writes go back to a pointer offset inside this specific
	/// file, not to a shared collection file like every other repository.</summary>
	public string FilePath { get; set; } = string.Empty;
}
