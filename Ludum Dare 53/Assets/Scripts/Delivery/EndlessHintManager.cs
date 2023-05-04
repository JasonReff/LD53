public class EndlessHintManager : HintManager
{
    public override void MoveCharacterUp()
    {
        GetCharacter();
        base.MoveCharacterUp();
    }
}