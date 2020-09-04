using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Chess.Model.Actions;
using Chess.Model.Models;
using GameBase.Model;
using Move = Chess.Model.Models.Move;

namespace Chess.Model.Reducers
{
    public class BoardReducer : IReducer<BoardStore>
    {
        private static readonly Point None = new Point(-1, -1);

        public BoardStore Apply(IAction action, BoardStore store)
        {
            var next = store;
            switch (action)
            {
                case InitializeAction ia:
                {
                    next = BoardFactory.Instance.Create(ia.Version);
                    next.Available = Enumerable.Empty<Point>();
                    next.History = Enumerable.Empty<MoveHistoryItem>();
                    next.Selection = None;
                    break;
                }
                case MoveAction ma:
                {
                    if (store.Available.Contains(ma.Target))
                    {
                        var (placements, taken) = Move(store.Placements, store.Selection, ma.Target);
                        next = new BoardStore
                        {
                            Size = store.Size,
                            CornerSize = store.CornerSize,
                            Selection = None,
                            Available = Enumerable.Empty<Point>(),
                            Placements = placements,
                            Captured = store.Captured.ToDictionary(pair => pair.Key, pair =>
                                {
                                    var captured = pair.Value.ToList();

                                    if(pair.Key == taken.Color) captured.Add(taken);

                                    return (IEnumerable<Piece>) captured;
                                }
                            ),
                            History = store.History.Append(new MoveHistoryItem
                            {
                                Start = store.Placements.Select(p=> new Placement<Piece>(p.Piece, p.Location)),
                                Move = new Move
                                {
                                    From = store.Selection,
                                    To = ma.Target
                                }
                            })
                        };
                    }
                    else
                    {
                        throw new InvalidOperationException($"{ma.Target} is not a valid move.");
                    }
                    break;
                }
                case SelectAction sa:
                {
                    return new BoardStore
                    {
                        Size = store.Size,
                        CornerSize = store.CornerSize,
                        Selection = sa.Selection,
                        Available = GetAvailable(store.Placements, sa.Selection, store.Size, store.CornerSize),
                        Placements = store.Placements.Select(p=> new Placement<Piece>(p.Piece, p.Location)),
                        Captured = store.Captured.ToDictionary(pair => pair.Key,
                            pair => (IEnumerable<Piece>)pair.Value.ToList()
                            ),
                        History = store.History.ToList()
                    };
                }
            }
            return next;
        }

        private static bool IsOnBoard(Point pt, int size, int corner)
        {
            var isXCorner = pt.X < corner || pt.X >= size - corner;
            var isYCorner = pt.Y < corner || pt.Y >= size - corner;
            return !(isXCorner && isYCorner) && (pt.X >= 0 && pt.X < size)
                                             && (pt.Y >= 0 && pt.Y < size);
        }

        private static IEnumerable<Point> GetAvailable(IEnumerable<Placement<Piece>> placements, Point location, int size, int corner)
        {
            var places = placements.ToList();

            var pc= Find(places, location).Piece;
            
            if(pc.IsEmpty) return Enumerable.Empty<Point>();

            var result = new List<Point>();

            foreach (var direction in Directions.All)
            {
                var rule = pc.MoveRules[direction];
                for (var d = rule.MinCount; d > 0 && d <= rule.MaxCount; d++)
                {
                    var to = rule.GetResult(location, direction, d);
                    var target = Find(places, to).Piece;

                    if (!(IsOnBoard(to, size, corner) && target.IsEmpty))
                    {
                        break;
                    }

                    result.Add(to);
                }

                rule = pc.AttackRules[direction];
                for (var d = rule.MinCount; d > 0 && d <= rule.MaxCount; d++)
                {
                    var to = rule.GetResult(location, direction, d);
                    var target = Find(places, to).Piece;

                    if (!IsOnBoard(to, size, corner) || target.Color.Equals(pc.Color)) break;

                    if(result.Contains(to) || target.IsEmpty) continue;

                    result.Add(to);

                    break;
                }
            }

            return result;
        }
        
        private static (IEnumerable<Placement<Piece>>, Piece) Move(IEnumerable<Placement<Piece>> placements, Point @from, Point to)
        {
            var places = placements.Select(p=> new Placement<Piece>(p.Piece, p.Location)).ToList();
            var source = Find(places, @from);
            var target = Find(places, to);

            var taken = target.Piece;

            source.Location = target.Location;
            source.Piece.Moved();

            return (places.Where(place => place != target), taken);
        }

        private static Placement<Piece> Find(IEnumerable<Placement<Piece>> placements, Point location)
        {
            try
            {
                return placements.First(p => p.Location == location);
            }
            catch
            {
                return new Placement<Piece>(Piece.CreateEmpty(), location);
            }
        }
    }
}
