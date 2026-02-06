using CodeBase.Infrastructure.Common;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Match
{
    public interface IMatchService
    {
        void TryOpenNearSimilarField( CellData cellData );
    }
}