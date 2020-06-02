using System;
using System.Diagnostics;
using System.Text;
using HWP_backend.Models.DTO.Builds;

namespace HWP_backend.Services.BuilderServices
{
    public abstract class ABuilder<T>
    {
        private readonly ProcessKiller _processKiller;

        protected ABuilder(ProcessKiller processKiller)
        {
            _processKiller = processKiller;
        }

        public BuildModelDTO BuildInternal(string buildCommand)
        {
            var outputMessage = "";
            var errorMessage = "";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "g++",
                    Arguments = buildCommand,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false
                }
            };

            try
            {
                _processKiller.KillProcess(process);
                process.Start();
                var sbError = new StringBuilder();
                var sbOut = new StringBuilder();
                while (!process.StandardOutput.EndOfStream) sbOut.Append($"{process.StandardOutput.ReadLine()}\n");
                if (!process.StandardError.EndOfStream) sbError.Append(process.StandardError.ReadToEnd());
                process.WaitForExit();

                errorMessage = sbError.ToString();
                outputMessage = errorMessage == "" ? sbOut.ToString() : errorMessage;
                outputMessage +=
                    $"Time Elapsed: {process.ExitTime - process.StartTime}\nProcess exit with code: {process.ExitCode}\n========== Finished ==========";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                process.Dispose();
            }

            var buildResult = new BuildModelDTO
            {
                IsSuccess = errorMessage == "",
                Output = outputMessage
            };

            return buildResult;
        }

        public string RunInternal(string runCommand, params string[] inputs)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = runCommand,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            _processKiller.KillProcess(process);

            process.Start();

            var writer = process.StandardInput;
            var count = 0;
            while (count < inputs.Length)
            {
                writer.WriteLine(inputs[count]);
                count++;
            }

            writer.Dispose();
            var stringBuilder = new StringBuilder();
            while (!process.StandardOutput.EndOfStream) stringBuilder.Append($"{process.StandardOutput.ReadLine()}\n");
            if (!process.StandardError.EndOfStream) stringBuilder.Append(process.StandardError.ReadToEnd());
            process.WaitForExit();
            stringBuilder.Append(
                $"\nTime Elapsed: {process.ExitTime - process.StartTime}\nProcess exit with code: {process.ExitCode}");
            return stringBuilder.ToString();
        }
    }
}