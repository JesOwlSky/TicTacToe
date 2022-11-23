using UnityEngine;

public class MoveOrder
{
    private User _activeUser;

    public readonly User Player;
    public readonly User AI;
    public User ActiveUser => _activeUser;
    public bool IsActivePlayer => _activeUser == Player;

    public MoveOrder(Field field)
    {
        Player = new User("Player", Sign.Cross);
        AI = new User("AI", Sign.Circle);
        _activeUser = Player;
        field.OnFieldChanged += ChangeUser;
    }

    private void ChangeUser(Vector2 position, Sign sign)
    {
        _activeUser = _activeUser == Player ? AI : Player;
    }

}
