using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Paths;

namespace G2DataGUI.Common.Data.Moves;

public class Moves
{
    public static Moves Instance { get; } = new();
    public ObservableCollection<Move> GameMoves { get; private set; } = new();
    public event EventHandler CollectionRefreshed;
    public static int NumberOfMoves { get; } = 0x80;

    private Moves()
    {
		_ = ReadMovesAsync();
    }

	public void Save() => WriteMoves();

	public void Reload() => _ = ReadMovesAsync();

	private async Task ReadMovesAsync() =>
		await Task.Run(ReadMoves).ConfigureAwait(false);

	private void ReadMoves()
    {
        GameMoves.Clear();
        using FileStream reader = File.Open(
            Version.Instance.RootDataDirectory + GamePaths.MovesPath,
            FileMode.Open,
            FileAccess.Read);
        using MemoryStream memReader = new();
        reader.CopyTo(memReader);
        memReader.Seek(0, SeekOrigin.Begin);
        while (memReader.Position < memReader.Length)
        {
            Move move = Move.ReadMove(memReader);
            move.ID = Convert.ToByte(GameMoves.Count);
            GameMoves.Add(move);
        }

        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteMoves()
    {
        using FileStream writer = File.Open(
            Version.Instance.RootDataDirectory + GamePaths.MovesPath,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite);
        foreach (Move move in GameMoves)
        {
            move.WriteMove(writer);
        }
    }

    public void GenerateCSV()
    {
        using FileStream stream = File.Open(
			ProjectPaths.MovesCSVPath,
			FileMode.Create,
			FileAccess.Write);
        using StreamWriter writer = new(stream);
        writer.WriteLine(Move.CSVHeader);
        foreach (Move move in GameMoves)
        {
            move.GenerateCSV(writer);
        }
    }
}
