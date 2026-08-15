namespace G2DataGUI.Domain.Chr;

/// <summary>
/// Builds the texture list a ModelViewport needs from a ChrModel's own
/// GlobalIdentifiers (its GIXL list) plus whatever DDS textures happen to
/// be loaded — order matters: a triangle's TextureIndex is a position in
/// this exact list, matching the model's own GIXL order, so this can't be
/// arbitrary sequential order, has to be model.GlobalIdentifiers[i] found
/// (or null if not loaded) at every position i.
/// </summary>
public static class ChrTextureBinder
{
	public static IReadOnlyList<DdsTexture?> BindTextures(
		ChrModel model,
		IEnumerable<DdsTexture> availableTextures)
	{
		List<DdsTexture> pool = availableTextures.ToList();

		return model.GlobalIdentifiers
			.Select(id => pool.FirstOrDefault(t => t.GlobalIdentifier == id))
			.ToList();
	}
}
