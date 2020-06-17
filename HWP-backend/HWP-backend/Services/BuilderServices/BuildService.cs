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

        public BuildModelDTO BuildAndRun(string pathDir, string fileName, params string[] inputs)
        {
            var model = Build(pathDir, fileName);
            if (model.IsSuccess) 
                model.Output += $"\n{Run(pathDir, fileName, inputs)}";
            DeleteFiles(pathDir, fileName);
            return model;
        }

        private BuildModelDTO Build(string pathDir, string fileName)
        {
            var result = _builder.Build(pathDir, fileName);
            return result;
        }

        private string Run(string pathDir, string fileName, params string[] inputs)
        {
            var result = _builder.Run(pathDir, fileName, inputs);
            return result;
        }

        public void MakeFile(string pathDir, string fileName, string code)
        {
            FileMaker.WriteToFile(pathDir, fileName, code);
        }

        public void DeleteFiles(string pathDir, string fileName)
        {
            FileMaker.DeleteFile(pathDir, fileName);
        }
    }
}