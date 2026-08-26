using G2DataGUI.Domain.Maps;

namespace G2DataGUI.Serialization.Maps;

/// <summary>
/// Decodes one dialogue section's raw bytes into an opcode sequence — the
/// missing piece that ties MapDialogueReader's byte-level framing to the
/// opcode decoder that's existed since early in this project
/// (MapOpcodeReader and everything under G2DataGUI.Serialization.Maps.Opcodes).
///
/// A top-level byte that isn't a real, dispatchable opcode is skipped —
/// exactly that one byte — rather than being wrapped as a TextOpcode or
/// treated as the end of the section. Real dialogue text only ever exists
/// NESTED inside a TextBoxOpcode's own opcode stream; it never appears as
/// a top-level opcode in a section on its own. Confirmed against real
/// dialogue data (2000.mdt): a section typically chains several separate
/// "blocks" together — preamble opcodes, then a TextBox, repeated — with
/// single stray bytes between/within blocks that are neither real opcodes
/// nor text. Two earlier approaches both lost real content: falling
/// through to TextOpcodeReader for these bytes produced bogus top-level
/// TextOpcodes (wrong per the format's own semantics), and stopping the
/// whole section on the first one silently discarded every subsequent
/// block — confirmed losing multiple full TextBox conversations per
/// section on real data. Skipping the one byte and continuing to scan
/// recovers every block with zero top-level TextOpcodes remaining.
///
/// A section ending right after an opcode byte, with fewer trailing bytes
/// left than that opcode needs, is caught here and treated as the
/// ordinary end of the section, not an error.
///
/// Catches per-section, not per-map — a section that hits a genuinely
/// malformed run still returns whatever opcodes decoded successfully
/// before that point, with the failure recorded rather than losing the
/// whole map's dialogue over one bad section.
/// </summary>
public static class MapDialogueSectionReader
{
	public static MapDialogueEntry Read(int index, byte[] sectionBytes)
	{
		MapDialogueEntry entry = new()
		{
			Index = index,
			RawBytes = sectionBytes,
		};

		using MemoryStream stream = new(sectionBytes);

		MapOpcodeReader opcodeReader = new();

		try
		{
			while (stream.Position < stream.Length)
			{
				long startPosition = stream.Position;

				IMapOpcode? opcode;

				try
				{
					opcode = opcodeReader.Read(stream);
				}
				catch (EndOfStreamException)
				{
					// The section ended right after an opcode byte, with
					// fewer bytes left than that opcode needed - the
					// ordinary end of the section. Nothing more to decode;
					// keep whatever's already in entry.Opcodes.
					break;
				}

				if (opcode is not null)
				{
					entry.Opcodes.Add(opcode);
					continue;
				}

				// NOT falling through to TextOpcodeReader here anymore - a
				// top-level byte that isn't a real opcode is NOT dialogue
				// text. Real dialogue text only ever exists nested inside a
				// TextBoxOpcode's own opcode stream (via
				// TextBoxNestedOpcodeReader) - a TextOpcode appearing
				// directly in entry.Opcodes at the top level is never
				// meaningful. Confirmed against real dialogue data
				// (2000.mdt): sections contain several separate
				// "blocks" - preamble opcodes, then a TextBox, repeated -
				// chained together with single stray bytes between them
				// (trailing padding within a block too) that aren't
				// opcodes and aren't text either. Falling through to
				// TextOpcodeReader wrapped these as bogus top-level
				// TextOpcodes; stopping the whole section on the first one
				// (an even earlier version) silently discarded every
				// subsequent block - confirmed losing multiple full
				// TextBox conversations per section. Skipping exactly this
				// one byte and continuing to scan for the next real opcode
				// is what actually matches the format: it recovers every
				// block with zero top-level TextOpcodes.
				stream.Seek(startPosition + 1, SeekOrigin.Begin);
			}
		}
		catch (Exception ex)
		{
			entry.DecodeError = ex.Message;
		}

		return entry;
	}
}
