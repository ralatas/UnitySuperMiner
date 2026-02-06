using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay
{
    public class CellView: MonoBehaviour
    {
        public Vector2Int WorldPosition;
        
        [SerializeField] public Sprite closedSprite;
        [SerializeField] public Sprite bombSprite;
        [SerializeField] public Sprite bombRedSprite;
        [SerializeField] public Sprite flagSprite;
        [SerializeField] public Sprite mineWrong;
        [SerializeField] public List<Sprite> NearBombSprite;
    }
}