namespace ClientExample.CrossCutting.Configuration
{
    public class BackgroundWorkingConfiguration: IBackgroundWorkingConfiguration
    {
        public int ProccessMessageDelay { get; set; }
    }
}
