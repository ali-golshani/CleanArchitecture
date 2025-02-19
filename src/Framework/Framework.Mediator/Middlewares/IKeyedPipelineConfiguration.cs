namespace Framework.Mediator.Middlewares;

public interface IKeyedPipelineConfiguration
{
    static abstract string PipelineName { get; }
    static abstract Type[] Middlewares();
}