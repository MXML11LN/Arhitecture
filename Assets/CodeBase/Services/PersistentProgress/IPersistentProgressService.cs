using CodeBase.Data.Progress;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; set; }
    }
}