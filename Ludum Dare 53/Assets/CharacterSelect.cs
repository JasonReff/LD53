using UnityEngine;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textbox;
    [SerializeField] private CharacterPool _pool;

    private void Start()
    {
        _pool.CurrentCharacter = null;
    }

    public void SetCharacter(CharacterData data = null)
    {
        if (data == null)
        {
            _textbox.text = $"Random";
            _pool.CurrentCharacter = null;
        }
        else
        {
            _textbox.text = $"{data.CharacterName}";
            _pool.CurrentCharacter = data;
        }
        
    }
}