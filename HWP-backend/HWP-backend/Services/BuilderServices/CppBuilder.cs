using HWP_backend.Models.DTO.Builds;

namespace HWP_backend.Services.BuilderServices
{
    public class CppBuilder : ABuilder<CppBuilder>, IBuilder
    {
        public CppBuilder(ProcessKiller processKiller) : base(processKiller)
        {
        }

        public BuildModelDTO Build(string buildDir, string projectName, bool isLinux)
        {
            var commandToBuild = isLinux ? $"-Wall {buildDir}/{projectName}.cpp -o {buildDir}/{projectName}" :
                $"-Wall {buildDir}\\{projectName}.cpp -o {buildDir}\\{projectName}.exe";

            return BuildInternal(commandToBuild);
        }

        public string Run(string buildDir, string projectName, bool isLinux, params string[] inputs)
        {
            var runCommand = isLinux ? $"{buildDir}/{projectName}" : $"{buildDir}\\{projectName}.exe";
            return RunInternal(runCommand, inputs);
        }
    }
}