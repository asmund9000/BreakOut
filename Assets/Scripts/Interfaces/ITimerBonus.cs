using System.Timers;

public interface ITimerBonus : ISimpleBonus
{
    void Update();
    void RemoveBonus();
}
