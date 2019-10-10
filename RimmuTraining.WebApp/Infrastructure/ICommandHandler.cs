using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Infrastructure
{
    public class Result
    {
        public Result()
        {
            Succeeded = true;
            Errors = new List<string>();
        }
        public Result(string error)
        {
            Succeeded = false;
            Errors = new List<string> { error };
        }

        public Result(params string[] errors)
        {
            Succeeded = false;
            Errors = new List<string>();
            for (int i = 0; i < errors.Length; i++)
            {
                Errors.Add(errors[i]);
            }
        }
        public Result(List<string> errors)
        {
            Succeeded = false;
            Errors = errors;
        }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
    }
    public interface ICommandHandler<TCommand>
    {
        Task<Result> HandleAsync(TCommand command);
    }
}
