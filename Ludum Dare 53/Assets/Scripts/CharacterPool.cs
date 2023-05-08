using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterPool")]
public class CharacterPool : ScriptableObject
{
    public CharacterData CurrentCharacter;
    [SerializeField] private List<CharacterData> _characters;
    public List<CharacterData> Characters { get => _characters; }
}