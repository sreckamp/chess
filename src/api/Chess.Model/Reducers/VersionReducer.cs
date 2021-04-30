using Chess.Model.Actions;
using Chess.Model.Models;

namespace Chess.Model.Reducers
{
    public sealed class VersionReducer : IReducer<Version>
    {
        public Version Apply(IAction action, Version store) => action is InitializeAction ia ? ia.Version : store;
    }
}