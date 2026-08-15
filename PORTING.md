# Porting status

This is a scaffold, not a finished port — it has **not been compiled or run**
(no .NET SDK was available while generating it). Treat everything here as a
strong first draft to review against real game files before trusting it with
real save data. `TreatWarningsAsErrors` is on in every csproj — you'll likely
want it off temporarily while getting the new projects to compile for the
first time.

## Project layout

```
G2DataGUI.Domain          → (nothing)                        pure models
G2DataGUI.IO                → (nothing)                        StreamExtensions, EmbeddedResources
G2DataGUI.Serialization      → Domain, IO                        binary readers/writers
G2DataGUI.Repositories         → Domain, Serialization             IRepository, per-feature repos
G2DataGUI.Contexts               → Common, Repositories               Global/Version/Project contexts
G2DataGUI.Locale                   → IO                                 headless ILocalizationService/LocaleService/LocaleKeys
G2DataGUI.Export                     → Domain, Repositories, Locale        CSV exporters
G2DataGUI.Services                     → Common, Contexts, Export, Locale    DataExportService
G2DataGUI.Common                         → (nothing)                          GamePaths, VersionGamePaths, GameProjectPaths, GameVersion
G2DataGUI.UI.Common                        → IO, Locale, Repositories             BaseViewModel, ItemViewModel/CollectionViewModel, LocaleExtension
G2DataGUI                                    → Common, Contexts, Services, UI.Common, Domain, Repositories   Avalonia app + ViewModels/
```

`IDataContext` lives in `G2DataGUI.Domain.Abstractions` (not a `Contexts`
project) so `IRepository : IDataContext` doesn't create a cycle between
Repositories and Contexts — see the comment on `AggregateDataContext`.

`GamePaths` (global: xls_data/map) and `VersionGamePaths` (enemy/boss,
per-version) are deliberately separate types, not one type with nullable
fields and throwing getters — asking the wrong one for a directory it
doesn't have is a compile error, not a runtime exception.

## Reader/writer interfaces

Split into four, matching how a format is actually shaped:

- `ICollectionDataReader<T>` / `ICollectionDataWriter<T>` — "read/write the
  whole file as a list of T", for formats where one file = one collection
  (Moves, Skills, Items, ...). `Repository<T,TId>` takes these.
- `IDataReader<T>` / `IDataWriter<T>` — "read/write exactly one T at the
  stream's current position", for formats where a single record is
  meaningful on its own (an Enemy at a known pointer offset, a single map
  dialogue opcode). This is what makes the Maps opcode system below work.

A type can implement both when it's genuinely used both ways.

## Locale refactor

`ILocalizationService` + `LocaleService` + `LocaleKeys` live in a headless
`G2DataGUI.Locale` project (IO-only dependency, no Avalonia). CSV exporters
take `ILocalizationService` as a constructor parameter like any other
dependency. `G2DataGUI.UI.Common.Locale.LocaleExtension` (the XAML markup
extension) is the only Avalonia-specific piece left, wrapping the headless
service purely as a XAML entry point.

## Fully ported (Domain + Serialization + Repository + Export)

Global (`xls_data/`): **Moves**, **Manaeggs**, **Skills**, **Skillbooks**,
**Levelups**, **Items**, **Specials**, **Initial** — all wired into
`GlobalDataContext` and `DataExportService`.

Per-version (`enemy`/`enemy_hardmode`, `boss`/`boss_hardmode`), via
`GameVersionContext`/`project.Current`: **Enemies**, **Bosses**. These don't
fit `Repository<T,TId>` — one `*_0.dat` per encounter, sometimes two entries
per file, saved by seeking back into that specific file. See
`DirectoryScanRepository<T>`.

Use any of these as the template for a similarly-shaped new feature: Domain
model → binary Reader/Writer → `Repository<T,TId>` subclass → CSV exporter →
one line in `GlobalDataContext` → one export call in `DataExportService`.

## Maps opcodes — fully ported

Every `DialogueOpcode` from the original now has a Domain type and a
`Serialization.Maps` reader, wired into `MapOpcodeReader`'s dispatch table
(the replacement for the old `ParseDialogueOpcode` switch). A few things
worth knowing before touching this code:

- **Two shapes of opcode.** Flat opcodes (`Unknown03Opcode`,
  `CameraMoveOpcode`, `ItemAcquireOpcode`, ...) read a fixed number of bytes
  and return. Compound opcodes (`TextBox`, `Pause`, `Highlight`,
  `Unknown10`) read a *second* dispatch byte (their own `...Option` enum)
  before deciding what to read — e.g. `TextBoxOpcodeReader` reads a
  `TextBoxOption` byte, then delegates to `CreateTextBoxOpcodeReader`,
  `RemoveTextBoxOpcodeReader`, etc.
