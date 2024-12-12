namespace Framework.DomainRules.Policies;

public interface IPolicyBuilder<in T>
{
    Policy Build(T value);
}