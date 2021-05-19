using System;
using System.Linq;
using Chess.Model;
using Chess.Model.Extensions;
using Chess.Model.Models;
using Chess.Model.Stores;
using Version = Chess.Model.Models.Version;

namespace Chess.Server.Services.Model
{
    public static class GameExtensions
    {
        public static GameFile ToStorage(this Game game) => new GameFile
        {
            Id = game.Id,
            Name = game.Name,
            CurrentColor = game.Store.CurrentColor.ToString(),
            Version = game.Store.Version.ToString(),
            Corners = game.Store.Board.Corners,
            Size = game.Store.Board.Size,
            Placements = game.Store.Board.Where(tuple => !tuple.Item2.IsEmpty).Select(tuple => new PlacementFile
            {
                X = tuple.Item1.X,
                Y= tuple.Item1.Y,
                Color = tuple.Item2.Color.ToString(),
                Piece = tuple.Item2.Type.ToString(),
                HasMoved = tuple.Item2.HasMoved
            })
        };

        public static Game ToModel(this GameFile file)
        {
            var builder = new BoardBuilder().SetSize(file.Size).SetCorners(file.Corners);

            foreach (var placement in file.Placements)
            {
                builder.AddPiece(placement.X, placement.Y,
                    placement.Color.ToColor(),
                    placement.Piece.ToPieceType(),
                    placement.HasMoved);
            }

            return new Game
            {
                Id = file.Id,
                Name = file.Name,
                Store = Evaluator.Instance.Evaluate(new GameStore
                {
                    Board = builder.Build(),
                    CurrentColor = Enum.Parse<Color>(file.CurrentColor ?? Color.None.ToString()),
                    Version = Enum.Parse<Version>(file.Version),
                    Markings = new MarkingStore()
                })
            };
        }
    }
}