- **`TextBox` and `Highlight` recurse.** `CreateTextBoxOpcode` and
  `HighlightStartOpcode` parse a *stream* of further opcodes into
  `NestedOpcodes`, stopping only when they produce a terminator (any
  `ITextBoxOpcode` for TextBox variants; specifically `HighlightEndOpcode`,
  not just any `IHighlightOpcode`, for Highlight — ported exactly as the
  original distinguishes these). Any byte encountered that isn't a defined
  `DialogueOpcode` gets treated as text and handed to `TextOpcodeReader`
  instead. This recursion is why `MapOpcodeReader.ReadFromOpcode(Stream,
  DialogueOpcode)` is public and static — `TextBoxNestedOpcodeReader` and
  `HighlightStartOpcodeReader` call back into it directly instead of each
  needing their own copy of the dispatch table.
- **`TextOpcodeReader` behaves differently from everything else.** It isn't
  reached via the dispatch table at all (`DialogueOpcode.Text` has no
  dictionary entry, matching the original — the switch never handled it
  either). It's only ever called directly by the TextBox/Highlight
  recursion when a byte doesn't match any known opcode: it consumes raw
  bytes as characters (treating a literal `NextLine` byte as an embedded
  `\n` and continuing) until it hits a byte that IS a different defined
  opcode, then backs the stream up one byte so the caller re-reads it. The
  original wrapped this in a try/catch that logged to a static `Errors`
  singleton; that's dropped (no replacement for the removed
  `Errors`/`Error` singleton — see "Known rough edges" below), so a
  malformed stream now throws instead of silently truncating text.
- **Two typos preserved on purpose.** `DialogueOpcode.ItemAquire` and
  `DialogueOpcode.CharacterPortait` keep the original's misspellings — the
  enum member names are cosmetic (only the byte values are load-bearing),
  but matching the source exactly makes it easier to cross-reference
  against any existing notes/comments about these opcodes. The Domain
  *type* names (`ItemAcquireOpcode`, `CharacterPortraitOpcode`) are spelled
  correctly.
- **One inconsistency in the original, preserved rather than "fixed."** On
  an unrecognized sub-dispatch byte, `Highlight` and `Unknown10` seek the
  stream back one byte before returning null; `TextBox` and `Pause` don't.
  That asymmetry is in the original source, not introduced here — changing
  it would be a behavior change I can't verify against a real map file.
- **Not yet touched:** `MapDialogue`/`MapDialogueHeader` (the length-bounded
  section that calls `MapOpcodeReader` in a loop), `MapHeader`/`MapEntry`/
  `Map` (the outer container), and `MapEnemyGroup`/`MapShop`/`MapIcon`/
  `MapHTA`/`MapMOS`/`MapScript`/`MapInstance` (other map substructures, not
  reviewed at all yet). The opcode system is now capable of parsing a
  dialogue byte stream end-to-end; what's missing is the code that finds
  that byte stream inside an actual map file and hands it to
  `MapOpcodeReader` in a loop.

## ViewModels — done for every feature, plus version-switch orchestration

Domain models deliberately don't implement `INotifyPropertyChanged`
themselves (kept as plain data), so that lives at the ViewModel layer:

- **`IRepository.MarkDirty()`** (new) — `Repository<T,TId>.MarkDirty()` and
  `DirectoryScanRepository<T>.MarkDirty()` were `protected`; nothing outside
  the repository could call them. Now `public`, declared on `IRepository`,
  so a ViewModel holding any repository can call it without needing the
  concrete type.
- **`ItemViewModel<TModel>`** (`G2DataGUI.UI.Common.ViewModels`) — wraps one
  model instance. `SetModelValue(value, getter, setter)` is the one thing
  every property setter calls: skips the write if unchanged, otherwise
  writes the model, calls `Repository.MarkDirty()`, and raises
  `PropertyChanged`.
- **`CollectionViewModel<TModel, TViewModel>`** (same namespace) — wraps a
  repository's `ObservableCollection<TModel>` (`Items`) as an
  `ObservableCollection<TViewModel>`, staying in sync as the repository
  loads/reloads (`Reset`) or gains/loses individual items (`Add`/`Remove`).

