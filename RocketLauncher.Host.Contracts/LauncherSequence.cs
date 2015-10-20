namespace RocketLauncher.Host.Contracts
{
    public struct LauncherSequence
    {
        public LauncherCommand Command { get; set; }

        public int Length { get; set; }
    }
}
