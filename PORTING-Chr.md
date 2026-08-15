# Porting status: DreamcastChrViewer → G2DataGUI

Companion to PORTING.md — covers integrating the .chr/.chr2 model-archive
viewer (originally a separate standalone Avalonia app) into this project.
Same caveat as PORTING.md: nothing here has been compiled (no .NET SDK
available in this environment). Confidence levels vary a lot piece to
piece — called out explicitly below rather than left implicit.

## Where everything lives

```
G2DataGUI.Domain/Chr/            ChrArchive, ChrChunk, CddhInfo, ChrModel,
                                  MotionSet, MotionAnimation, DdsTexture,
                                  ChrModelMover, ChrTextureBinder
G2DataGUI.Serialization/Chr/     ChrArchiveReader, ChrArchiveWriter,
                                  DdsReader, DdsTextureReader
G2DataGUI.Repositories/Chr/      ChrArchiveRepository

G2DataGUI.Domain/Models/         NjModel, NjBone, NjVertex, NjTriangle,
                                  NjInfluence, NjMotion, NjMotionTrack,
                                  NjVectorKey/NjEuler32Key/NjQuaternionKey
G2DataGUI.Serialization/Models/  NjModelReader, NjMotionReader,
                                  NjVertexStateData

G2DataGUI.Rendering (new project) NjPoseEvaluator, ModelViewport
```

`ChrArchiveReader : IDataReader<ChrArchive>` — one file is one archive.
Doesn't set `ChrArchive.FileName`; `ChrArchiveRepository` fills that in
after reading, same convention as Enemy/Boss's `FilePath`.

## Confidence levels — read this before trusting any given piece

**Verified against real data, high confidence:**
- The chunk-container walk (`ChrArchiveReader`'s chunk/model/motion-set
  parsing) — matched chunk-for-chunk against a real archive (a430.chr)
  before porting.
- `NjModelReader`/`NjMotionReader` (NJCM geometry, NMDM motion) — this is
  the product of an extensive back-and-forth debugging session against
  real files, including a full FBX-export ground-truth comparison
  (vertex positions, triangle connectivity, AND triangle winding all
  independently verified to match Neosis's own output on real files).
  The vertex-chaining fix, the `0x2C`-is-really-`0x2B` finding, and the
  B/C winding swap all came from that process, not from guessing.
- `NjPoseEvaluator`'s world-transform formula — the exact scale-rotate-
  translate/parent-accumulation math already verified against FBX ground
  truth while debugging `NjModelReader`.

**Reasoned but NOT verified — new this session, no compiler or renderer
available to check:**
- `ModelViewport`'s adaptation from cached pre-skinned arrays to
  per-frame skinning via `NjPoseEvaluator`. The rasterizer itself (depth
  buffer, bilinear filtering, projection) is an unmodified copy of code
  that WAS verified interactively against real renders; only the
  skinning integration around it is new and unverified.
- `ChrArchiveWriter` — reasoned from a structural fact (chunk payload
  pointers are self-relative, so payloads can move between files intact)
  rather than round-trip tested. No compiler here to actually write a
  file, read it back, and confirm it matches. **Test this before trusting
  it with real archives** — write an archive, load the result back
  through `ChrArchiveReader`, and check it renders correctly.
- `ChrModelMover` — the move/add/remove logic itself is straightforward
  list manipulation; the one subtle part (motion-set ownership tracked by
  object reference, not by the numeric ModelIndex, specifically so two
  archives' same-numbered models can't collide during a merge) is
  reasoned carefully but likewise untested.

**Known incomplete, not a bug:** the last investigation into DreamcastChrViewer
found a small, real gap — about 8 triangles out of 520 on one test model
don't get picked up by the current strip-reading logic (see the
DreamcastChrViewer conversation history for specifics: two small clusters,
one at head height, one at the neckline). That gap carries over here
unchanged. It's real but small, and no fix was found before this port —
worth knowing if a specific rendered model looks like it's missing a small
detail.

## What NjModel/NjMotion look like here, and why they differ from the original

`NjModel`/`NjBone`/`NjVertex`/`NjMotion`/`NjMotionTrack` hold data only —
no `AddMesh`/`ApplyPose`/`SkinPosition` methods on the types themselves,
unlike the original where `NjModel` mixed data and algorithm. All of that
lives in `NjPoseEvaluator` (`G2DataGUI.Rendering`) instead — bones store
their bind-pose Position/Rotation/Scale plus a flattened `ParentIndex`
(not child/sibling pointers), and world transforms/skinning are computed
on demand rather than cached and invalidated on pose changes. This means
`ModelViewport` recomputes every vertex's world position on every single
`Render` call — fine for a viewer, would need a real cache if this ever
needs to render many models/frames per second at once.

## Rendering — what's there, what isn't

`ModelViewport : Control` exists as a standalone control (not wired to any
window/view, as requested) with:
- Orbit camera (drag to rotate, wheel to zoom) — ported unchanged.
- Depth-tested software rasterization (solid and textured paths).
- Bilinear texture filtering.
- `SetModel(NjModel?, textures)` for static viewing, `SetAnimation(NjMotion?,
  frame)` for animation playback (caller drives the frame number/timer —
  nothing here auto-advances).

Not done: hooking `ModelViewport` up to any actual window, and no
"play this animation over real time" driver (a `DispatcherTimer` calling
`SetAnimation` with an advancing frame each tick — small, not built yet).

## Moving models and motions between archives

The design that makes this tractable: `ChrArchiveWriter` reconstructs a
file from `ChrArchive.Models`/`MotionSets`, and every internal pointer
inside an NJCM/NMDM/POF0 chunk's payload is relative to that payload's own
start — verified repeatedly while reverse-engineering these formats. That
means a chunk's payload bytes are valid wherever they end up in a
rebuilt file; only the outer chunk sequence (which chunks, in what order)
needs regenerating, never the payload contents.

`ChrModelMover.Move`/`.Remove` operate purely on in-memory `ChrArchive`
objects — nothing touches a file until you call `ChrArchiveRepository.
SaveAsync` afterward (once per archive touched, twice for a cross-archive
move). `Move` works within one archive too, as a reorder.

**Not yet built:** any UI/ViewModel layer for this — right now it's a
library-level operation (`ChrModelMover.Move(sourceArchive, index,
destinationArchive, insertAt)`), not something wired to a drag-and-drop
or cut/paste UI gesture. Given the writer is unverified (see above), I'd
treat "move a model" as blocked on testing the writer first, not ready to
build a UI on top of yet.

## The `Map`/`MapEntry` gap — still there, still real

Associating a `.chr` archive with a specific map (so "which archive
belongs to map `a430`" is answerable) still needs the `Map`/`MapEntry`
container in `G2DataGUI.Domain.Maps`, which doesn't exist — everything
built for Maps so far is the dialogue *opcode* system (see PORTING.md).
Nothing in this session touched that; it's the same gap noted before.

## Suggested next steps

1. Get this compiling for the first time and shake out whatever the (very
   likely nonzero) list of small mistakes turns out to be — nothing here
   has seen a compiler yet.
2. Round-trip test `ChrArchiveWriter`: write an archive, read it back,
   confirm it renders the same as the original file.
3. Wire `ModelViewport` into an actual window/ViewModel to see real output
   for the first time.
4. A `DispatcherTimer`-based animation driver for `SetAnimation`.
5. `Map`/`MapEntry` container — the real prerequisite for tying a `.chr`
   to a specific map location.
6. Investigate the ~8-triangle gap noted above, now that there's a
   rendering path to actually see its effect rather than only detecting it
   by FBX comparison.