**Every feature now has the full three-file shape**, under
`G2DataGUI/ViewModels/<Feature>/`: Moves, Manaeggs, Skills, Skillbooks,
Levelups, Items, Specials, Starting (global — all wrapped by
`GlobalDataViewModel`), and Enemies, Bosses (version-specific — wrapped by
`GameVersionViewModel`). Nested fixed-size collections (`Manaegg.Spells`,
`Skillbook.Skills`, `CharacterLevelups.Levels`, `EnemyAISection.Conditions`,
`EnemyMoveset.Moves`, `Boss.Movesets`) are projected once at construction as
`IReadOnlyList<TViewModel>` rather than run through `CollectionViewModel` —
they never grow/shrink at runtime, and the whole parent ViewModel gets
rebuilt on reload anyway, so there's nothing for change-tracking to do.
`CharacterLevelupsViewModel`/`SpecialSetViewModel`/
`CharacterStartingStatsViewModel` each expose a read-only `CharacterName`
resolved via `ILocalizationService` (`LocaleKeys.Characters`/
`LocaleKeys.SpecialChars`) — there's no on-disk name, only the index, same
convention the CSV exporters already use. `Equipment`/`Usable`'s
`CharacterFlag(CharacterTypes)`/`AilmentFlag(AilmentTypes)` are
method-based, not per-flag properties like `MoveStatsViewModel`'s ailment
bools — exposed as raw bitflags in `EquipmentViewModel`/`UsableViewModel`;
a per-flag checkbox list would enumerate the enum and isn't built out here.

**Version switching** — `GameProjectViewModel` is the piece that makes
"the user can switch Normal/Hard at any time" actually work:

- `Current` (a `GameVersionViewModel`) is not fixed at construction.
  `SwitchVersionAsync(version)` calls `GameProjectContext.SwitchVersionAsync`
  (which loads that version's Enemies/Bosses from disk the first time it's
  visited, and is a no-op — no disk hit — on a version already loaded),
  then reassigns `Current` and raises `PropertyChanged` for both `Current`
  and `CurrentVersion`.
- Anything bound to `Current.Enemies`/`Current.Bosses` — a tab, a list —
  updates the moment `Current` itself changes; it doesn't need to know a
  switch happened, only that `Current` is a property that can change.
- `GameVersionViewModel` instances are cached per `GameVersion` inside
  `GameProjectViewModel`, mirroring `GameProjectContext`'s own `_versions`
  cache — switching back to a version you've already visited reuses both
  the loaded data and its ViewModel wrappers rather than rebuilding either.
- `GlobalDataViewModel` (xls_data — Moves, Items, etc.) is the opposite:
  never forked by version, never rebuilt, one instance for the project's
  whole lifetime — wired up once in `GameProjectViewModel`'s constructor.

No XAML views exist yet — these ViewModels aren't bound to anything in the
running app, and `MainWindow.axaml.cs`'s test code still constructs
`GlobalDataContext`/`GameVersionContext` directly rather than going through
`GameProjectViewModel`.

## Deliberately not touched

- **DDS viewer** — not game data, just a texture preview tool. Move
  `DDSLoader` under `G2DataGUI.IO` as-is.
- **Views (XAML)** — untouched. `G2DataGUI.csproj` now references
  `UI.Common`/`Domain`/`Repositories` so the ViewModels above compile, but
  nothing binds them to a window yet.

## Known rough edges, called out on purpose rather than silently "fixed"

- **No error-reporting story.** The old `Errors`/`Error` singleton tied
  error state to `Avalonia.Media.Color`, which doesn't belong in a data
  layer. Nothing replaces it — `LoadAsync`/`SaveAsync`/`TextOpcodeReader`
  just propagate exceptions now. Decide how the UI wants to surface
  load/save/parse failures before wiring the real UI up.
- **`FixedString` padding side matters per-field.** Names are left-padded;
  descriptions are right-padded. Get this backwards for a new field and
  values round-trip with whitespace silently trimmed from the wrong side —
  verify against a hex dump when porting a new fixed-width field.
- **`DifficultyMode` was removed, not ported.** It was a second, independent
  source of truth for "which version is active" alongside
  `GameProjectContext.CurrentVersion`. Project a bindable flag from
  `CurrentVersion` if the UI needs one, rather than reintroducing a
  separate settable singleton.
- **`Boss.Movesets` is a single-entry list, matching the original.** The
  original's own doc comment describes an intended `zXX_act.dat` for extra
  movesets that `ReadBoss`/`WriteBoss` never actually read. Preexisting gap
  in the source project, not introduced here.
