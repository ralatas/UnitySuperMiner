using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Meta
{
    public class UIGameStateView: MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI Title;
        [SerializeField] public Image StateImage; 
        [SerializeField] public Button OneMoreButton;
        
        [SerializeField] public Sprite StateImageWin;
        [SerializeField] public Sprite StateImageLose;
        [SerializeField] public Sprite StateImageGaming;
    }
}