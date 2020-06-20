using HWP_backend.Models.DTO.Builds;

namespace HWP_backend.Services.BuilderServices
{
    public class BuildService
    {
        private readonly CppBuilder _builder;

        public BuildService()
        {
            _builder = new CppBuilder(new ProcessKiller());
        }

        public BuildModelDTO BuildAndRun(string pathDir, string fileName, bool isLinux, params string[] inputs)
        {
            var model = Build(pathDir, fileName, isLinux);
            if (model.IsSuccess)
                model.Output += $"\n{Run(pathDir, fileName, isLinux, inputs)}";
            DeleteFiles(pathDir, fileName, isLinux);
            return model;
        }

        private BuildModelDTO Build(string pathDir, string fileName, bool isLinux)
        {
            var result = _builder.Build(pathDir, fileName, isLinux);
            return result;
        }

        private string Run(string pathDir, string fileName, bool isLinux, params string[] inputs)
        {
            var result = _builder.Run(pathDir, fileName, isLinux, inputs);
            return result;
        }

        public void MakeFile(string pathDir, string fileName, string code, bool isLinux)
        {
            FileMaker.WriteToFile(pathDir, fileName, code, isLinux);
        }

        public void DeleteFiles(string pathDir, string fileName, bool isLinux)
        {
            FileMaker.DeleteFile(pathDir, fileName, isLinux);
        }
    }
}