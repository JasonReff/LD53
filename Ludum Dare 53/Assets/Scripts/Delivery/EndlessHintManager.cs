public class EndlessHintManager : HintManager
{
    protected override void GetCharacter()
    {
        _pool.CurrentCharacter = null;
        base.GetCharacter();
    }

    public override void MoveCharacterUp()
    {
        GetCharacter();
        base.MoveCharacterUp();
    }
}