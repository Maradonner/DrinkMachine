namespace DrinkMachine.DAL.Entities;

public class SessionModel
{
    private const long MinBalance = 0;
    private long _balance;


    public Guid Id { get; set; }

    public long Balance
    {
        get => _balance;
        set => _balance = value < MinBalance ? MinBalance : value;
    }

    public DateTime Created { get; set; }

    public DateTime LastAccessed { get; set; }

    public int? UserId { get; set; }
}