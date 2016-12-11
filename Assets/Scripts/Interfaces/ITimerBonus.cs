using System.Timers;

public interface ITimerBonus : ISimpleBonus
{
    void RemoveBonus(object sender, ElapsedEventArgs e);
}
