using HWP_backend.Models.DTO.Builds;

namespace HWP_backend.Services.BuilderServices
{
    public interface IBuilder
    {
        BuildModelDTO Build(string buildDir, string projectName);
        string Run(string buildDir, string projectName, params string[] inputs);
    }
}