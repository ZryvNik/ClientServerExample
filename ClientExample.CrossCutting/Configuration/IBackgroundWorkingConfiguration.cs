namespace ClientExample.CrossCutting.Configuration
{
    public interface IBackgroundWorkingConfiguration
    {
        int ProccessMessageDelay { get; set; }
    }
}
